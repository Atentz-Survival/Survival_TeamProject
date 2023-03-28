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
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("GetStarted");

            // 횟수로 한다.
            HP--;

            if(HP > 0)
            {
                Debug.Log(HP);
                GameObject obj = Instantiate(Effect);
                obj.transform.position = transform.position;
            }

            else if(HP <= 0)
            {
                Debug.Log(HP);
                GameObject obj = Instantiate(Meffect);
                obj.transform.position = transform.position;

                // 드랍 아이템 생성

                if (collision.gameObject.name == "Sphere")      // 다른씬에서 사용시 이곳을 axe와같은 무기 오브젝트의 이름으로 설정하면 된다.
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

                HP = maxHP;
            }
        }
    }
}
