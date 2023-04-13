using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

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

    public bool isAction = false;
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

            if (hp < 1)
            {
                OnDie();
            }


            onUpgradeHp?.Invoke(HP / maxHp);
        }
    }

    public Action<float> onUpgradeHp;
    public Action<bool> onDie;

    /*------------------플레이어 상태 -------------------*/
    public enum playerState
    {
        Nomal,
        TreeFelling,    //벌목
        Gathering,      //풀채집
        Mining,         //채광
        Fishing,        //낚시
    }

    playerState state;
    public Action<playerState> GetState;
    public playerState State
    {
        get => state;
    }
    /*------------------ToolItem 상태 -------------------*/
    private bool[] isEqualWithState = new bool[5];   //playerState enum 순서대로

    private GameObject[] tools;                     // axe, Reap, Pick, FishingRod 순서대로
    private string[] toolsNames = { "Axe", "Reap", "Pick", "FishingRod" };

    public Action<ToolItemTag, int> GetToolItem;    //장착 아이템 관련 델리게이트

    //------------------------------기타----------------------------------------------
    [Header("컴포넌트")]
    private Animator anim;
    private Rigidbody rigid;

    private ItemInventoryWindowExplanRoom item;

    private Axe axe;
    private FishinfRod fishingRod;
    private Reap reap;
    private Pick pick;

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
    }
    private void OnEnable()
    {
        //애니메이션 테스트용

        /*inputActions.Test.Enable();
        inputActions.Test.Test1.performed += Test1;*/

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

        /*inputActions.Test.Test1.performed -= Test1;
        inputActions.Test.Disable();*/
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
        //컴포턴트 찾기
        item = FindObjectOfType<ItemInventoryWindowExplanRoom>();
        axe = FindObjectOfType<Axe>();
        fishingRod = FindObjectOfType<FishinfRod>();
        reap = FindObjectOfType<Reap>();
        pick = FindObjectOfType<Pick>();

        //델리게이트 받기
        item.onChangeHp += OnUpgradeHp; // <<인벤
        axe.UsingTool += OnUpgradeHp;
        item.onChangeTool += OnUpgradeTool; // << tool

        //배열로 tool게임오브젝트 찾기
        tools = new GameObject[toolsNames.Length];
        for (int i = 0; i < tools.Length; i++)
        {
            tools[i] = GameObject.Find(toolsNames[i]);
            tools[i].SetActive(false);
        }
        //배열로 tool의 불배열 모두 false로 돌리기
        for (int i = 0; i < isEqualWithState.Length; i++)
        {
            isEqualWithState[i] = false;
        }

        inputActions.CharacterMove.Activity.Disable();  //시작할때 마우스클릭 막기
        state = playerState.Nomal;      //플레이어의 상태는 노멀
        HP = maxHp;                 //hp 초기화
        HpChange();                 //hp깎기
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
        mouseDelta = 0.0f;                       //초기화 작업
        transform.Rotate(V3 * turnSpeed);        // 턴스피드 속도만큼 회전
        if (isRun == true)
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
        if (HP > 0)
        {
            HP = HP + getHp;
            if (HP > maxHp)
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
        if (state == playerState.Fishing && isEqualWithState[4] == true)
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
        if (state == playerState.Fishing && isEqualWithState[4] == true)
        {
            StopCoroutine(Fishing());
        }
        else
        { 
            StartCoroutine(ActionCoroutine()); 
        }

        Debug.Log("손땔게");
    }

    //---공격용 코루틴---
    IEnumerator ActionCoroutine()
    {
        while (isAction == true)    //누르면 지속적으로 어택 모션 취하기
        {
            switch (state)
            {
                case playerState.Nomal:
                    anim.SetTrigger("Hand_Trigger");
                    break;
                case playerState.TreeFelling:
                    anim.SetTrigger("Axe_Trigger");
                    break;
                case playerState.Gathering:
                    anim.SetTrigger("Reap_Trigger");
                    break;
                case playerState.Mining:
                    anim.SetTrigger("Pick_Trigger");
                    break;
            }
            yield return new WaitForSeconds(1.0f);
        }
    }


    IEnumerator Fishing()
    {
        anim.SetBool("IsFishing", true);
        yield return new WaitForSeconds(5.0f);
        anim.SetBool("IsFishing", false);

    }
    private void OnUpgradeTool(ToolItemTag toolItem, int level)
    {
        switch (toolItem)
        {
            case ToolItemTag.Axe:
                ResetToolSetting();
                tools[0].SetActive(true);
                axe.OnCangeAxelLevel();
                isEqualWithState[1] = true;
                break;
            case ToolItemTag.Sickle:
                ResetToolSetting();
                tools[1].SetActive(true);
                reap.OnCangeReapLevel();
                isEqualWithState[2] = true;
                break;
            case ToolItemTag.Pickaxe:
                ResetToolSetting();
                tools[2].SetActive(true);
                pick.OnCangePickLevel();
                isEqualWithState[3] = true;
                break;
            case ToolItemTag.Fishingrod:
                ResetToolSetting();
                tools[3].SetActive(true);
                fishingRod.OnCangeFishinfRodlLevel();
                isEqualWithState[4] = true;
                break;
        }
        GetToolItem?.Invoke(toolItem, level);
    }

    void ResetToolSetting()                     //툴 리셋하는 함수
    {
        for (int i = 0; i < tools.Length; i++)
        {
            tools[i].SetActive(false);
        }
        for (int i = 0; i < isEqualWithState.Length; i++)
        {
            isEqualWithState[i] = false;
        }
    }

    //----------------------------------그랩용 함수-------------------------------

    private void OnTriggerEnter(Collider other)
    {
        // 아이템과 트리거가 닿았을 때
        if (other.gameObject.CompareTag("DropItem"))
        {
            DropItem pick = other.GetComponent<DropItem>();
            if (pick != null)
            {
                pick.Picked();
            }
            Debug.Log(other.gameObject.name);
        }

        // 맵 오브젝트와 트리거가 닿았을 때
        isEqualWithState[0] = true;
        if (other.gameObject.CompareTag("Tree"))
        {
            inputActions.CharacterMove.Activity.Enable();
            state = playerState.TreeFelling;
            if (isEqualWithState[2] || isEqualWithState[3] || isEqualWithState[4] == true)
            {
                Debug.Log("사용불가 아이템");
            }
        }
        else if (other.gameObject.CompareTag("Flower"))
        {
            inputActions.CharacterMove.Activity.Enable();
            state = playerState.Gathering;
            isEqualWithState[(int)state] = true;
            if (isEqualWithState[1] || isEqualWithState[3] || isEqualWithState[4] == true)
            {
                Debug.Log("사용불가 아이템");
            }
        }
        else if (other.gameObject.CompareTag("Rock"))
        {
            inputActions.CharacterMove.Activity.Enable();
            state = playerState.Mining;
            isEqualWithState[(int)state] = true;
            if (isEqualWithState[1] || isEqualWithState[2] || isEqualWithState[4] == true)
            {
                Debug.Log("사용불가 아이템");
            }
        }
        else if (other.gameObject.CompareTag("Ocean"))
        {
            inputActions.CharacterMove.Activity.Enable();
            state = playerState.Fishing;
            isEqualWithState[(int)state] = true;
            if (isEqualWithState[1] || isEqualWithState[2] || isEqualWithState[3] == true)
            {
                Debug.Log("사용불가 아이템");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Tree")
           || other.gameObject.CompareTag("Flower")
           || other.gameObject.CompareTag("Rock")
           || other.gameObject.CompareTag("Ocean"))
        {
            inputActions.CharacterMove.Activity.Disable();
            state = playerState.Nomal;
            for (int i = 0; i < isEqualWithState.Length; i++)
            {
                isEqualWithState[i] = false;
            }
            isEqualWithState[(int)state] = true;        // playerState.Nomal 만 true
        }
    }

    private void OnGrab(InputAction.CallbackContext context)
    {
        anim.SetBool("ItemGrab", !context.canceled);
    }

    //----------------------------------장소 상호작용 함수-------------------------------

    private void OnMaking(InputAction.CallbackContext context)
    {
        int useHp = 50;                         // 행동에 따른 hp

        if (HP > useHp)
        {
            anim.SetTrigger("Making_Trigger");
            HP -= 50;                               // 행동에 따른 hp
            //Debug.Log($"{hp} : 사용 했어요~~");

        }
    }
}