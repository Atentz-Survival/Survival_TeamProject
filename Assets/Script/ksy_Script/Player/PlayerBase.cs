using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBase : MonoBehaviour
{
    // ����ð� 6�� = ���ӽð� 12�ð� / 1�� - 2�ð� / 30�� ���� hp 1 ����
    [Header("�÷��̾� ������")]
    //--------------------Public---------------------------
    public float moveSpeed = 5.0f;      //�̵��ӵ�
    public float turnSpeed = 0.5f;     //ȸ���ӵ�
    public float rushSpeed = 10.0f;
    //--------------------private---------------------------
    private float mouseDelta;          // ���콺�� ��ġ��
    private int maxHp = 1000;          // �ִ� hp
    public int hp = 1000;              // ���� hp

    public bool isAction = false;
    private bool isRun = false;

    public int HP                      // ���� hp ������Ƽ > ui
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

    /*------------------�÷��̾� ���� -------------------*/
    public enum playerState
    {
        Nomal,
        TreeFelling,    //����
        Gathering,      //Ǯä��
        Mining,         //ä��
        Fishing,        //����
    }

    playerState state;
    public Action<playerState> GetState;
    public playerState State
    {
        get => state;
    }
    /*------------------ToolItem ���� -------------------*/
    private bool[] isEqualWithState = new bool[5];   //playerState enum �������

    private GameObject[] tools;                     // axe, Reap, Pick, FishingRod �������
    private string[] toolsNames = { "Axe", "Reap", "Pick", "FishingRod" };

    public Action<ToolItemTag, int> GetToolItem;    //���� ������ ���� ��������Ʈ

    //------------------------------��Ÿ----------------------------------------------
    [Header("������Ʈ")]
    private Animator anim;
    private Rigidbody rigid;

    private ItemInventoryWindowExplanRoom item;

    private Axe axe;
    private FishinfRod fishingRod;
    private Reap reap;
    private Pick pick;

    [Header("�Է� ó����")]
    private PlayerInput inputActions;
    private Vector3 inputDir = Vector3.zero;
    Vector3 V3;

    //----------------------------------�Ϲ� �Լ�-------------------------------
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        inputActions = new PlayerInput();
    }
    private void OnEnable()
    {
        //�ִϸ��̼� �׽�Ʈ��

        /*inputActions.Test.Enable();
        inputActions.Test.Test1.performed += Test1;*/

        //-----�Է� Enable----
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

        //-----���� ���� ��������Ʈ----
    }


    private void OnDisable()
    {
        //�ִϸ��̼� �׽�Ʈ��

        /*inputActions.Test.Test1.performed -= Test1;
        inputActions.Test.Disable();*/
        //-----�Է� Disable----
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
        //������Ʈ ã��
        item = FindObjectOfType<ItemInventoryWindowExplanRoom>();
        axe = FindObjectOfType<Axe>();
        fishingRod = FindObjectOfType<FishinfRod>();
        reap = FindObjectOfType<Reap>();
        pick = FindObjectOfType<Pick>();

        //��������Ʈ �ޱ�
        item.onChangeHp += OnUpgradeHp; // <<�κ�
        axe.UsingTool += OnUpgradeHp;
        item.onChangeTool += OnUpgradeTool; // << tool

        //�迭�� tool���ӿ�����Ʈ ã��
        tools = new GameObject[toolsNames.Length];
        for (int i = 0; i < tools.Length; i++)
        {
            tools[i] = GameObject.Find(toolsNames[i]);
            tools[i].SetActive(false);
        }
        //�迭�� tool�� �ҹ迭 ��� false�� ������
        for (int i = 0; i < isEqualWithState.Length; i++)
        {
            isEqualWithState[i] = false;
        }

        inputActions.CharacterMove.Activity.Disable();  //�����Ҷ� ���콺Ŭ�� ����
        state = playerState.Nomal;      //�÷��̾��� ���´� ���
        HP = maxHp;                 //hp �ʱ�ȭ
        HpChange();                 //hp���
    }

    private void FixedUpdate()
    {
        Move();
    }

    //----------------------------------��ǲ�� �Լ�-------------------------------
    private void OnMoveInput(InputAction.CallbackContext context)
    {
        Vector3 input = context.ReadValue<Vector2>();   // ���� Ű���� �Է� ��Ȳ �ޱ�
        inputDir.z = input.y;
        inputDir.x = input.x;
        anim.SetBool("IsMove", !context.canceled);
    }
    private void OnMouseMoveInput(InputAction.CallbackContext context)      //���콺 x��ǥ �̵� delta�� �����ϱ�
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
        V3 = new Vector3(0, mouseDelta, 0);      //���콺 z��ǥ �̵� y�� ȸ�� ������ ����
        mouseDelta = 0.0f;                       //�ʱ�ȭ �۾�
        transform.Rotate(V3 * turnSpeed);        // �Ͻ��ǵ� �ӵ���ŭ ȸ��
        if (isRun == true)
        {
            rigid.MovePosition(Time.fixedDeltaTime * rushSpeed * transform.TransformDirection(inputDir).normalized + transform.position);
        }
        else
        {
            rigid.MovePosition(Time.fixedDeltaTime * moveSpeed * transform.TransformDirection(inputDir).normalized + transform.position);       // ����(���α��� �޿����Ʒ�) ���� �̵�ó��
        }
    }

    //----------------------------------Hp ������ �Լ�-------------------------------


    void OnUpgradeHp(int getHp)             //�κ����� ���޹��� hp(getHp)
    {
        if (HP > 0)
        {
            HP = HP + getHp;
            if (HP > maxHp)
            {
                HP = maxHp;
            }
            Debug.Log($"{getHp} : ����");
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
        //Debug.Log("�� �׾���");
        onDie?.Invoke(true);
    }


    //--- ������ ��� �� �Լ�(left click)---
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

        Debug.Log("�ն���");
    }

    //---���ݿ� �ڷ�ƾ---
    IEnumerator ActionCoroutine()
    {
        while (isAction == true)    //������ ���������� ���� ��� ���ϱ�
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

    void ResetToolSetting()                     //�� �����ϴ� �Լ�
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

    //----------------------------------�׷��� �Լ�-------------------------------

    private void OnTriggerEnter(Collider other)
    {
        // �����۰� Ʈ���Ű� ����� ��
        if (other.gameObject.CompareTag("DropItem"))
        {
            DropItem pick = other.GetComponent<DropItem>();
            if (pick != null)
            {
                pick.Picked();
            }
            Debug.Log(other.gameObject.name);
        }

        // �� ������Ʈ�� Ʈ���Ű� ����� ��
        isEqualWithState[0] = true;
        if (other.gameObject.CompareTag("Tree"))
        {
            inputActions.CharacterMove.Activity.Enable();
            state = playerState.TreeFelling;
            if (isEqualWithState[2] || isEqualWithState[3] || isEqualWithState[4] == true)
            {
                Debug.Log("���Ұ� ������");
            }
        }
        else if (other.gameObject.CompareTag("Flower"))
        {
            inputActions.CharacterMove.Activity.Enable();
            state = playerState.Gathering;
            isEqualWithState[(int)state] = true;
            if (isEqualWithState[1] || isEqualWithState[3] || isEqualWithState[4] == true)
            {
                Debug.Log("���Ұ� ������");
            }
        }
        else if (other.gameObject.CompareTag("Rock"))
        {
            inputActions.CharacterMove.Activity.Enable();
            state = playerState.Mining;
            isEqualWithState[(int)state] = true;
            if (isEqualWithState[1] || isEqualWithState[2] || isEqualWithState[4] == true)
            {
                Debug.Log("���Ұ� ������");
            }
        }
        else if (other.gameObject.CompareTag("Ocean"))
        {
            inputActions.CharacterMove.Activity.Enable();
            state = playerState.Fishing;
            isEqualWithState[(int)state] = true;
            if (isEqualWithState[1] || isEqualWithState[2] || isEqualWithState[3] == true)
            {
                Debug.Log("���Ұ� ������");
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
            isEqualWithState[(int)state] = true;        // playerState.Nomal �� true
        }
    }

    private void OnGrab(InputAction.CallbackContext context)
    {
        anim.SetBool("ItemGrab", !context.canceled);
    }

    //----------------------------------��� ��ȣ�ۿ� �Լ�-------------------------------

    private void OnMaking(InputAction.CallbackContext context)
    {
        int useHp = 50;                         // �ൿ�� ���� hp

        if (HP > useHp)
        {
            anim.SetTrigger("Making_Trigger");
            HP -= 50;                               // �ൿ�� ���� hp
            //Debug.Log($"{hp} : ��� �߾��~~");

        }
    }
}