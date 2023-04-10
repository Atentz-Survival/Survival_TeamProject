using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Axe : MonoBehaviour
{
    private GameObject[] axes;
    private int[] axesLevel = {1,2,3};
    private string[] axesNames = { "Axe1", "Axe2", "Axe3" };

    PlaneBase player;


    private void Awake()
    {
        player = GetComponent<PlaneBase>();
    }
    private void Start()
    {
        axes = new GameObject[axesNames.Length];
        for (int i = 0; i < axesNames.Length; i++)
        {
            axes[i] = GameObject.Find(axesNames[i]);
            axes[i].SetActive(false);
        }
    }

    public void OnCangeAxelLevel()
    {
        if(ItemManager.Instance.itemInventory.GetEquipToolLevel(ToolItemTag.Axe) == axesLevel[ItemManager.Instance.itemInventory.GetEquipToolLevel(ToolItemTag.Axe) - 1])
        {
            for (int i = 0; i < axesNames.Length; i++)
            {
                axes[i].SetActive(false);
            }
            axes[ItemManager.Instance.itemInventory.GetEquipToolLevel(ToolItemTag.Axe) - 1].SetActive(true);
        }
    }
}
