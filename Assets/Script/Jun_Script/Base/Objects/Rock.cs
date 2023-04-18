using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : BasementObject
{
    // public GameObject DropRock;

    public Action<int> RockHp;

    Rigidbody rigid;
    SphereCollider sphere;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        sphere = GetComponent<SphereCollider>();
    }
    private void Start()
    {
        RockHp += RockObject;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pick"))
        {
            Debug.Log("GetStarted");

            // 횟수로 한다.
            objectHP--;

            if(objectHP > 0)
            {
                Debug.Log(objectHP);
                GameObject obj = Instantiate(Effect);
                obj.transform.position = transform.position;
            }

            else if(objectHP <= 0)
            {
                Debug.Log(objectHP);
                GameObject obj = Instantiate(Meffect);
                obj.transform.position = transform.position;

                // 드랍 아이템 생성

                if (collision.gameObject.transform.GetChild(0).gameObject.activeSelf == true)
                {
                    RockDrop1();
                }
                else if (collision.gameObject.transform.GetChild(1).gameObject.activeSelf == true)
                {
                    RockDrop2();
                }
                else if (collision.gameObject.transform.GetChild(2).gameObject.activeSelf == true)
                {
                    RockDrop3();
                }
                else
                {
                    Debug.Log("None");
                }

                objectHP = objectMaxHP;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RightHand"))
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

                float a_Hand = 0.25f;
                float b_Hand = 0.50f;
                float c_Hand = 0.75f;
                float d_Hand = 1.00f;
                if (Hand_Random <= a_Hand)
                {
                    Hand_Drop1();
                }
                else if (Hand_Random <= b_Hand && Hand_Random > a_Hand)
                {
                    Hand_Drop2();
                }
                else if (Hand_Random <= c_Hand && Hand_Random > b_Hand)
                {
                    Hand_Drop3();
                }
                else if (Hand_Random <= d_Hand && Hand_Random > c_Hand)
                {
                    // 플레이어의 체력 증가
                }
                else
                {
                    Debug.Log("ERROR");
                }
                handObjectHp = handObjectMaxHp;
            }
        }
        else
        {
            Debug.Log("None");
        }
    }


    void RockObject(int Hp)
    {
        Hp = objectHP;
    }
}
