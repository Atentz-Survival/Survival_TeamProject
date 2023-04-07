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

    private bool isAction = false;
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

            if(hp < 1)
            {
                OnDie();
            }


            onUpgradeHp?.Invoke(HP / maxHp);
        }
    }

    public Action<float> onUpgradeHp;
    public Action<bool> onDie;

    /*------------------�÷��̾� ���� ������Ƽ-------------------*/
    public enum playerState
    {
        Nomal,
        TreeFelling,    //����
        Gathering,      //Ǯä��
        Mining,         //ä��
        Fishing,        //����
    }

    playerState state;
    public Action <playerState> GetState;
    public playerState State
    {
        get=> state;
    }

    private bool[] isEqualWithState = new bool[5];   //playerState enum �������

    private GameObject[] tools;                     // axe, Reap, Pick, FishingRod �������
    private string[] toolsNames = { "axe", "Reap", "Pick", "FishingRod" };

    //------------------------------��Ÿ----------------------------------------------
    [Header("������Ʈ")]
    private Animator anim;
    private Rigidbody rigid;
    private ItemInventoryWindowExplanRoom item;

    Collider handCollider;

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

        handCollider = GetComponent<Collider>();
    }
    private void OnEnable()
    {
        //�ִϸ��̼� �׽�Ʈ��

        inputActions.Test.Enable();
        inputActions.Test.Test1.performed += Test1;

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

        inputActions.Test.Test1.performed -= Test1;
        inputActions.Test.Disable();
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
        item = FindObjectOfType<ItemInventoryWindowExplanRoom>();
        HP = maxHp;
        HpChange();
        //Debug.Log(hp);
        item.onChangeHp += OnUpgradeHp; // <<�κ�

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
        mouseDelta = 0.0f;                       //�ʱ��� �۾�
        transform.Rotate(V3 * turnSpeed);        // �Ͻ��ǵ� �ӵ���ŭ ȸ��
        if(isRun == true)
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
        if (HP > 0 )
        {
            HP = HP + getHp;
            if(HP > maxHp)
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

    //---���ݿ� �ڷ�ƾ---
   IEnumerator ActionCoroutine()
    {
        while (true)    //������ ���������� ���� ��� ���ϱ�
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
                isEqualWithState[i] = false;                    // bool �迭 �ʱ�ȭ�ϰ�
            }
            isEqualWithState[(int)state] = true;        // playerState.Nomal �� true
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
    //----------------------------------�׷��� �Լ�-------------------------------

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
    //----------------------------------��� ��ȣ�ۿ� �Լ�-------------------------------

    private void OnMaking(InputAction.CallbackContext context)
    {
        int useHp = 50;                         // �ൿ�� ���� hp

        if (HP > useHp)
        {
            anim.SetTrigger("Making_Trigger");
            HP -= 50;
            //Debug.Log($"{hp} : ��� �߾��~~");
        }
    }

}
