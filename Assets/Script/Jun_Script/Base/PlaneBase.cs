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
}
