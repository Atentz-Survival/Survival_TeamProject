using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingBase : MonoBehaviour
{
    public Transform target;
    public GameObject fishingEffect;

    Vector3 dropPlace;

    private bool isTimeCheck = false;
    private float timer = 0f;

    private void Start()
    {
        if(target == null)
        {
            Player player = FindObjectOfType<Player>();
            target = player.transform;
        }
    }

    private void Update()
    {
        if(isTimeCheck)
        {
            timer += Time.deltaTime;
        }

        else
        {
            timer += Time.deltaTime * 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameObject obj = Instantiate(fishingEffect);
            obj.transform.position = transform.position;

            isTimeCheck = true;

            if(timer >= 10.0f)
            {
                // 낚시대를 빼는 애니메이션 적용필요

                GameObject fobj = ItemManager.Instance.GetObject(ItemType.Avocado);
                fobj.transform.position = obj.transform.position;

            }

            isTimeCheck= false;
        }
    }
}