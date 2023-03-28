using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BroadCast : TestBase
{
    public Action BroadA;
    public Action BroadB;
    public Action<int,float> BroadC;

    protected override void Test1(InputAction.CallbackContext _)
    {
        BroadA();
    }

    protected override void Test2(InputAction.CallbackContext _)
    {
        BroadB();
    }

    protected override void Test3(InputAction.CallbackContext _)
    {
        BroadC(10, 5.5f);
    }

}