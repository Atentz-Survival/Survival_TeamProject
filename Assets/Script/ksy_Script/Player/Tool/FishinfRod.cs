using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishinfRod : MonoBehaviour
{
    private int[] fishingRodLevel = { 1, 2, 3 };

    private void Start()
    {
    }

    public void OnCangeFishinfRodlLevel()
    {
        if (ItemManager.Instance.itemInventory.GetEquipToolLevel(ToolItemTag.Fishingrod) > 0)
        {
            for (int i = 0; i < 3; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            transform.GetChild(ItemManager.Instance.itemInventory.GetEquipToolLevel(ToolItemTag.Fishingrod) - 1).gameObject.SetActive(true);
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            this.gameObject.SetActive(false);
        }
    }
}
