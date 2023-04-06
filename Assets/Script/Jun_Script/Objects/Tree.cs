using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Tree : PlaneBase
{
    public GameObject rotateObject;
    private bool isTree = false;

    private void Start()
    {
        Sunshine.OnRespawn += Respawn;      // Onrespawn의 낮밤이 실행될때 respawn실행

    }
    private void Update()
    {
        
    }

    private void Respawn()
    {
        if (rotateObject.transform.rotation.x >= 0.0f)      // 이거를 update로 올리고 범위를 0.0 ~ 0.01정도로 맞추면 되지않을까....?
        {
            isTree = false;
            if (isTree == false)
            {
                Debug.Log(gameObject);
                gameObject.SetActive(true);
            }

            else
            {
                Debug.Log("ERROR");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Axe"))
        {
            Debug.Log("TreeStart");

            if (collision.gameObject.CompareTag("Axe"))
            {

                HP--;
                Debug.Log($"First : {HP}");
                if (HP > 0)
                {
                    GameObject obj = Instantiate(Effect);
                    obj.transform.position = transform.position;
                }

                else if (HP <= 0)
                {
                    GameObject obj = Instantiate(Meffect);
                    obj.transform.position = transform.position;

                    isTree = true;
                    gameObject.SetActive(false);

                    if (collision.gameObject.name == "axe")
                    {
                        TreeDrop1();
                    }
                    else if (collision.gameObject.name == "Cube")
                    {
                        TreeDrop2();
                    }
                    else if (collision.gameObject.name == "Axe")
                    {
                        TreeDrop3();
                    }
                    else
                    {
                        Debug.Log("None");
                    }
                }
            }
        }
    }
}
