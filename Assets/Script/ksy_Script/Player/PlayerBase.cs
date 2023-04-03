using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor.Sprites;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBase : MonoBehaviour
{
    // 현재시간 6분 = 게임시간 12시간 / 1분 - 2시간 / 30초 마다 hp 1 감소
    [Header("플레이어 데이터")]
    //--------------------Public---------------------------
    public float moveSpeed = 5.0f;      //이동속도
    public float turnSpeed = 0.5f;     //회전속도
    //--------------------private---------------------------
    private float mouseDelta;          // 마우스의 위치값
    private int maxHp = 1000;          // 최대 hp
    private int hp = 1000;              // 현재 hp

    private bool isAction = false;

    public int HP                      // 현재 hp 프로퍼티 > ui
    {
        get => hp;
        set
        {

            if (value > 1000)
            {
                Debug.LogError("올바르지 않은 값 입력됨");
            }
            else
                hp = value;

            onUpgradeHp?.Invoke(HP / maxHp);
        }
    }

    public Action<float> onUpgradeHp;

    /*플레이어 상태 프로퍼티*/
    public enum playerState
    {
        Nomal,
        Gathering,      //풀채집
        Fishing,        //낚시
        TreeFelling,    //벌목
        Mining,         //채광
    }

    public playerState State
    {
        get => state;
        set
        {
            playerState state = value;
        }
    }
    
    playerState state;

    GameObject fishing;
    GameObject axe;
    GameObject Reap;
    GameObject Pick;

    [Header("컴포넌트")]
    private Animator anim;
    private Rigidbody rigid;
    private ItemInventoryWindowExplanRoom item;

    Collider handCollider;

    [Header("입력 처리용")]
    private PlayerInput inputActions;
    private Vector3 inputDir = Vector3.zero;
    Vector3 V3;

    //----------------------------------일반 함수-------------------------------
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        inputActions = new PlayerInput();

        handCollider = GetComponent<Collider>();
    }
    private void OnEnable()
    {
        //애니메이션 테스트용

        inputActions.Test.Enable();
        inputActions.Test.Test1.performed += Test1;

        //-----입력 Enable----
        inputActions.CharacterMove.Enable();
        inputActions.CharacterMove.MouseMove.performed += OnMouseMoveInput;
        inputActions.CharacterMove.Move.performed += OnMoveInput;
        inputActions.CharacterMove.Move.canceled += OnMoveInput;

        inputActions.CharacterMove.Activity.performed += OnAvtivity;
        inputActions.CharacterMove.Activity.canceled += OnAvtivityStop;

        inputActions.CharacterMove.Interaction_Item.performed += OnGrab;
        inputActions.CharacterMove.Interaction_Item.canceled += OnGrab;

        inputActions.CharacterMove.Interaction_Place.performed += OnMaking;

        //-----전달 받을 델리게이트----
    }
    private void OnDisable()
    {
        //애니메이션 테스트용

        inputActions.Test.Test1.performed -= Test1;
        inputActions.Test.Disable();
        //-----입력 Disable----
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

    private void Start()
    {
        item = FindObjectOfType<ItemInventoryWindowExplanRoom>();
        HP = maxHp;
        HpChange();
        //Debug.Log(hp);
        item.onChangeHp += OnUpgradeHp; // <<인벤

        fishing = GameObject.Find("FishingRod");
        fishing.SetActive(false);
        axe = GameObject.Find("axe");
        axe.SetActive(false);
        Reap = GameObject.Find("Reap");
        Reap.SetActive(false);
        Pick = GameObject.Find("Pick");
        Pick.SetActive(false);

        state = playerState.Nomal;
    }
    private void FixedUpdate()
    {
        Move();
    }

    //----------------------------------인풋용 함수-------------------------------
    private void OnMoveInput(InputAction.CallbackContext context)
    {
        Vector3 input = context.ReadValue<Vector2>();   // 현재 키보드 입력 상황 받기
        inputDir.z = input.y;
        inputDir.x = input.x;
        anim.SetBool("IsMove", !context.canceled);
    }
    private void OnMouseMoveInput(InputAction.CallbackContext context)      //마우스 x좌표 이동 delta에 저장하기
    {
        mouseDelta = context.ReadValue<float>();
    }
    void Move()
    {
        V3 = new Vector3(0, mouseDelta, 0);      //마우스 z좌표 이동 y축 회전 값으로 저장
        mouseDelta = 0.0f;                       //초기하 작업
        transform.Rotate(V3 * turnSpeed);        // 턴스피드 속도만큼 회전
        rigid.MovePosition(Time.fixedDeltaTime * moveSpeed * transform.TransformDirection(inputDir).normalized + transform.position);       // 로컬(본인기준 왼오위아래) 방향 이동처리
    }

    //----------------------------------Hp 관리용 함수-------------------------------


    void OnUpgradeHp(int getHp)             //인벤에서 전달받을 hp(getHp)
    {
        if (HP > 0 )
        {
            HP = HP + getHp;
            if(HP > maxHp)
            {
                HP = maxHp;
            }
            Debug.Log($"{getHp} : 얻음");
        }
    }


    void HpChange()
    {
        if (HP > 0)
        {
            StartCoroutine(Decrease());
            OnUpgradeHp(hp);
        }
    }

    IEnumerator Decrease()
    {
        while (HP > 0)
        {
            yield return new WaitForSeconds(1.0f);  //test
            HP--;
        }
    }

    private void OnDie()
    {
        StopCoroutine(Decrease());
        inputActions.CharacterMove.Disable();
        anim.SetTrigger("IsDead");
        //Debug.Log("나 죽었어");
    }


    //--- 도끼질 곡갱이 질 함수(left click)---
    private void OnAvtivity(InputAction.CallbackContext context)
    {
        isAction = true;
        if(state == playerState.Fishing)
        {
            StartCoroutine(Fishing());
        }
        else
        {
            StartCoroutine(ActionCoroutine());
        }
    }

    private void OnAvtivityStop(InputAction.CallbackContext context)
    { 
        isAction = false;
        if (state == playerState.Fishing)
        {
            StopCoroutine(Fishing());
        }
        else
        {
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


    IEnumerator Fishing()
    {
        anim.SetBool("IsFishing", true);
        yield return new WaitForSeconds(5.0f);
        anim.SetBool("IsFishing", false);

    }
    void Test1(InputAction.CallbackContext context)
    {
        switch (state)
        {
            case playerState.Gathering:
                fishing.SetActive(false);
                axe.SetActive(false);
                Reap.SetActive(true);
                Pick.SetActive(false);
                break;
            case playerState.Fishing:

                fishing.SetActive(true);
                axe.SetActive(false);
                Reap.SetActive(false);
                Pick.SetActive(false);

                break;
            case playerState.TreeFelling:
                fishing.SetActive(false);
                axe.SetActive(true);
                Reap.SetActive(false);
                Pick.SetActive(false);
                break;
            case playerState.Mining:
                fishing.SetActive(false);
                axe.SetActive(false);
                Reap.SetActive(false);
                Pick.SetActive(true);
                break;
        }
        Debug.Log(state);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Tree"))
        {
            state = playerState.TreeFelling;
        }
        else if(collision.gameObject.CompareTag("Flower"))
        {
            state = playerState.Gathering;
        }
        else if(collision.gameObject.CompareTag("Rock"))
        {
            state = playerState.Mining;
        }
        else if(collision.gameObject.CompareTag("Ocean"))
        {
            state = playerState.Fishing;
        }
    }
    //----------------------------------그랩용 함수-------------------------------

    private void OnGrab(InputAction.CallbackContext context)
    {
        anim.SetBool("ItemGrab", !context.canceled);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DropItem"))
        {
            DropItem pick = other.GetComponent<DropItem>();
            if (pick != null)
            {
                pick.Picked();
            }
            Debug.Log(other.gameObject.name);
        }
    }
    //----------------------------------장소 상호작용 함수-------------------------------

    private void OnMaking(InputAction.CallbackContext context)
    {
        int useHp = 50;                         // 행동에 따른 hp

        if (HP > useHp)
        {
            anim.SetTrigger("Making_Trigger");
            HP -= 50;
            //Debug.Log($"{hp} : 사용 했어요~~");
        }
    }

}
