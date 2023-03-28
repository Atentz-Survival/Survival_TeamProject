using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveA : MonoBehaviour
{
    BroadCast broad;
    private void Awake()
    {
        broad = FindObjectOfType<BroadCast>();
        broad.BroadA += ReceiveABroad;
        broad.BroadC += ReceiveCBroad;
    }

    void ReceiveABroad()
    {
        Debug.Log("BroadA¹ÞÀ½");
    }
    void ReceiveCBroad(int ic,float fc)
    {
        Debug.Log($"Sub A : {ic} , {fc}");
    }
}
