using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventoryWindow : MonoBehaviour
{
    // Start is called before the first frame update

    public const int notSelect = -1;
    int _SelectedIndex = notSelect;
    public int _selectedIndex
    {
        get => _SelectedIndex;
        set => _SelectedIndex = value;
    }


    Color InventoryNormalColor = new Color(255f / 255f, 227f / 255f, 0, 1f);
    public Color inventoryNormalColor
    {
        get => InventoryNormalColor;
    }

    ItemInventoryWindowRoom[] itemInventoryWindowRooms;
    ItemInventoryWindowExplanRoom explanRoom;

    string[] itemTagString;

    void Awake()
    {
        itemInventoryWindowRooms = GetComponentsInChildren<ItemInventoryWindowRoom>();
        itemTagString = new string[] { "음식 아이템", "재료 아이템", "장비 아이템", "기타 아이템"};
    }

    private void Start()
    {
        explanRoom = FindObjectOfType<ItemInventoryWindowExplanRoom>();
        RefreshItemInventory();
    }

    public void PreOnDisble()
    {
        if (_selectedIndex != notSelect)
        {
            itemInventoryWindowRooms[_selectedIndex]._panelImage.color = inventoryNormalColor;
            _selectedIndex = notSelect;
        }
    }

    public void RefreshItemInventory()
    {
        for (int i = 0; i < ItemManager.Instance.itemInventoryMaxSpace; i++)
        {
            if (ItemManager.Instance.itemInventory.ItemTypeArray[i] != ItemType.Null)
            {
                if (ItemManager.Instance[ItemManager.Instance.itemInventory.ItemTypeArray[i]].Tag != ItemTag.Tool)
                {
                    itemInventoryWindowRooms[i].SetSpace(ItemManager.Instance[ItemManager.Instance.itemInventory.ItemTypeArray[i]].IconSprite, ItemManager.Instance.itemInventory.ItemAmountArray[i]);
                }
                else
                {
                    bool nowEquip = false;
                    if (i == ItemManager.Instance.itemInventory._equipToolIndex)
                    {
                        nowEquip = true;
                    }
                    itemInventoryWindowRooms[i].SetSpace(ItemManager.Instance[ItemManager.Instance.itemInventory.ItemTypeArray[i]].IconSprite, nowEquip);
                }
            }
            else
            {
                itemInventoryWindowRooms[i].DisableComponent();
            }
        }

    }

    public void SetExplan(int index)
    {
        if (index == notSelect)
        {
            itemInventoryWindowRooms[_selectedIndex]._panelImage.color = inventoryNormalColor;
            _selectedIndex = index;
            return;
        }
        if (_selectedIndex != notSelect)
        {
            itemInventoryWindowRooms[_selectedIndex]._panelImage.color = inventoryNormalColor;
        }
        explanRoom._itemName.text = ItemManager.Instance[ItemManager.Instance.itemInventory.ItemTypeArray[index]].ItemName;
        explanRoom._itemExplan.text = ItemManager.Instance[ItemManager.Instance.itemInventory.ItemTypeArray[index]].Explan;
        explanRoom._itemTag.text = itemTagString[(int)(ItemManager.Instance[ItemManager.Instance.itemInventory.ItemTypeArray[index]].Tag)];
        explanRoom._itemUseButton.gameObject.SetActive(true);
        explanRoom._itemDumpButton.gameObject.SetActive(true);
        _selectedIndex = index;
    }
    // Update is called once per frame
}
