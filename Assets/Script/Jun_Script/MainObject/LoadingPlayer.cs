using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingPlayer : MonoBehaviour
{
    private void Awake()
    {
        var obj = FindObjectsOfType<LoadingPlayer>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
