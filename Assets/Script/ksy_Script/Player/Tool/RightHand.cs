using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.ProBuilder;

public class RightHand : MonoBehaviour
{
    PlayerBase player;
    public Action<int> UsingTool;
    public Collider rHandCollider;
    int useToolHp = -50;
    public int ToolHP                      // 현재 hp 프로퍼티 > ui
    {
        get => useToolHp;
        set
        {
            
            useToolHp = value;

            UsingTool?.Invoke(useToolHp);
        }
    }

    private Tree tree;
    private Rock rock;
    private Flower flower;

    private void Start()
    {
        rHandCollider = GetComponent<Collider>();
        flower = FindObjectOfType<Flower>();
        rock = FindObjectOfType<Rock>();
        tree = FindObjectOfType<Tree>();
        flower.FlowerHp += OnUpgradeObjectHp;
        tree.TreeHp += OnUpgradeObjectHp;
        rock.RockHp += OnUpgradeObjectHp;

    }


    private void Awake()
    {
        player = FindObjectOfType<PlayerBase>();
    }

    private void UsingRhand()
    {
        ToolHP = ToolHP;
    }
    private void OnUpgradeObjectHp(int obj)
    {
        ToolHP = ToolHP + obj;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tree") || collision.gameObject.CompareTag("Ocean") || collision.gameObject.CompareTag("Rock") || collision.gameObject.CompareTag("Flower"))
        {
            UsingRhand();
        }
    }
}
