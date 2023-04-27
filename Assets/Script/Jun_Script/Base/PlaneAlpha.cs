using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneAlpha : MonoBehaviour
{
    ItemManager itemManager;
    public Material mat;

    private void Awake()
    {
        itemManager = FindObjectOfType<ItemManager>();
    }

    private void Start()
    {
        SetActivate(0);
        itemManager.OnHousingmode += ChangeAlpha;
    }

    public void ChangeAlpha()
    {
        SetActivate(255);
        Debug.Log(mat.color.a);
    }
    
    void SetActivate(float alpha)
    {
        Color color = mat.color;
        color.a = alpha;
        mat.color = color;
    }
}
