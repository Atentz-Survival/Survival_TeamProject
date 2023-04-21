using System;
using UnityEngine;

public class Flower22 : PlaneBase
{
    int giveHp = 0;

    public Action<int> FlowerHp;

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

                if (collision.gameObject.transform.GetChild(0).gameObject.activeSelf == true)
                {
                    FlowerDrop1();
                }
                else if (collision.gameObject.transform.GetChild(1).gameObject.activeSelf == true)
                {
                    FlowerDrop2();
                }
                else if (collision.gameObject.transform.GetChild(2).gameObject.activeSelf == true)
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
                isDisFlower = true;
                if (Hand_Random < 0.25f)
                {
                    Hand_Drop_Flower1();
                }
                else if (Hand_Random < 0.5f)
                {
                    Hand_Drop_Flower2();
                }
                else if (Hand_Random < 0.75)
                {
                    Hand_Drop_Flower3();
                }
                else
                {
                    float rand = UnityEngine.Random.Range(0.0f, 1.0f);
                    giveHp = 0;
                    if (rand < 0.25f)
                    {
                        giveHp = 250;
                    }
                    else if (rand < 0.5f)
                    {
                        giveHp = 350;
                    }
                    else if (rand < 0.75)
                    {
                        giveHp = 450;
                    }
                    else
                    {
                        giveHp = 500;
                    }
                    FlowerHp?.Invoke(giveHp);
                }
                handObjectHp = handObjectMaxHp;
            }
        }
        else
        {
            Debug.Log("None");
        }
    }
}