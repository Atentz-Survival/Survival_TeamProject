using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHand : MonoBehaviour
{
    PlayerBase player;
    public Action<int> UsingTool;
    public Collider rHandCollider;
    int useToolHp = -50;

    private void Start()
    {
        rHandCollider = GetComponent<Collider>();
    }

    private void Awake()
    {
        player = FindObjectOfType<PlayerBase>();
    }

    private void UsingRhand()
    {
            UsingTool?.Invoke(useToolHp);
            Debug.Log(useToolHp);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tree") || collision.gameObject.CompareTag("Ocean") || collision.gameObject.CompareTag("Rock") || collision.gameObject.CompareTag("Flower"))
        {
            UsingRhand();
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{

    //    if (other.gameObject.CompareTag("Tree") || other.gameObject.CompareTag("Ocean")|| other.gameObject.CompareTag("Rock") || other.gameObject.CompareTag("Flower"))
    //    {
    //        UsingRhand(useToolHp);
    //    }
    //}
}
