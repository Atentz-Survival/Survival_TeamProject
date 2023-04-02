using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : PlayerUI
{

    private void Awake()
    {
        gameObject.SetActive(false);
    }
}
