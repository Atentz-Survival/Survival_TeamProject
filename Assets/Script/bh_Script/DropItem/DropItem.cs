using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : PoolObjectShape
{
    // Start is called before the first frame update
    public float lifeTime = 10.0f;
    void OnEnable() 
    {
        StopAllCoroutines();
        if (gameObject.activeSelf == true) 
        {
            StartCoroutine(LifeOver(lifeTime));
        }
    }
}
