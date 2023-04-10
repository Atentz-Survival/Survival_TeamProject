using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishinfRod : MonoBehaviour
{
    private GameObject[] fishingRod;
    private int[] fishingRodLevel = { 1, 2, 3 };
    private string[] fishingRodNames = { "FishingRod1", "FishingRod2", "FishingRod3" };

    PlaneBase player;


    private void Awake()
    {
        player = GetComponent<PlaneBase>();
    }
    private void Start()
    {
        fishingRod = new GameObject[fishingRodNames.Length];
        for (int i = 0; i < fishingRodNames.Length; i++)
        {
            fishingRod[i] = GameObject.Find(fishingRodNames[i]);
            fishingRod[i].SetActive(false);
        }
    }

    public void OnCangeFishinfRodlLevel()
    {
        if (ItemManager.Instance.itemInventory.GetEquipToolLevel(ToolItemTag.Fishingrod) == fishingRodLevel[ItemManager.Instance.itemInventory.GetEquipToolLevel(ToolItemTag.Fishingrod) - 1])
        {
            for (int i = 0; i < fishingRodNames.Length; i++)
            {
                fishingRod[i].SetActive(false);
            }
            fishingRod[ItemManager.Instance.itemInventory.GetEquipToolLevel(ToolItemTag.Fishingrod) - 1].SetActive(true);
        }
    }
}
