using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick : MonoBehaviour
{
    private GameObject[] pick;
    private int[] pickLevel = { 1, 2, 3 };
    private string[] pickNames = { "Pick1", "Pick2", "Pick3" };


    private void Start()
    {
        pick = new GameObject[pickNames.Length];
        for (int i = 0; i < pickNames.Length; i++)
        {
            pick[i] = GameObject.Find(pickNames[i]);
            pick[i].SetActive(false);
        }
    }

    public void OnCangePickLevel()
    {
        if (ItemManager.Instance.itemInventory.GetEquipToolLevel(ToolItemTag.Pickaxe) == pickLevel[ItemManager.Instance.itemInventory.GetEquipToolLevel(ToolItemTag.Pickaxe) - 1])
        {
            for (int i = 0; i < pickNames.Length; i++)
            {
                pick[i].SetActive(false);
            }
            pick[ItemManager.Instance.itemInventory.GetEquipToolLevel(ToolItemTag.Pickaxe) - 1].SetActive(true);
        }
    }
}
