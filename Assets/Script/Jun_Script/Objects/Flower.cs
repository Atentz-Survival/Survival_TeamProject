using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : PlaneBase
{
    private bool isFlower = false;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("FlowerStart");

        if (collision.gameObject.CompareTag("Reap"))
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

                isFlower = true;                 // 오브젝트 삭제
                if (isFlower)
                {
                    // Destroy(gameObject);
                    gameObject.SetActive(false);
                }

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

                // isFlower = true일 때 오브젝트가 삭제 되며 드랍아이템이 생성
                // 그 이후 isFlower = false가 되며 Sunshine의 vec의 x.rotation의 값이 0도가 되었을 때 , 리스폰
                isFlower= false;                 // 오브젝트 활성화를 위한 false
                
            }
        }
    }
}
