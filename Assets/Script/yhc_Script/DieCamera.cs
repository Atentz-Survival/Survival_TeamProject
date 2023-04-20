using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieCamera : MonoBehaviour
{
    PlayerBase player;
    CinemachineVirtualCamera vCamera;
    CinemachineDollyCart dollyCart;

    public Action CameraEnd;

    private void Start()
    {
        player = FindObjectOfType<PlayerBase>();
        player.onDie += ProduceStart;

        vCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        vCamera.LookAt = player.transform;
        dollyCart = GetComponentInChildren<CinemachineDollyCart>();
    }

    private void ProduceStart()
    {
        transform.position = player.transform.position;
        vCamera.Priority = 100;
        dollyCart.m_Speed = 3;
    }
}
