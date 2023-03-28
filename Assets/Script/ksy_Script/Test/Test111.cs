using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class Test111 : TestBase
{
    public int k = 0;
    public TestPlayer tplayer;
    private void Awake()
    {
        tplayer = GameObject.FindObjectOfType<TestPlayer>();
        tplayer.A += PlayA;
        
    }

    void PlayA(int x)
    {
        k=k+x;
        Debug.Log(k);
        Debug.Log(k);
    }
}
