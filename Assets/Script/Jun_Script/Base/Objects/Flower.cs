using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : PlaneBase
{
    public Transform rotateObject;
    private bool isDisFlower = false;

    private void Start()
    {
        Sunshine.OnRespawn += RespawnF;
    }

    private void RespawnF()
    {
        if (isDisFlower)
        {
            Quaternion qua = rotateObject.rotation;
            if ((qua.x >= -0.001f && qua.x <= 0.0f) || (qua.x <= 0.001f && qua.x >= 0.0f))
            {
                gameObject.SetActive(true);
                Debug.Log("FlowerRespawn");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Reap"))
        {
            objectHP--;
            Debug.Log($"First : {objectHP}");
            if (objectHP > 0)
            {
                GameObject obj = Instantiate(Effect);
                obj.transform.position = transform.position;
            }

            else if (objectHP == 0)
            {
                Debug.Log($"Second : {objectHP}");
                GameObject obj = Instantiate(Meffect);
                obj.transform.position = transform.position;

                gameObject.SetActive(false);

                isDisFlower = true;

                if (collision.gameObject.name == "Reap")
                {
                    FlowerDrop1();
                }
                else if (collision.gameObject.name == "Cube")
                {
                    FlowerDrop2();
                }
                else if (collision.gameObject.name == "Axe")
                {
                    FlowerDrop3();
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
