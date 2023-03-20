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

                HP = maxHP;
            }
        }
    }
}
