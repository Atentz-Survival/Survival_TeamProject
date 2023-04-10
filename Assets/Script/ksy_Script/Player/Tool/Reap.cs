using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reap : MonoBehaviour
{
    public void OnCangeReapLevel()
    {
        if (ItemManager.Instance.itemInventory.GetEquipToolLevel(ToolItemTag.Sickle) > 0)
        {
            for (int i = 0; i < 3; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            transform.GetChild(ItemManager.Instance.itemInventory.GetEquipToolLevel(ToolItemTag.Sickle) - 1).gameObject.SetActive(true);
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
