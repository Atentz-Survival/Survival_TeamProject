using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : PlaneBase
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("FlowerStart");

        if (collision.gameObject.CompareTag("Player"))
        {

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

                if (collision.gameObject.name == "Sphere")      // 다른씬에서 사용시 이곳을 axe와같은 무기 오브젝트의 이름으로 설정하면 된다.
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
                // 드랍아이템 생성

                // 오브젝트가 서서히 사라지는 코드 작성
                // mesg filter와 meshrenderer을 사용해서 알파값을 서서히 0으로 낮추는 코드 작성
                // 페이드인 , 페이드 아웃 사용

            }
        }
    }
}
