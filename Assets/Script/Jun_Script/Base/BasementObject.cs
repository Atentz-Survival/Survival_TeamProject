using System;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BasementObject : MonoBehaviour
{
    // 돌 , 꽃 , 나무가 공통으로 가지는 Base
    // 체력 , effect
    public int objectMaxHP = 3;
    public int objectHP = 3;

    int ObjectHP
    {
        get => objectHP;
        set
        {
            if(objectHP != value)
            {
                objectHP = value;
                playerHpDel?.Invoke(objectHP);
            }
        }
    }

    public Action<int> playerHpDel;


    public int handObjectMaxHp = 5;
    public int handObjectHp = 5;

    public GameObject Effect;   // 치집 , 채굴시의 이펙트
    public GameObject Meffect;

    public float delayTime = 4.0f;

    private GameObject target;

    private void Start()
    {
        
    }

    public void FlowerDrop1()
    {
        float a_Random = UnityEngine.Random.Range(0.0f, 1.0f);
        float a = 0.90f;

        if (a_Random <= a)
        {
            objectInit1();
        }
        else
        {
            Debug.Log("None1");
        }

        // FishingRod

        void objectInit1()
        {
            GameObject obj = ItemManager.Instance.GetObject(ItemType.Strawberry); // Tomato 게임오브젝트를 ItemManager에서 가져와 활성화
            obj.transform.position = transform.position;
            target = obj;

            Invoke("TimeCount", 4.0f);
        }
    }

    public void FlowerDrop2()
    {
        float b_Random = UnityEngine.Random.Range(0.0f, 1.0f);

        float b = 0.90f;

        if (b_Random <= b)
        {
            objectInit2();
        }
        else
        {
            Debug.Log("None2");
        }

        void objectInit2()
        {
            GameObject obj = ItemManager.Instance.GetObject(ItemType.Avocado); // Tomato 게임오브젝트를 ItemManager에서 가져와 활성화
            obj.transform.position = transform.position;
            target = obj;

            Invoke("TimeCount", 4.0f);
        }
    }

    public void FlowerDrop3()
    {
        float c_Random = UnityEngine.Random.Range(0.0f, 1.0f);

        float c = 0.90f;

        if (c_Random <= c)
        {
            objectInit3();
        }
        else
        {
            Debug.Log("None3");
        }

        void objectInit3()
        {
            GameObject obj = ItemManager.Instance.GetObject(ItemType.Peanut); // Tomato 게임오브젝트를 ItemManager에서 가져와 활성화
            obj.transform.position = transform.position;
            target = obj;

            Invoke("TimeCount", 4.0f);
        }
    }


    public void TreeDrop1()
    {
        float a_Random = UnityEngine.Random.Range(0.0f, 1.0f);
        float a = 0.90f;

        if (a_Random <= a)
        {
            Debug.Log("Step1");
            objectInit1();
        }
        else
        {
            Debug.Log("None1");
        }
    }

    public void objectInit1()
    {
        GameObject obj = ItemManager.Instance.GetObject(ItemType.Firewood); // Tomato 게임오브젝트를 ItemManager에서 가져와 활성화
        obj.transform.position = transform.position;
        target = obj;
        Debug.Log("Time1");

        Invoke("TimeCount", 4.0f);
    }


    public void TreeDrop2()
    {
        float b_Random = UnityEngine.Random.Range(0.0f, 1.0f);

        float b = 0.90f;

        if (b_Random <= b)
        {
            objectInit2();
        }
        else
        {
            Debug.Log("None2");
        }

        void objectInit2()
        {
            GameObject obj = ItemManager.Instance.GetObject(ItemType.FirewoodX3); // Tomato 게임오브젝트를 ItemManager에서 가져와 활성화
            obj.transform.position = transform.position;
            target = obj;

            Invoke("TimeCount", 4.0f);
        }
    }

    public void TreeDrop3()
    {
        float c_Random = UnityEngine.Random.Range(0.0f, 1.0f);

        float c = 0.90f;

        if (c_Random <= c)
        {
            objectInit3();
        }
        else
        {
            Debug.Log("None3");
        }

        void objectInit3()
        {
            GameObject obj = ItemManager.Instance.GetObject(ItemType.FirewoodX5); // Tomato 게임오브젝트를 ItemManager에서 가져와 활성화
            obj.transform.position = transform.position;
            target = obj;

            Invoke("TimeCount", 4.0f);
        }
    }

    public void RockDrop1()
    {
        float a_Random = UnityEngine.Random.Range(0.0f, 1.0f);
        float a = 0.90f;

        if (a_Random <= a)
        {
            objectInit1();
        }
        else
        {
            Debug.Log("None1");
        }

        // FishingRod

        void objectInit1()
        {
            GameObject obj = ItemManager.Instance.GetObject(ItemType.Stone); // Tomato 게임오브젝트를 ItemManager에서 가져와 활성화
            obj.transform.position = transform.position + Vector3.up;
            target = obj;

            Invoke("TimeCount", 4.0f);
        }
    }

    public void RockDrop2()
    {
        float b_Random = UnityEngine.Random.Range(0.0f, 1.0f);

        float b = 0.90f;

        if (b_Random <= b)
        {
            objectInit2();
        }
        else
        {
            Debug.Log("None2");
        }

        void objectInit2()
        {
            GameObject obj = ItemManager.Instance.GetObject(ItemType.Iron); // Tomato 게임오브젝트를 ItemManager에서 가져와 활성화
            obj.transform.position = transform.position + Vector3.up;
            target = obj;

            Invoke("TimeCount", 4.0f);
        }
    }

    public void RockDrop3()
    {
        float c_Random = UnityEngine.Random.Range(0.0f, 1.0f);

        float c = 0.90f;

        if (c_Random <= c)
        {
            objectInit3();
        }
        else
        {
            Debug.Log("None3");
        }

        void objectInit3()
        {
            GameObject obj = ItemManager.Instance.GetObject(ItemType.Gold); // Tomato 게임오브젝트를 ItemManager에서 가져와 활성화
            obj.transform.position = transform.position + Vector3.up;
            target = obj;

            Invoke("TimeCount", 4.0f);
        }
    }

    private void TimeCount()
    {
        Debug.Log(target);
        target.SetActive(false);
    }

    public void Hand_Drop1()
    {
        GameObject obj = ItemManager.Instance.GetObject(ItemType.Gold);
        obj.transform.position = transform.position + Vector3.up;
        target = obj;

        Invoke("TimeCount", 4.0f);
    }
    public void Hand_Drop2()
    {
        GameObject obj = ItemManager.Instance.GetObject(ItemType.FirewoodX5);
        obj.transform.position = transform.position + Vector3.up;
        target = obj;

        Invoke("TimeCount", 4.0f);
    }
    public void Hand_Drop3()
    {
        GameObject obj = ItemManager.Instance.GetObject(ItemType.Peanut);
        obj.transform.position = transform.position + Vector3.up;
        target = obj;

        Invoke("TimeCount", 4.0f);
    }

    public void Hand_HpCare()
    {
        
    }
}
