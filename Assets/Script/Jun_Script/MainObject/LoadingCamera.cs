using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingCamera : MonoBehaviour
{
    private void Awake()
    {
        var obj = FindObjectsOfType<LoadingCamera>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
