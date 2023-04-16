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


    void RockObject(int Hp)
    {
        Hp = objectHP;
    }
}
