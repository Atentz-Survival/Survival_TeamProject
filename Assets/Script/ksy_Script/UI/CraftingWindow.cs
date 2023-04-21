using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
=======
using UnityEngine.InputSystem;
>>>>>>> 08ddd8247ee228ecd9df1dbaeeb2ad34a9dc1cd8

public class CraftingWindow : MonoBehaviour
{
    PlayerBase player;
<<<<<<< HEAD
    private void Start()
    {
        player = FindObjectOfType<PlayerBase>();
        player.onMaking += OpenCraftingWindow;
    }
    private void OnEnable()
    {
    }
=======

    public Action DontAction;
    public Action DoAction;
    private void Start()
    {
        player = FindObjectOfType<PlayerBase>();
        gameObject.SetActive(false);
        player.onMaking += OpenCraftingWindow;
    }
>>>>>>> 08ddd8247ee228ecd9df1dbaeeb2ad34a9dc1cd8

    private void OpenCraftingWindow()
    {
       if (gameObject.activeSelf == true)
       {
           gameObject.SetActive(false);
<<<<<<< HEAD
=======
            DoAction.Invoke();

>>>>>>> 08ddd8247ee228ecd9df1dbaeeb2ad34a9dc1cd8
       }
       else
       {
           gameObject.SetActive(true);
<<<<<<< HEAD
=======
            DontAction?.Invoke();
>>>>>>> 08ddd8247ee228ecd9df1dbaeeb2ad34a9dc1cd8
       }
    }
}
