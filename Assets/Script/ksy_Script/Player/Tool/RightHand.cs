using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHand : MonoBehaviour
{
    public Action<int> UsingTool;
    public Collider rHandCollider;
    int useToolHp;

    private void Start()
    {
        rHandCollider = GetComponent<Collider>();
    }

    private int UsingRhand(int hp)
    {
        hp = -50;
        UsingTool?.Invoke(hp);
        Debug.Log(hp);
        return hp;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Tree") || other.gameObject.CompareTag("Ocean")|| other.gameObject.CompareTag("Rock") || other.gameObject.CompareTag("Flower"))
        {
            UsingRhand(useToolHp);
        }
    }
}
