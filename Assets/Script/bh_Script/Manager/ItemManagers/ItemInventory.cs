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
    int[] _EquipToolIndex;
    public int[] _equipToolIndex
    {
        get => _EquipToolIndex;
        set => _EquipToolIndex = value;
    }

    ItemInventoryWindow itemsInventoryWindow;
    public ItemInventoryWindow ItemsInventoryWindow
    {
        set => itemsInventoryWindow = value; 
    }

    public void AddItem(ItemType itemType, int amount) 
    {
        if (ItemManager.Instance[itemType].Tag != ItemTag.Tool)
        {
            bool inventoryAlreadyhave = false; // 아이템 인벤토리에 특정 아이템이 있는지 여부를 확인하는 bool 변수
            for (int i = 0; i < emptySpaceStartIndex; i++)
            {
                if (ItemTypeArray[i] == itemType && ItemAmountArray[i] < ItemManager.Instance.itemInventoryWindowMaxAmount)
                {
                    if (ItemAmountArray[i] + amount <= ItemManager.Instance.itemInventoryWindowMaxAmount)
                    {
                        ItemAmountArray[i] += amount;
                        inventoryAlreadyhave = true;
                    }
                    else 
                    {
                        int amountOfPart = ItemManager.Instance.itemInventoryWindowMaxAmount - ItemAmountArray[i];
                        ItemAmountArray[i] += amountOfPart;
                        AddItem(itemType, amount - amountOfPart);
                        inventoryAlreadyhave = true;
                    }
                    break;
                } //아이템 인벤토리에서 Strawberry가 있는지 검사하고 있다면, 그 위치에 개수 1개 추가
            }
            if (!inventoryAlreadyhave) // 만약 아이템 인벤토리에 Strawberry가 없다면
            {
                if (emptySpaceStartIndex < ItemManager.Instance.itemInventoryMaxSpace) // 그리고 만약 아이템 인벤토리가 꽉 차지 않았다면
                {
                    ItemTypeArray[emptySpaceStartIndex] = itemType;
                    ItemAmountArray[emptySpaceStartIndex] += amount;
                    emptySpaceStartIndex++; // 아이템 인벤토리에 Strawberry를 추가한 뒤  그 Strawberry의 개수를 1개 추가
                }
            }
        }
        else
        {
            if (emptySpaceStartIndex < ItemManager.Instance.itemInventoryMaxSpace)
            {
                ItemTypeArray[emptySpaceStartIndex] = itemType;
                ItemAmountArray[emptySpaceStartIndex] = 1;
                emptySpaceStartIndex++;
            }
        }
        if (itemsInventoryWindow != null) {
            itemsInventoryWindow.RefreshItemInventory();
        }
    }

    public void SubtractItem(ItemType itemType, int amount) 
    {
    }

    public void GetEquipToolLevel() 
    {
        
    }
}
