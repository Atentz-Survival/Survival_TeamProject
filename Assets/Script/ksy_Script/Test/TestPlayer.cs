using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Android;

public class TestPlayer : TestBase
{
    public Action<int> A;

    protected override void Test1(InputAction.CallbackContext _)
    {
        A(20);
    }
}
