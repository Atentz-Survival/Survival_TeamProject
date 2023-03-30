using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Android;

public class TestPlayer : TestBase
{
    PlayerBase player;
    private void Start()
    {
        player = FindObjectOfType<PlayerBase>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Test1(InputAction.CallbackContext _)
    {
        int a = player.HP;
        Debug.Log(a);
    }
}
