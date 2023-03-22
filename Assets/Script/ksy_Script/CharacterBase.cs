using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterBase : MonoBehaviour
{
    [Header("플레이어 데이터")]
    //--------------------Public---------------------------

    public float moveSpeed = 5.0f;      //이동속도
    public float turnSpeed = 10.0f;     //회전속도

    //--------------------private---------------------------
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


    //----------------------------------일반 함수-------------------------------
    private void Start()
    {
        hp = maxHp;
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
        inputActions.CharacterMove.Interaction_Place.canceled += OnMaking;

        //Time.timeScale = 0.1f;

    }

    private void OnDisable()
    {
        inputActions.CharacterMove.Move.canceled -= OnMoveInput;
        inputActions.CharacterMove.Move.performed -= OnMoveInput;
        inputActions.CharacterMove.Activity.performed -= OnAvtivity;
        inputActions.CharacterMove.Activity.canceled -= OnAvtivityStop;
        inputActions.CharacterMove.Interaction_Item.performed -= OnGrab;
        inputActions.CharacterMove.Interaction_Item.canceled -= OnGrab;
        inputActions.CharacterMove.Interaction_Place.performed -= OnMaking;
        inputActions.CharacterMove.Interaction_Place.canceled -= OnMaking;
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
        Vector3 dir = context.ReadValue<Vector3>();

        anim.SetInteger("InputMove", (int)dir.x);
        anim.SetInteger("InputMove2", (int)dir.z);
        inputDir = dir;
    }

    void Move()
    {
        V3 = new Vector3(0, Input.GetAxis("Mouse X"), 0);

        transform.Rotate(V3 * turnSpeed);
        rigid.MovePosition(Time.fixedDeltaTime * moveSpeed * transform.TransformDirection(inputDir).normalized + transform.position);
    }

    //--- 공격용 함수(left click)---

    private void OnAvtivity(InputAction.CallbackContext _obj)
    {
        isAction = true;
        StartCoroutine(ActionCoroutine());
    }

    private void OnAvtivityStop(InputAction.CallbackContext _obj)
    {
        isAction = false;
        StopCoroutine(ActionCoroutine());
    }

    //---공격용 코루틴---
    IEnumerator ActionCoroutine()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("axe");

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

    private void OnMaking(InputAction.CallbackContext obj)
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
            yield return new WaitForSeconds(1.0f);
            hp--;
        }
        OnDie();
    }

    private void DecreaseMotionHp()
    {
        // 채집 관련 함수 예정
    }

    //죽음 처리 함수
    private void OnDie()
    {
        isAlive = false;
        anim.SetBool("isDead", true);
        Debug.Log("나 죽었어");
    }
}
