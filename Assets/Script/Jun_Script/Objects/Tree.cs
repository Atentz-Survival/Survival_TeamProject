using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Tree : PlaneBase
{
    private void OnCollisionEnter(Collision collision)
    {
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
                    TreeDrop1();
                }
                else if (collision.gameObject.name == "Cube")
                {
                    TreeDrop2();
                }
                else if (collision.gameObject.name == "Axe")
                {
                    TreeDrop3();
                }
                else
                {
                    Debug.Log("None");
                }
            }
        }
    }
}
