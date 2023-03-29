using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    [Header("컴포넌트")]
    private Animator anim;
    private Rigidbody rigid;
    private ItemInventoryWindowExplanRoom item;

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
        //-----입력 Enable----
        inputActions.CharacterMove.Enable();
        inputActions.CharacterMove.MouseMove.performed += OnMouseMoveInput;
        inputActions.CharacterMove.Move.performed += OnMoveInput;
        inputActions.CharacterMove.Move.canceled += OnMoveInput;

        /*inputActions.CharacterMove.Activity.performed += OnAvtivity;
        inputActions.CharacterMove.Activity.canceled += OnAvtivityStop;

        inputActions.CharacterMove.Interaction_Item.performed += OnGrab;
        inputActions.CharacterMove.Interaction_Item.canceled += OnGrab;

        inputActions.CharacterMove.Interaction_Place.performed += OnMaking;*/

        //-----전달 받을 델리게이트----
        item.onChangeHp += OnUpgradeHp;
    }
    private void OnDisable()
    {
        //-----입력 Disable----
        inputActions.CharacterMove.MouseMove.performed -= OnMouseMoveInput;
        inputActions.CharacterMove.Move.canceled -= OnMoveInput;
        inputActions.CharacterMove.Move.performed -= OnMoveInput;

        /*inputActions.CharacterMove.Activity.performed -= OnAvtivity;
        inputActions.CharacterMove.Activity.canceled -= OnAvtivityStop;

        inputActions.CharacterMove.Interaction_Item.performed -= OnGrab;
        inputActions.CharacterMove.Interaction_Item.canceled -= OnGrab;

        inputActions.CharacterMove.Interaction_Place.performed -= OnMaking;*/

        inputActions.CharacterMove.Disable();
    }
    private void Start()
    {
        item = FindObjectOfType<ItemInventoryWindowExplanRoom>();
        hp = maxHp;
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
        if (hp > 0 )
        {
            hp = hp + getHp;
            if(hp > maxHp)
            {
                hp = maxHp;
            }
            Debug.Log(hp);
        }
    }
}
