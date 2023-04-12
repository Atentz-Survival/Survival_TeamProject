using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.ProBuilder;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class FishingBase : MonoBehaviour
{
    // 이펙트
    public GameObject fishingEffect;        // 물에 닿았을 때의 이펙트

    public Transform target;                // 타겟(종점 좌표)

    //public float flightTime = 2.0f;         // 체공 시간
    //public float speedRate = 1.0f;          // 허공 시간을 기준으로 한 이동속도의 배율
    //private const float gravity = -9.8f;    // 중력

    private Vector3 endPos;                 // 타겟의 위치
    private Vector3 startPos;               // 생성 위치

    // 낚시할때의 대기시간
    public float waitT = 5.0f;              // 낚시후의 딜레이 시간

    Rigidbody rigid;

    private GameObject desTarget;


    private bool isTimeCheck = false;

    private void Start()
    {
        endPos = target.transform.position;
        if (target == null)
        {
            Player player = FindObjectOfType<Player>();
            target = player.transform;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FisingRod"))
        {
            GameObject obj = Instantiate(fishingEffect);
            obj.transform.position = target.position;

            isTimeCheck = false;

            if (isTimeCheck == false)
            {
                Debug.Log("Step1");

                if (other.gameObject.name == "FishingRod")
                {
                    Invoke("FishDrop1", waitT);
                    Debug.Log("Step3");
                }

                else if (other.gameObject.name == "Cube")
                {
                    FishDrop2();
                    Debug.Log("Step4");
                }

                else if (other.gameObject.name == "te")
                {
                    FishDrop3();
                    Debug.Log("Step5");
                }
                StopAllCoroutines();
            }
            isTimeCheck = true;
            Debug.Log("Step2");
        }
    }

    public void FishDrop1()
    {
        Debug.Log("Alpha");
        GameObject fobj = ItemManager.Instance.GetObject(ItemType.Galchi);
        fobj.transform.position = transform.position;
        desTarget = fobj;

        startPos = fobj.transform.position;
        Vector3 disVec = (endPos - startPos) + new Vector3(0 , 50 , 0) + Vector3.right;
        rigid = fobj.GetComponent<Rigidbody>();
        rigid.AddForce(disVec* 10);

        Invoke("FishFalse", 6.0f);
        Debug.Log("Step3-1");
    }

    private void FishDrop2()
    {
        GameObject fobj = ItemManager.Instance.GetObject(ItemType.Gazami);
        fobj.transform.position = transform.position;
        desTarget = fobj;

        startPos = fobj.transform.position;
        Vector3 disVec = (endPos - startPos) + new Vector3(0, 50, 0) + Vector3.right;
        rigid = fobj.GetComponent<Rigidbody>();
        rigid.AddForce(disVec * 10);

        Invoke("FishFalse", 6.0f);
        Debug.Log("Step3-2");
    }

    private void FishDrop3()
    {
        GameObject fobj = ItemManager.Instance.GetObject(ItemType.Shark);
        fobj.transform.position = transform.position;
        desTarget = fobj;

        startPos = fobj.transform.position;
        Vector3 disVec = (endPos - startPos) + new Vector3(0, 50, 0) + Vector3.right;
        rigid = fobj.GetComponent<Rigidbody>();
        rigid.AddForce(disVec * 10);

        Invoke("FishFalse", 6.0f);
        Debug.Log("Step3-3");
    }

    private void FishFalse()
    {
        desTarget.SetActive(false);
    }
}