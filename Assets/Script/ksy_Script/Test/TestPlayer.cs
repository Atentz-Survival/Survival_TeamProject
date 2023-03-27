using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Android;

public class TestPlayer : TestBase
{
    CharacterBase player;
    private void Start()
    {
        player = FindObjectOfType<CharacterBase>();
    }

    protected override void Test1(InputAction.CallbackContext _)
    {
        Transform Axe = player.transform.Find("Axe");
        if (Axe != null)
        {
            Axe.gameObject.SetActive(true);
        }
    }
}
