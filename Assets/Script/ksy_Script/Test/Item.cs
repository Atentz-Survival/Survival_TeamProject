using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Android;

public class Item : MonoBehaviour
{
    Player01 aplayer;

    private int wood = 0;

    /*public Action SuccessMake;
    public Action<int> FailMake;*/

    private void Awake()
    {
        
        aplayer = FindObjectOfType<Player01>();
    }

    private void OnEnable()
    {
        aplayer.GetItem += AddItem;
    }

    void AddItem(int a)
    {
        wood += a;
        Debug.Log(wood);
    }
}
