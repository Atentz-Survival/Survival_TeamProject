using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BasementObject : MonoBehaviour
{
    // 돌 , 꽃 , 나무가 공통으로 가지는 Base
    // 체력 , effect

    public int maxHP = 3;

    public int HP = 3;

    public GameObject Effect;   // 치집 , 채굴시의 이펙트
    public GameObject Meffect;

    //static public bool GetRandomItem(float percentage)
    //{
    //    if (Random.value < percentage)
    //    {
    //        return true;
    //    }
    //    else
    //        return false;
    //}


    //       1단1  1단2   1단3   2단1   2단2   2단3   3단1   3단2   3단3
    // 낫1 : 0.15  0.60   0.15   0.05
    // 낫2 : 0.50  0.80   0.50   0.15   0.30   0.15
    // 낫3 : 0.85  1.00   0.85   0.50   0.80   0.50
    // 곡1 : 0.15  0.60   0.15   0.05
    // 곡2 : 0.50  0.80   0.50   0.15   0.60   0.40   0.80   0.40
    // 곡3 : 0.85  1.00   0.85   0.50   0.80   0.80   0.20   0.80   0.40
    // 도1 : 0.15  0.60   0.15   0.05
    // 도2 : 0.50  0.80   0.50   0.15   0.30   0.15   0.85   0.15
    // 도3 : 0.85  1.00   0.85   0.50   0.80   0.50   0.00   0.50   0.50

    // 낫으로 파밍시의 오브젝트 생성
    // 아이템 확률은 player의 무기쪽에서 다룰것. 확률을 다루게 되면 여기서 오브젝트를 생성

    public void FlowerDrop1()
    {
        float a_Random = Random.Range(0.0f, 1.0f);
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

            new WaitForSeconds(2);
            Destroy(gameObject);
        }
    }

    public void FlowerDrop2()
    {
        float b_Random = Random.Range(0.0f, 1.0f);

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

            Destroy(gameObject , 4.0f);
        }
    }

    public void FlowerDrop3()
    {
        float c_Random = Random.Range(0.0f, 1.0f);

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

            Destroy(gameObject , 4.0f);
        }
    }


    public void TreeDrop1()
    {
        float a_Random = Random.Range(0.0f, 1.0f);
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
            GameObject obj = ItemManager.Instance.GetObject(ItemType.Null); // Tomato 게임오브젝트를 ItemManager에서 가져와 활성화
            obj.transform.position = transform.position;

            Destroy(gameObject, 4.0f);
        }
    }

    public void TreeDrop2()
    {
        float b_Random = Random.Range(0.0f, 1.0f);

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
            GameObject obj = ItemManager.Instance.GetObject(ItemType.Null); // Tomato 게임오브젝트를 ItemManager에서 가져와 활성화
            obj.transform.position = transform.position;

            Destroy(gameObject, 4.0f);
        }
    }

    public void TreeDrop3()
    {
        float c_Random = Random.Range(0.0f, 1.0f);

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
            GameObject obj = ItemManager.Instance.GetObject(ItemType.Null); // Tomato 게임오브젝트를 ItemManager에서 가져와 활성화
            obj.transform.position = transform.position;

            Destroy(gameObject, 4.0f);
        }
    }

    public void RockDrop1()
    {
        float a_Random = Random.Range(0.0f, 1.0f);
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

            Destroy(obj , 4.0f);
        }
    }

    public void RockDrop2()
    {
        float b_Random = Random.Range(0.0f, 1.0f);

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
            obj.transform.position = Vector3.up * 9;

            new WaitForSeconds(2);
            Destroy(obj , 4.0f);
        }
    }

    public void RockDrop3()
    {
        float c_Random = Random.Range(0.0f, 1.0f);

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
            obj.transform.position = Vector3.up * 9;

            new WaitForSeconds(2);
            Destroy(obj , 4.0f);
        }
    }


    // 이걸 한곳에 다하면 너무 복잡함. 따라서 각각의 스크립트안에 랜덤 함수를 이용해 넣을 필요가있을듯....

    // 리스폰
    // 만약에 원래좌표에 오브젝트가 없는 경우 밤이지나고 리스폰하는 코드

}
