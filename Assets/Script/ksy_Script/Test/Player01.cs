using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Android;

public class Player01 : TestBase
{
    Item item;
    public Action<int> GetItem;
    /*public Action MakeFurnitual;*/

    private void Awake()
    {
      item = FindObjectOfType<Item>();
    }

    protected override void Test1(InputAction.CallbackContext _)
    {
        GetItem?.Invoke(1);

    }

    /*protected override void Test2(InputAction.CallbackContext _)
    {
        MakeFurnitual();
    }*/
}
