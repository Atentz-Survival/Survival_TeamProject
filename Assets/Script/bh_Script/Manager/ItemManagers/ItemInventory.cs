using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    int[] itemAmountArray;
    public int[] ItemAmountArray
    {
        get => itemAmountArray;
        set { itemAmountArray = value; }
    }

    ItemType[] itemTypeArray;
    public ItemType[] ItemTypeArray
    {
        get => itemTypeArray;
        set { itemTypeArray = value; }
    }

    //public const int emptySpace = -1;
    int EmptySpaceStartIndex = 0;
    public int emptySpaceStartIndex
    {
        get => EmptySpaceStartIndex;
        set => EmptySpaceStartIndex = value;
    }

    public const int notEquip = -1;
    int _EquipToolIndex = notEquip;
    public int _equipToolIndex
    {
        get => _EquipToolIndex;
        set => _EquipToolIndex = value;
    }
}
