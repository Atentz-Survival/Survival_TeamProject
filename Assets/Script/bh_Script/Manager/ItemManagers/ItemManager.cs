using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ItemType
{
    Strawberry = 0,
    Avocado,
    Peanut,
    Firewood,
    FirewoodX3,
    FirewoodX5,
    Gazami,
    Galchi,
    Shark,
    Stone,
    StrawberryGazamiZorim,
    AvocadoGrilledGalchi,
    PeanutSteamedShark,
    EscapeShip,
    StoneAxe,
    Null
}

public class ItemManager : Singleton<ItemManager>
{
    ItemInventory itemsInventory;
    public ItemInventory itemInventory
    {
        get => itemsInventory;
        set => itemsInventory = value;
    }

    [SerializeField]
    int ItemInventoryMaxSpace;
    public int itemInventoryMaxSpace
    {
        get => ItemInventoryMaxSpace;
    }

    [SerializeField]
    int NotDropItemTypeAmount = 1;
    public int notDropItemTypeAmount
    {
        get => NotDropItemTypeAmount;
    }

    // Start is called before the first frame update
    [SerializeField]
    List<ItemData> itemDatas;
    public ItemData this[ItemType type]
    {
        get { return itemDatas[(int)type]; }
        set { itemDatas[(int)type] = value; }
    }

    int ItemTypeCount;
    DropItemPool[] dropItemPools;


    protected override void PreInitialize()
    {
        if (initialized == false)
        {
            base.PreInitialize();
            ItemTypeCount = System.Enum.GetValues(typeof(ItemType)).Length - notDropItemTypeAmount - 1;
            dropItemPools = new DropItemPool[ItemTypeCount];
            for (int i = 0; i < ItemTypeCount; i++)
            {
                Transform childTransform = transform.GetChild(i);
                dropItemPools[i] = childTransform.GetComponent<DropItemPool>();
            }
            itemInventory = GetComponentInChildren<ItemInventory>();
            itemInventory.ItemAmountArray = new int[itemInventoryMaxSpace];
            itemInventory.ItemTypeArray = new ItemType[itemInventoryMaxSpace];
            for (int i = 0; i < itemInventoryMaxSpace; i++)
            {
                itemInventory.ItemTypeArray[i] = ItemType.Null;
            }
        }
    }

    protected override void Initialize()
    {
        for (int i = 0; i < ItemTypeCount; i++)
        {
            dropItemPools[i]?.MakeObjectPool();
        }
    }

    public GameObject GetObject(ItemType itemType)
    {
        GameObject result = dropItemPools[(int)itemType]?.GetObject().gameObject;
        return result;
    }
}
