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
    public Action<int> TreeHp;

    public Transform rotateObject;
    private bool isDisTree = false;     // 나무가 사라졌는지 확인하기위한 변수

    private void Start()
    {
        Sunshine.OnRespawn += Respawn;      // Onrespawn의 낮밤이 실행될때 respawn실행(sunshine에서 update에 넣었기 때문에 Update를 우회하여 실행)
        TreeHp += TreeObject;
        
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
            Debug.Log("AXE");
            objectHP--;
            Debug.Log($"First : {objectHP}");

            float tree_Random = UnityEngine.Random.Range(0.0f, 1.0f);

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
                    if(tree_Random <= 0.75f)
                    {
                        TreeDrop1();
                    }
                    else if(tree_Random <= 0.95f)
                    {
                        TreeDrop2();
                    }
                    else
                    {
                        TreeDrop3();
                    }
                }
                else if (collision.gameObject.transform.GetChild(1).gameObject.activeSelf == true)
                {
                    if(tree_Random <= 0.6f)
                    {
                        TreeDrop1();
                    }
                    else if(tree_Random <= 0.85f)
                    {

                        TreeDrop2();
                    }
                    else
                    {
                        TreeDrop3();
                    }
                }
                else if (collision.gameObject.transform.GetChild(2).gameObject.activeSelf == true)
                {
                    if(tree_Random <= 0.45f)
                    {
                        TreeDrop1();
                    }
                    else if(tree_Random <= 0.7f)
                    {
                        TreeDrop2();
                    }
                    else
                    {
                        TreeDrop3();
                    }
                }
                else
                {
                    Debug.Log("None");
                }
                objectHP = objectMaxHP;                         // 체력이0이되면서 아이템이 생성되며 오브젝트의 체력을 max체력으로 돌려준다.
            }
        }

        else if (collision.gameObject.CompareTag("RightHand"))
        {
            Debug.Log("RightHand");
            handObjectHp--;

            Debug.Log(handObjectHp);

            float Hand_Random = UnityEngine.Random.Range(0.0f, 1.0f);
            Debug.Log(Hand_Random);

            if (handObjectHp > 0)
            {
                GameObject obj = Instantiate(Effect);
                obj.transform.position = transform.position;
            }
            else if (handObjectHp <= 0)
            {
                GameObject obj = Instantiate(Meffect);
                obj.transform.position = transform.position;

                gameObject.SetActive(false);
                isDisTree = true;
                float a_Hand = 0.50f;
                float b_Hand = 0.80f;
                float c_Hand = 1.00f;
                // float d_Hand = 0.00f;
                if (Hand_Random <= a_Hand)
                {
                    Debug.Log("None");
                }
                else if (Hand_Random <= b_Hand)
                {
                    Hand_Drop_Tree1();
                }
                else if (Hand_Random <= c_Hand)
                {
                    Hand_Drop_Tree2();
                }
                //else if (Hand_Random <= d_Hand)
                //{
                //    // 플레이어의 체력 증가
                //    Hand_Drop_Tree3();
                //}
                else
                {
                    Debug.Log("ERROR");
                }
                handObjectHp = handObjectMaxHp;
            }
        }
    }
    void TreeObject(int Hp)
    {
        Hp = objectHP;
    }
}
