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
            bool inventoryAlreadyhave = false; // ������ �κ��丮�� Ư�� �������� �ִ��� ���θ� Ȯ���ϴ� bool ����
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
                } //������ �κ��丮���� Strawberry�� �ִ��� �˻��ϰ� �ִٸ�, �� ��ġ�� ���� 1�� �߰�
            }
            if (!inventoryAlreadyhave) // ���� ������ �κ��丮�� Strawberry�� ���ٸ�
            {
                if (emptySpaceStartIndex < ItemManager.Instance.itemInventoryMaxSpace) // �׸��� ���� ������ �κ��丮�� �� ���� �ʾҴٸ�
                {
                    ItemTypeArray[emptySpaceStartIndex] = itemType;
                    ItemAmountArray[emptySpaceStartIndex] += amount;
                    emptySpaceStartIndex++; // ������ �κ��丮�� Strawberry�� �߰��� ��  �� Strawberry�� ������ 1�� �߰�
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
