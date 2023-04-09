using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : BasementObject
{
    // public GameObject DropRock;

    Rigidbody rigid;
    SphereCollider sphere;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        sphere = GetComponent<SphereCollider>();
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

                if (collision.gameObject.name == "Pick")
                {
                    RockDrop1();
                }
                else if (collision.gameObject.name == "Cube")
                {
                    RockDrop2();
                }
                else if (collision.gameObject.name == "Axe")
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
}