using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.InputSystem;

public class ItemInventoryWindowExplanRoom : MonoBehaviour
{
    // Start is called before the first frame update
    TextMeshProUGUI _ItemName;
    public TextMeshProUGUI _itemName
    {
        get => _ItemName;
        set => _ItemName = value;
    }
    TextMeshProUGUI _ItemExplan;
    public TextMeshProUGUI _itemExplan
    {
        get => _ItemExplan;
        set => _ItemExplan = value;
    }
    TextMeshProUGUI _ItemTag;
    public TextMeshProUGUI _itemTag
    {
        get => _ItemTag;
        set => _ItemTag = value;
    }
    Button _ItemUseButton;
    public Button _itemUseButton
    {
        get => _ItemUseButton;
        set => _ItemUseButton = value;
    }
    Button _ItemDumpButton;
    public Button _itemDumpButton
    {
        get => _ItemDumpButton;
        set => _ItemDumpButton = value;
    }

    public Action<int> onChangeHp;
    public Action onChangeTool;

    HousingAction housingAction;
    ItemInventoryWindow itemInventoryWindow;

    void Awake()
    {
        Transform child1 = transform.GetChild(0);
        _itemName = child1.GetComponent<TextMeshProUGUI>();
        Transform child2 = transform.GetChild(1);
        _itemExplan = child2.GetComponent<TextMeshProUGUI>();
        Transform child3 = transform.GetChild(2);
        _itemTag = child3.GetComponent<TextMeshProUGUI>();
        Transform child4 = transform.GetChild(3);
        _itemUseButton = child4.GetComponent<Button>();
        Transform child5 = transform.GetChild(4);
        _itemDumpButton = child5.GetComponent<Button>();
        housingAction = new HousingAction();
        initialize();
    }

    void Start()
    {
        itemInventoryWindow = FindObjectOfType<ItemInventoryWindow>();
        _itemUseButton.onClick.AddListener(ItemUse);
        _itemDumpButton.onClick.AddListener(ItemDump);
    }

    private void OnEnable()
    {
        housingAction.Player.Enable();
        //housingAction.Player.SetUp.performed += D;
    }

    void OnDisable()
    {
        initialize();
    }

    public void initialize()
    {
        _itemName.text = string.Empty;
        _itemExplan.text = string.Empty;
        _itemTag.text = string.Empty;
        _itemUseButton.gameObject.SetActive(false);
        _itemDumpButton.gameObject.SetActive(false);
    }

    void ItemUse()
    {
        if (ItemManager.Instance[ItemManager.Instance.itemInventory.ItemTypeArray[itemInventoryWindow._selectedIndex]].Tag == ItemTag.Food)
        {
            ItemManager.Instance.itemInventory.ItemAmountArray[itemInventoryWindow._selectedIndex] -= 1;
            if (((FoodItemData)ItemManager.Instance[ItemManager.Instance.itemInventory.ItemTypeArray[itemInventoryWindow._selectedIndex]]).ItemsAffectStatus == AffectStatus.Hp)
            {
                onChangeHp?.Invoke(((FoodItemData)ItemManager.Instance[ItemManager.Instance.itemInventory.ItemTypeArray[itemInventoryWindow._selectedIndex]]).AmountOfHungerRecovery);
            }
            if (ItemManager.Instance.itemInventory.ItemAmountArray[itemInventoryWindow._selectedIndex] <= 0)
            {
                int currentIndex = itemInventoryWindow._selectedIndex;
                for (; currentIndex + 1 < ItemManager.Instance.itemInventory.emptySpaceStartIndex; currentIndex++)
                {
                    ItemManager.Instance.itemInventory.ItemTypeArray[currentIndex] = ItemManager.Instance.itemInventory.ItemTypeArray[currentIndex + 1];
                    ItemManager.Instance.itemInventory.ItemAmountArray[currentIndex] = ItemManager.Instance.itemInventory.ItemAmountArray[currentIndex + 1];
                    for (int i = 0; i < ItemManager.Instance.itemInventory._equipToolIndex.Length; i++)
                    {
                        if (currentIndex + 1 == ItemManager.Instance.itemInventory._equipToolIndex[i])
                        {
                            ItemManager.Instance.itemInventory._equipToolIndex[i] -= 1;
                            break;
                        }
                    }
                }
                ItemManager.Instance.itemInventory.ItemTypeArray[currentIndex] = ItemType.Null;
                ItemManager.Instance.itemInventory.ItemAmountArray[currentIndex] = 0;
                ItemManager.Instance.itemInventory.emptySpaceStartIndex -= 1;
                itemInventoryWindow.SetExplan(ItemInventoryWindow.notSelect);
                initialize();
            }
        }
        else if (ItemManager.Instance[ItemManager.Instance.itemInventory.ItemTypeArray[itemInventoryWindow._selectedIndex]].Tag == ItemTag.Tool)
        {
            ToolItemTag selectedItemToolType = ((ToolItemData)ItemManager.Instance[ItemManager.Instance.itemInventory.ItemTypeArray[itemInventoryWindow._selectedIndex]]).ToolType;
            for (int i = 0; i < ItemManager.Instance.itemInventory._equipToolIndex.Length; i++)
            {
                if (selectedItemToolType == (ToolItemTag)i)
                {
                    if (ItemManager.Instance.itemInventory._equipToolIndex[i] != itemInventoryWindow._selectedIndex)
                    {
                        ItemManager.Instance.itemInventory._equipToolIndex[i] = itemInventoryWindow._selectedIndex;
                        onChangeTool?.Invoke();
                    }
                    else
                    {
                        ItemManager.Instance.itemInventory._equipToolIndex[i] = ItemInventory.notEquip;
                        onChangeTool?.Invoke();
                    }
                    break;
                }
            }
        }
        itemInventoryWindow.RefreshItemInventory();
    }

    void ItemDump()
    {
        ItemManager.Instance.itemInventory.ItemAmountArray[itemInventoryWindow._selectedIndex] -= 1;
        if (ItemManager.Instance.itemInventory.ItemAmountArray[itemInventoryWindow._selectedIndex] <= 0)
        {
            for (int i = 0; i < ItemManager.Instance.itemInventory._equipToolIndex.Length; i++)
            {
                if (ItemManager.Instance.itemInventory._equipToolIndex[i] == itemInventoryWindow._selectedIndex)
                {
                    ItemManager.Instance.itemInventory._equipToolIndex[i] = ItemInventory.notEquip;
                    onChangeTool?.Invoke();
                    break;
                }
            }
            int currentIndex = itemInventoryWindow._selectedIndex;
            for (; currentIndex + 1 < ItemManager.Instance.itemInventory.emptySpaceStartIndex; currentIndex++)
            {
                ItemManager.Instance.itemInventory.ItemTypeArray[currentIndex] = ItemManager.Instance.itemInventory.ItemTypeArray[currentIndex + 1];
                ItemManager.Instance.itemInventory.ItemAmountArray[currentIndex] = ItemManager.Instance.itemInventory.ItemAmountArray[currentIndex + 1];
                for (int i = 0; i < ItemManager.Instance.itemInventory._equipToolIndex.Length; i++)
                {
                    if (currentIndex + 1 == ItemManager.Instance.itemInventory._equipToolIndex[i])
                    {
                        ItemManager.Instance.itemInventory._equipToolIndex[i] -= 1;
                        break;
                    }
                }
            }
            ItemManager.Instance.itemInventory.ItemTypeArray[currentIndex] = ItemType.Null;
            ItemManager.Instance.itemInventory.ItemAmountArray[currentIndex] = 0;
            ItemManager.Instance.itemInventory.emptySpaceStartIndex -= 1;
            itemInventoryWindow.SetExplan(ItemInventoryWindow.notSelect);
            initialize();
        }
        itemInventoryWindow.RefreshItemInventory();
    }

    // Update is called once per frame
}
