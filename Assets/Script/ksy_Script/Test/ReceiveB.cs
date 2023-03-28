using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveB : MonoBehaviour
{
    BroadCast broad;
    private void Awake()
    {
        broad = FindObjectOfType<BroadCast>();
        broad.BroadB += ReceiveBBroad;
        broad.BroadC += ReceiveCBroad;
    }

    void ReceiveBBroad()
    {
        Debug.Log("BroadB¹ÞÀ½");
    }
    void ReceiveCBroad(int ic, float fc)
    {
        Debug.Log($"Sub B : {ic} , {fc}");
    }
}
