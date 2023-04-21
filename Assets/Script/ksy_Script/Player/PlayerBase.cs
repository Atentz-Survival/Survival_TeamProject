using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UIElements.UxmlAttributeDescription;

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
    private int hp = 1000;              // 현재 hp

    public bool isAction = false;
    private bool isRun = false;
    private bool isDead = false;
    private bool isConsumeHP = false;
    int usehp = 0;

    private bool isDoing = false;

    public int HP                      // 현재 hp 프로퍼티 > ui
    {
        get => hp;
        set
        {
            if (value > 1000)
            {
                hp = maxHp;
            }
            else if(1<value && value< 1000)
            {
                hp = value;
                Debug.Log(hp);
            }
            else if (value < 1 && isDead==false)
            {
                isDead = true;
                hp = 0;
                OnDie();
            }

            onUpgradeHp?.Invoke(hp / maxHp);
        }
    }

    public Action<float> onUpgradeHp;
    public Action onDie;
    public Action onMaking;
    public Action onInventory;

    /*------------------저장처리 -------------------*/

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
    SaveFileUI save;
    private Animator anim;
    private Rigidbody rigid;

    private ItemInventoryWindowExplanRoom item;

    private Axe axe;
    private FishinfRod fishingRod;
    private Reap reap;
    private Pick pick;
    private RightHand rHand;

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
        inputActions.CharacterMove.Inventory.performed += Oninventory;

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
        inputActions.CharacterMove.Inventory.performed -= Oninventory;

        inputActions.CharacterMove.Interaction_Place.performed -= OnMaking;

        inputActions.CharacterMove.Disable();
    }

   

    private void Start()
    {
        item = FindObjectOfType<ItemInventoryWindowExplanRoom>();
        axe = FindObjectOfType<Axe>();
        fishingRod = FindObjectOfType<FishinfRod>();
        reap = FindObjectOfType<Reap>();
        pick = FindObjectOfType<Pick>();
        rHand = FindObjectOfType<RightHand>();

        save = FindObjectOfType<SaveFileUI>();

        HP = maxHp;
        HpChange();
/*--------------------HP 델리게이트 전달 받기-------------------------------*/
        item.onChangeHp += OnUpgradeHp; // <<인벤

        axe.UsingTool += OnUpgradeHp;
        reap.UsingTool += OnUpgradeHp;
        pick.UsingTool += OnUpgradeHp;
        fishingRod.UsingTool += OnUpgradeHp;
        rHand.UsingTool += OnUpgradeHp;

        item.onChangeTool += OnUpgradeTool; // << tool

        save.onChangeTool += OnUpgradeTool; // << save
        save.LoadHp += LoadingHp;

        tools = new GameObject[toolsNames.Length];
        for (int i = 0; i < tools.Length; i++)
        {
            tools[i] = GameObject.Find(toolsNames[i]);
            tools[i].SetActive(false);
        }

        for (int i = 0; i < isEqualWithState.Length; i++)
        {
            isEqualWithState[i] = false;
        }
        isEqualWithState[0] = true;
        state = playerState.Nomal;
    }

    

    private void LoadingHp(int obj)
    {
        HP = obj;
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
        HP = HP + getHp;
    }
    void HpChange()
    {
       StartCoroutine(Decrease());
    }

    IEnumerator Decrease()
    {
            while (HP > 0 && isDead==false)
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
        onDie?.Invoke();
    }


    //--- 도끼질 곡갱이 질 함수(left click)---
    private void OnAvtivity(InputAction.CallbackContext context)
    {
        if (isAction)
        {
            if ((state == playerState.Fishing) && (isEqualWithState[4] == true))
            {
                StartCoroutine(Fishing());
            }
            else
            {
                if (isDoing == false)
                    StartCoroutine(ActionCoroutine());
            }
        }
    }

    private void OnAvtivityStop(InputAction.CallbackContext context)
    {
        isDoing = false;
        /*StopCoroutine(ActionCoroutine());*/
        StopCoroutine(Fishing());
    }

    //---공격용 코루틴---
    IEnumerator ActionCoroutine()
    {
        isDoing = true;
        while (isDoing)    //누르면 지속적으로 어택 모션 취하기
        {
            switch (state)
            {
                case playerState.TreeFelling:
                    if ((isEqualWithState[1] == true))
                    {
                        anim.SetTrigger("Axe_Trigger");
                    }
                    else
                    {
                        anim.SetTrigger("Hand_Trigger");
                    }
                    break;
                case playerState.Gathering:
                    if ((isEqualWithState[2] == true))
                    {
                        anim.SetTrigger("Reap_Trigger");
                    }
                    else
                    {
                        anim.SetTrigger("Hand_Trigger");
                    }
                    break;
                case playerState.Mining:
                    if ((isEqualWithState[3] == true))
                    {
                        anim.SetTrigger("Pick_Trigger");
                    }
                    else
                    {
                        anim.SetTrigger("Hand_Trigger");
                    }
                    break;
                case playerState.Fishing:
                    if (isEqualWithState[4] == false)
                    {
                        anim.SetTrigger("Hand_Trigger");
                    }
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
        isEqualWithState[0] = true;
        switch (toolItem)
        {
            case ToolItemTag.Axe:
                ResetToolState();
                tools[0].SetActive(true);
                axe.OnCangeAxelLevel();
                isEqualWithState[1] = true;
                break;
            case ToolItemTag.Sickle:
                ResetToolState();
                tools[1].SetActive(true);
                reap.OnCangeReapLevel();
                isEqualWithState[2] = true;
                break;
            case ToolItemTag.Pickaxe:
                ResetToolState();
                tools[2].SetActive(true);
                pick.OnCangePickLevel();
                isEqualWithState[3] = true;
                break;
            case ToolItemTag.Fishingrod:
                ResetToolState();
                tools[3].SetActive(true);
                fishingRod.OnCangeFishinfRodlLevel();
                isEqualWithState[4] = true;
                break;
        }
        GetToolItem?.Invoke(toolItem, level);
    }

    void ResetToolState()
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
        // 맵 오브젝트와 트리거가 닿았을 때
        isEqualWithState[0] = true;
        if (other.gameObject.CompareTag("Tree"))
        {
            isAction = true;
            state = playerState.TreeFelling;
            if (isEqualWithState[2] || isEqualWithState[3] || isEqualWithState[4] == true)
            {
                isAction = false;
                Debug.Log("사용불가 아이템");
            }

            //isTreeFelling = true;
        }
        else if (other.gameObject.CompareTag("Flower"))
        {
            isAction = true;
            state = playerState.Gathering;
            if (isEqualWithState[1] || isEqualWithState[3] || isEqualWithState[4] == true)
            {
                isAction = false;
                Debug.Log("사용불가 아이템");
            }
        }
        else if (other.gameObject.CompareTag("Rock"))
        {
            isAction = true;
            state = playerState.Mining;
            if (isEqualWithState[1] || isEqualWithState[2] || isEqualWithState[4] == true)
            {
                isAction = false;
                Debug.Log("사용불가 아이템");
            }
        }
        else if (other.gameObject.CompareTag("Ocean"))
        {
            isAction = true;
            state = playerState.Fishing;
            if (isEqualWithState[1] || isEqualWithState[2] || isEqualWithState[3] == true)
            {
                isAction = false;
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
            isAction = false;
        }
    }

    private void OnGrab(InputAction.CallbackContext context)
    {
        anim.SetBool("ItemGrab", !context.canceled);
    }


    //----------------------------------장소 상호작용 함수-------------------------------

    private void OnMaking(InputAction.CallbackContext context)      //F키
    {
        StopActionAtMake();
        onMaking?.Invoke();
    }

    private void StopActionAtMake()
    {
        GameObject obj = GameObject.Find("CraftingTable");
        if(obj.activeSelf == true)
        {
            inputActions.CharacterMove.Activity.Disable();
           inputActions.CharacterMove.MouseMove.Disable();
        }
        else
        {
            inputActions.CharacterMove.Activity.Enable();
            inputActions.CharacterMove.MouseMove.Enable();
        }
    }
    private void Oninventory(InputAction.CallbackContext obj)   // i키
    {
        StopActionAtInventory();
        onInventory.Invoke();
    }

    private void StopActionAtInventory()
    {
        if (item.gameObject.activeSelf == true)
        {
            inputActions.CharacterMove.Activity.Enable();
            inputActions.CharacterMove.MouseMove.Enable();
        }
        else if (item.gameObject.activeSelf == false)
        {
            inputActions.CharacterMove.Activity.Disable();
            inputActions.CharacterMove.MouseMove.Disable();
        }
    }
}
