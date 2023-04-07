using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor.Sprites;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class PlayerBase : MonoBehaviour
{
    // 현재시간 6분 = 게임시간 12시간 / 1분 - 2시간 / 30초 마다 hp 1 감소
    [Header("플레이어 데이터")]
    //--------------------Public---------------------------
    public float moveSpeed = 5.0f;      //이동속도
    public float turnSpeed = 0.5f;     //회전속도
    public float rushSpeed = 10.0f;
    //--------------------private---------------------------
    private float mouseDelta;          // 마우스의 위치값
    private int maxHp = 1000;          // 최대 hp
    public int hp = 1000;              // 현재 hp

    private bool isAction = false;
    private bool isRun = false;

    public int HP                      // 현재 hp 프로퍼티 > ui
    {
        get => hp;
        set
        {

            if (value > 1000)
            {
                HP = maxHp;
            }
                hp = value;

            if(hp < 1)
            {
                OnDie();
            }


            onUpgradeHp?.Invoke(HP / maxHp);
        }
    }

    public Action<float> onUpgradeHp;
    public Action<bool> onDie;

    /*------------------플레이어 상태 프로퍼티-------------------*/
    public enum playerState
    {
        Nomal,
        TreeFelling,    //벌목
        Gathering,      //풀채집
        Mining,         //채광
        Fishing,        //낚시
    }

    playerState state;
    public Action <playerState> GetState;
    public playerState State
    {
        get=> state;
    }

    private bool[] isEqualWithState = new bool[5];   //playerState enum 순서대로

    private GameObject[] tools;                     // axe, Reap, Pick, FishingRod 순서대로
    private string[] toolsNames = { "axe", "Reap", "Pick", "FishingRod" };

    //------------------------------기타----------------------------------------------
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
        inputActions.CharacterMove.Rush.performed += OnRunInput;
        inputActions.CharacterMove.Rush.canceled += OnRunInputStop;

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
        inputActions.CharacterMove.Rush.performed -= OnRunInput;
        inputActions.CharacterMove.Rush.canceled -= OnRunInputStop;

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

        tools = new GameObject[toolsNames.Length];
        for (int i = 0; i < tools.Length; i++)
        {
            tools[i] = GameObject.Find(toolsNames[i]);
            tools[i].SetActive(false);
        }

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

    private void OnRunInput(InputAction.CallbackContext obj)
    {
        isRun = true;
        anim.SetBool("isRun", isRun);
    }
    private void OnRunInputStop(InputAction.CallbackContext obj)
    {
        isRun = false;
        anim.SetBool("isRun", isRun);
    }
    void Move()
    {
        V3 = new Vector3(0, mouseDelta, 0);      //마우스 z좌표 이동 y축 회전 값으로 저장
        mouseDelta = 0.0f;                       //초기하 작업
        transform.Rotate(V3 * turnSpeed);        // 턴스피드 속도만큼 회전
        if(isRun == true)
        {
            rigid.MovePosition(Time.fixedDeltaTime * rushSpeed * transform.TransformDirection(inputDir).normalized + transform.position);
        }
        else
        {
            rigid.MovePosition(Time.fixedDeltaTime * moveSpeed * transform.TransformDirection(inputDir).normalized + transform.position);       // 로컬(본인기준 왼오위아래) 방향 이동처리
        }
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
        onDie?.Invoke(true);
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
            switch (state)
            {
                case playerState.Nomal:
                    if (isAction == true)
                    {
                        anim.SetTrigger("Making_Trigger");
                    }
                    break;
                case playerState.TreeFelling:
                    if (isAction == true)
                    {
                        anim.SetTrigger("Axe_Trigger");
                    }
                    break;
                case playerState.Gathering:
                    if (isAction == true)
                    {
                        anim.SetTrigger("Reap_Trigger");
                    }
                    break;
                case playerState.Mining:
                    if (isAction == true)
                    {
                        anim.SetTrigger("Pick_Trigger");
                    }
                    break;
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
            case playerState.Nomal:
                    for (int i = 0; i < tools.Length; i++)
                    {
                        tools[i].SetActive(false);
                    }
                break;
            case playerState.Gathering:
                WhatKindTool();
                ItemManager.Instance.itemInventory.GetEquipToolLevel(ToolItemTag.Sickle);
                break;
            case playerState.Fishing:
                WhatKindTool();
                ItemManager.Instance.itemInventory.GetEquipToolLevel(ToolItemTag.Fishingrod);

                break;
            case playerState.TreeFelling:
                WhatKindTool();
                ItemManager.Instance.itemInventory.GetEquipToolLevel(ToolItemTag.Axe);
                break;
            case playerState.Mining:
                WhatKindTool();
                ItemManager.Instance.itemInventory.GetEquipToolLevel(ToolItemTag.Pickaxe);
                break;
        }
        Debug.Log(state);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Tree"))
        {
            state = playerState.TreeFelling;
            isEqualWithState[(int)state] = true;

            //isTreeFelling = true;
        }
        else if(collision.gameObject.CompareTag("Flower"))
        {
            state = playerState.Gathering;
            isEqualWithState[(int)state] = true;
        }
        else if(collision.gameObject.CompareTag("Rock"))
        {
            state = playerState.Mining;
            isEqualWithState[(int)state] = true;
        }
        else if(collision.gameObject.CompareTag("Ocean"))
        {
            state = playerState.Fishing;
            isEqualWithState[(int)state] = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {

        if (collision.gameObject.CompareTag("Tree")
            || collision.gameObject.CompareTag("Flower")
            || collision.gameObject.CompareTag("Rock")
            || collision.gameObject.CompareTag("Ocean"))
        {
            state = playerState.Nomal;
            for (int i = 0; i < isEqualWithState.Length; i++)
            {
                isEqualWithState[i] = false;                    // bool 배열 초기화하고
            }
            isEqualWithState[(int)state] = true;        // playerState.Nomal 만 true
        }
    }

    private void WhatKindTool()
    {
        for (int i = 0; i < tools.Length; i++)
        {
            tools[i].SetActive(false);
        }
        tools[(int)state - 1].SetActive(true);
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
