using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UIElements;

public class Tree : PlaneBase
{
    public Transform rotateObject;
    private bool isDisTree = false;     // 나무가 사라졌는지 확인하기위한 변수

    private void Start()
    {
        Sunshine.OnRespawn += Respawn;      // Onrespawn의 낮밤이 실행될때 respawn실행(sunshine에서 update에 넣었기 때문에 Update를 우회하여 실행)
    }

    private void Respawn()
    {
        // Debug.Log(gameObject);
        if (isDisTree)
        {
            Quaternion qua = rotateObject.rotation;
            // Debug.Log(qua);
            if ((qua.x >= -0.001f && qua.x <= 0.0f) || (qua.x <= 0.001f && qua.x >= 0.0f))
            {
                // 쿼터니안의 범위는 0~ 1.0 ~ 0 ~ -1.0 ~ 0 의 2*sin x 의 그래프와같은 형태이므로 아침이오기전의 값인 -0.001f 와 0.001값부터 아침이되는 0사이에 오브젝트 활성화
                gameObject.SetActive(true);
                Debug.Log("TreeRespawn");
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Axe"))
        {
            objectHP--;
            Debug.Log($"First : {objectHP}");
            if (objectHP > 0)
            {
                Debug.Log("TreeStart");
                GameObject obj = Instantiate(Effect);
                obj.transform.position = transform.position;
            }

            else if (objectHP == 0)
            {
                Debug.Log($"Second : {objectHP}");
                GameObject obj = Instantiate(Meffect);
                obj.transform.position = transform.position;

                gameObject.SetActive(false);
                isDisTree = true;
                Debug.Log("do");

                if (collision.gameObject.transform.GetChild(0).gameObject.activeSelf == true)
                {
                    TreeDrop1();
                }
                else if (collision.gameObject.transform.GetChild(1).gameObject.activeSelf == true)
                {
                    TreeDrop2();
                }
                else if (collision.gameObject.transform.GetChild(2).gameObject.activeSelf == true)
                {
                    TreeDrop3();
                }
                else
                {
                    Debug.Log("None");
                }
                objectHP = objectMaxHP;                         // 체력이0이되면서 아이템이 생성되며 오브젝트의 체력을 max체력으로 돌려준다.
            }
        }
    }
}
