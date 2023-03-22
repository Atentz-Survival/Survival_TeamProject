using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ItemType
{
    Strawberry = 0, 
    Avocado,
    Peanut,
    StoneAxe,
    Null
}

public class ItemManager : Singleton<ItemManager>
{
    public ItemInventory itemInventory;
    public int itemInventoryMaxSpace;
    public int notDropItemTypeAmount = 1; 

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
