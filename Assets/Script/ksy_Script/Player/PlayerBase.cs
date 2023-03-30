using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBase : MonoBehaviour
{
    // ����ð� 6�� = ���ӽð� 12�ð� / 1�� - 2�ð� / 30�� ���� hp 1 ����
    [Header("�÷��̾� ������")]
    //--------------------Public---------------------------
    public float moveSpeed = 5.0f;      //�̵��ӵ�
    public float turnSpeed = 0.5f;     //ȸ���ӵ�
    //--------------------private---------------------------
    private float mouseDelta;          // ���콺�� ��ġ��
    private int maxHp = 1000;          // �ִ� hp
    private int hp = 1000;              // ���� hp

    bool isAction = false;

    public int HP                      // ���� hp ������Ƽ > ui
    {
        get => hp;
        set
        {

            if (value > 1000)
            {
                Debug.LogError("�ùٸ��� ���� �� �Էµ�");
            }
            else
                hp = value;
            /*
            hp = value;

            if(hp < 1)
            {
                OnDie();
            }*/
        }
    }

    [Header("������Ʈ")]
    private Animator anim;
    private Rigidbody rigid;
    private ItemInventoryWindowExplanRoom item;

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

        inputActions.Test.Enable();
        inputActions.Test.Test1.performed += Test1;

        //-----�Է� Enable----
        inputActions.CharacterMove.Enable();
        inputActions.CharacterMove.MouseMove.performed += OnMouseMoveInput;
        inputActions.CharacterMove.Move.performed += OnMoveInput;
        inputActions.CharacterMove.Move.canceled += OnMoveInput;

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
        hp = maxHp;
        HpChange();
        //Debug.Log(hp);
        item.onChangeHp += OnUpgradeHp; // <<�κ�
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
    void Move()
    {
        V3 = new Vector3(0, mouseDelta, 0);      //���콺 z��ǥ �̵� y�� ȸ�� ������ ����
        mouseDelta = 0.0f;                       //�ʱ��� �۾�
        transform.Rotate(V3 * turnSpeed);        // �Ͻ��ǵ� �ӵ���ŭ ȸ��
        rigid.MovePosition(Time.fixedDeltaTime * moveSpeed * transform.TransformDirection(inputDir).normalized + transform.position);       // ����(���α��� �޿����Ʒ�) ���� �̵�ó��
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
            //Debug.Log(HP);
        }
    }


    void HpChange()
    {
        if (HP > 0)
        {
            StartCoroutine(Decrease());
            OnUpgradeHp(HP);
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
    }


    //--- ������ ��� �� �Լ�(left click)---
    private void OnAvtivity(InputAction.CallbackContext context)
    {
        isAction = true;
        StartCoroutine(ActionCoroutine());
    }

    private void OnAvtivityStop(InputAction.CallbackContext context)
    {
        isAction = false;
        StopCoroutine(ActionCoroutine());
    }

    //---���ݿ� �ڷ�ƾ---
   IEnumerator ActionCoroutine()
    {
        while (true)    //������ ���������� ���� ��� ���ϱ�
        {
            if (isAction == true)
            {
                anim.SetTrigger("Attack-trigger");
            }
                yield return new WaitForSeconds(2.5f);
        }
    }

    private void Test1(InputAction.CallbackContext obj)
    {
        //StartCoroutine(Fishing());
    }

    IEnumerator Fishing()
    {
        anim.SetBool("IsFishing", true);
        yield return new WaitForSeconds(5.0f);
        anim.SetBool("IsFishing", false);

    }
    //----------------------------------�׷��� �Լ�-------------------------------

    private void OnGrab(InputAction.CallbackContext context)
    {
        anim.SetBool("ItemGrab", !context.canceled);
    }

    //----------------------------------��� ��ȣ�ۿ� �Լ�-------------------------------

    private void OnMaking(InputAction.CallbackContext context)
    {
        int useHp = 50;                         // �ൿ�� ���� hp

        if (hp > useHp)
        {
            anim.SetTrigger("Making_Trigger");
            HP -= 50;
            //Debug.Log($"{hp} : ��� �߾��~~");
        }
    }
}
