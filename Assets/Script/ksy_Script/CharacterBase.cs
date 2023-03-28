using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static CharacterBase;

public class CharacterBase : MonoBehaviour
{
    //뭔가가 이상하다
    [Header("플레이어 데이터")]
    //--------------------Public---------------------------

    public float moveSpeed = 5.0f;      //이동속도
    public float turnSpeed = 0.5f;     //회전속도

    //--------------------private---------------------------
    private float delta;        // 마우스의 위치값
    private int maxHp = 1000;
    private int hp = 1000;
    private bool isAlive = true;

    private Animator anim;
    private Rigidbody rigid;

    [Header("입력 처리용")]
    private PlayerInput inputActions;
    private Vector3 inputDir = Vector3.zero;
    Vector3 V3;

    IEnumerator actionCoroutine;    //공격용 코루틴
    IEnumerator DecreaseCoroutine;//실시간 hp -- 코루틴

    bool isAction = false;

    //------- 플레이어의 행동 양식 -------

    public enum playerAtive
    {
        Nomal = 0,

        Gathering,      //풀채집
        Fishing,        //낚시
        TreeFelling,    //벌목
        Mining,         //채광

        MakingBoat,     //배 제작
        Sleeping,       //자기
        ToolMaking      //무기 업그레이드
    }

    playerAtive state = playerAtive.Nomal;

    //----------------------------------일반 함수-------------------------------
    private void Start()
    {
        hp = maxHp;
        state = playerAtive.Mining;
        Debug.Log(state);
        
        StartCoroutine(Decrease());
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        inputActions = new PlayerInput();
        actionCoroutine = ActionCoroutine();
    }

    private void OnEnable()
    {
        inputActions.CharacterMove.Enable();
        inputActions.CharacterMove.Move.performed += OnMoveInput;
        inputActions.CharacterMove.Move.canceled += OnMoveInput;

        inputActions.CharacterMove.Activity.performed += OnAvtivity;
        inputActions.CharacterMove.Activity.canceled += OnAvtivityStop;

        inputActions.CharacterMove.Interaction_Item.performed += OnGrab;
        inputActions.CharacterMove.Interaction_Item.canceled += OnGrab;

        inputActions.CharacterMove.Interaction_Place.performed += OnMaking;
        inputActions.CharacterMove.MouseMove.performed += OnMouseMoveInput;



        //Time.timeScale = 0.1f;

    }



    private void OnDisable()
    {
        inputActions.CharacterMove.MouseMove.performed -= OnMouseMoveInput;
        inputActions.CharacterMove.Move.canceled -= OnMoveInput;
        inputActions.CharacterMove.Move.performed -= OnMoveInput;
        inputActions.CharacterMove.Activity.performed -= OnAvtivity;
        inputActions.CharacterMove.Activity.canceled -= OnAvtivityStop;
        inputActions.CharacterMove.Interaction_Item.performed -= OnGrab;
        inputActions.CharacterMove.Interaction_Item.canceled -= OnGrab;
        inputActions.CharacterMove.Interaction_Place.performed -= OnMaking;
        inputActions.CharacterMove.Disable();

    }

    private void FixedUpdate()
    {
        Move();
    }

    //----------------------------------인풋용 함수-------------------------------
    //--- 이동용 함수(wsad)---
    private void OnMoveInput(InputAction.CallbackContext context)
    {
        Vector3 input = context.ReadValue<Vector2>();   // 현재 키보드 입력 상황 받기
        inputDir.z = input.y;
        inputDir.x = input.x;
        anim.SetBool("IsMove", !context.canceled);
    }

    private void OnMouseMoveInput(InputAction.CallbackContext context)      //마우스 x좌표 이동 delta에 저장하기
    {
        delta = context.ReadValue<float>();
    }

    void Move()
    {
        V3 = new Vector3(0, delta, 0);      //마우스 w좌표 이동 y축 회전 값으로 저장
        delta = 0.0f;                       //쵝화 작업
        transform.Rotate(V3 * turnSpeed);   // 턴스피드 속도만큼 회전
        rigid.MovePosition(Time.fixedDeltaTime * moveSpeed * transform.TransformDirection(inputDir).normalized + transform.position);       // 로컬(본인기준 왼오위아래) 방향 이동처리
    }

    //--- 공격용 함수(left click)---

    private void OnAvtivity(InputAction.CallbackContext context)
    {
        if (state != playerAtive.Nomal)
        {
            isAction = true;
            StartCoroutine(ActionCoroutine());
        }
    }

    private void OnAvtivityStop(InputAction.CallbackContext context)
    {
        if (state != playerAtive.Nomal)
        {
            isAction = false;
            StopCoroutine(ActionCoroutine());
        }
    }

    //---공격용 코루틴---
    IEnumerator ActionCoroutine()
    {

        while (true)    //누르면 지속적으로 어택 모션 취하기
        {
            if (isAction == true)
            {
                anim.SetTrigger("Attack-trigger");
            }
            yield return new WaitForSeconds(2.5f);
        }

    }

    //----------------------------------그랩용 함수-------------------------------

    private void OnGrab(InputAction.CallbackContext context)
    {
        anim.SetBool("ItemGrab", !context.canceled);
    }

    //----------------------------------장소 상호작용 함수-------------------------------

    private void OnMaking(InputAction.CallbackContext context)
    {
        int useHp = 50;                         // 행동에 따른 hp

        if(hp > useHp)
        {
            anim.SetTrigger("Making_Trigger");
            hp -= 50;
            Debug.Log($"{hp} : 사용 했어요~~");
        }
        else
        {
            Debug.Log($"{hp} : 배고파요");

        }
    }

    //----------------------------------HP 관련 함수-------------------------------
    
    //실시간 hp 깎는 코루틴
    IEnumerator Decrease()
    {
        while(hp>0)
        {
            isAlive = true;
            yield return new WaitForSeconds(5.0f);
            hp--;
        }
        OnDie();
    }

    private void ConsumeHp()
    {
        // 채집 관련 함수 예정
        switch (state)
        {
            case playerAtive.Nomal:
                StartCoroutine(Decrease());
                inputActions.CharacterMove.Activity.Enable();
                break;
            case playerAtive.Gathering:
                //hp -= 15;
                break;
            case playerAtive.Fishing:
                //hp -= 15;
                break;
            case playerAtive.TreeFelling:
                //hp -= 30;
                break;
            case playerAtive.Mining:
                //hp -= 30;
                break;

            case playerAtive.MakingBoat:
                //hp -= 20;
                break;
            case playerAtive.Sleeping:
                //decrese 시간 간격 증가
                break;
            case playerAtive.ToolMaking:
                //hp -= 10;
                break;
        }
    }

    //죽음 처리 함수
    private void OnDie()
    {
        isAlive = false;
        inputActions.CharacterMove.Disable();
        anim.SetTrigger("IsDead");
        Debug.Log("나 죽었어");
    }
}
