using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : PlaneBase
{
    Action<int> FlowerHp;

    public Transform rotateObject;
    private bool isDisFlower = false;

    private void Start()
    {
        Sunshine.OnRespawn += RespawnF;
        FlowerHp += FlowerObject;
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

            float flower_Random = UnityEngine.Random.Range(0.0f, 1.0f);

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
                    if(flower_Random <= 0.75f)
                    {
                        FlowerDrop1();
                    }
                    else if(flower_Random<= 0.95f)
                    {
                        FlowerDrop2();
                    }
                    else
                    {
                        FlowerDrop3();
                    }
                }
                else if (collision.gameObject.transform.GetChild(1).gameObject.activeSelf == true)
                {
                    if(flower_Random <= 0.6f)
                    {
                        FlowerDrop1();
                    }
                    else if(flower_Random <= 0.85f)
                    {
                        FlowerDrop2();
                    }
                    else
                    {
                        FlowerDrop3();
                    }
                }
                else if (collision.gameObject.transform.GetChild(2).gameObject.activeSelf == true)
                {
                    if(flower_Random <= 0.45f)
                    {
                        FlowerDrop1();
                    }
                    else if(flower_Random <= 0.7f)
                    {
                        FlowerDrop2();
                    }
                    else
                    {
                        FlowerDrop3();
                    }
                }
                else
                {
                    Debug.Log("None");
                }

                objectHP = objectMaxHP;
                
            }
        }

        // 맨손 -----------------------------------------------------------------
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
                float a_Hand = 0.5f;
                float b_Hand = 0.8f;
                float c_Hand = 1.0f;
                if (Hand_Random <= a_Hand)
                {
                    Debug.Log("None");
                }
                else if (Hand_Random <= b_Hand)
                {
                    Hand_Drop_Flower1();
                }
                else if (Hand_Random <= c_Hand)
                {
                    Hand_Drop_Flower2();
                }
                //else if (Hand_Random <= c_Hand)
                //{
                //    // 플레이어의 체력 증가
                //    Hand_HpCare();
                //}
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
    void FlowerObject(int Hp)
    {
        Hp = objectHP;
    }
}
