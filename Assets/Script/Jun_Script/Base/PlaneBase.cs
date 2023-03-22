using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneBase : BasementObject
{
    Rigidbody rigid;
    CapsuleCollider capsule;
    //Renderer renderers;
    //Material mat;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();
        //renderers = GetComponent<Renderer>();
        //mat = renderers.material;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("TreeStart");

            HP--;
            Debug.Log($"First : {HP}");
            if (HP > 0)
            {
                GameObject obj = Instantiate(Effect);
                obj.transform.position = transform.position;
            }

            else if (HP <= 0)
            {
                GameObject obj = Instantiate(Meffect);
                obj.transform.position = transform.position;

                Destroy(gameObject);
                // 드랍아이템 생성

                // 오브젝트가 서서히 사라지는 코드 작성
                // mesg filter와 meshrenderer을 사용해서 알파값을 서서히 0으로 낮추는 코드 작성
                // 페이드인 , 페이드 아웃 사용

            }
        }
    }
}
