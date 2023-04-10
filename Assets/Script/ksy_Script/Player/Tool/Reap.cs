using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reap : MonoBehaviour
{
    private GameObject[] reap;
    private int[] reapLevel = { 1, 2, 3 };
    private string[] reapNames = { "Reap1", "Reap2", "Reap3" };


    private void Start()
    {
        reap = new GameObject[reapNames.Length];
        for (int i = 0; i < reapNames.Length; i++)
        {
            reap[i] = GameObject.Find(reapNames[i]);
            reap[i].SetActive(false);
        }
    }

    public void OnCangeReapLevel()
    {
        if (ItemManager.Instance.itemInventory.GetEquipToolLevel(ToolItemTag.Sickle) == reapLevel[ItemManager.Instance.itemInventory.GetEquipToolLevel(ToolItemTag.Sickle) - 1])
        {
            for (int i = 0; i < reapNames.Length; i++)
            {
                reap[i].SetActive(false);
            }
            reap[ItemManager.Instance.itemInventory.GetEquipToolLevel(ToolItemTag.Sickle) - 1].SetActive(true);
        }
    }
}
