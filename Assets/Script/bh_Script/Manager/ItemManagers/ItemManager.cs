using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    Iron,
    Gold,
    StrawberryGazamiZorim,
    AvocadoGrilledGalchi,
    PeanutSteamedShark,
    EscapeShip,
    StoneAxe,
    IronAxe,
    GoldAxe,
    StonePickaxe,
    IronPickaxe,
    GoldPickaxe,
    StoneSickle,
    IronSickle,
    GoldSickle,
    StoneFishingrod,
    IronFishingrod,
    GoldFishingrod,
    CraftingTable,
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
    int ItemInventoryWindowMaxAmount;
    public int itemInventoryWindowMaxAmount
    {
        get => ItemInventoryWindowMaxAmount;
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
    const int notSelect = -1;

    HousingAction housingAction;
    bool isHousingMode = false;

    SetUpItem setUpItem;

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
            itemInventory._equipToolIndex = new int[System.Enum.GetValues(typeof(ToolItemTag)).Length];
            for (int i = 0; i < itemInventoryMaxSpace; i++)
            {
                itemInventory.ItemTypeArray[i] = ItemType.Null;
            }
            for (int i = 0; i < itemInventory._equipToolIndex.Length; i++) 
            {
                itemInventory._equipToolIndex[i] = notSelect;
            }
            housingAction = new HousingAction();
        }
    }

    protected override void Initialize()
    {
        for (int i = 0; i < ItemTypeCount; i++)
        {
            dropItemPools[i]?.MakeObjectPool();
        }
        itemInventory.ItemsInventoryWindow = FindObjectOfType<ItemInventoryWindow>();
        setUpItem = FindObjectOfType<SetUpItem>();
    }

    private void OnDisable()
    {
        OffHousingMode(false);
    }

    public GameObject GetObject(ItemType itemType)
    {
        GameObject result = dropItemPools[(int)itemType]?.GetObject().gameObject;
        return result;
    }
    //test
    void SetUpObject(InputAction.CallbackContext _)
    {
        //Vector2 screenPosition = Mouse.current.position.ReadValue();
        //Vector2 aaa = UnityEngine.Camera.main.ScreenToWorldPoint(screenPosition);
        //Vector3 newPositon = new Vector3(aaa.x, 0, aaa.y);
        //Ray ray = UnityEngine.Camera.main.ScreenPointToRay(newPositon);
        bool isUse = false;
        Vector2 screenPosition = Mouse.current.position.ReadValue();
        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            setUpItem.transform.position = hit.point;
            isUse = true;
        }
        OffHousingMode(isUse);
    }

    public void OnHousingMode() 
    {
        if (isHousingMode == false) 
        { 
            isHousingMode = true;
            housingAction.Player.Enable();
            housingAction.Player.SetUp.performed += SetUpObject;
            Debug.Log("Onせせせせ");

        }   
    }

    void OffHousingMode(bool isUse) 
    {
        if (isHousingMode == true)
        {
            isHousingMode = false;
            housingAction.Player.SetUp.performed -= SetUpObject;
            housingAction.Player.Disable();
            Debug.Log("OFFせせせせ");
            if (itemInventory.ItemsInventoryWindow.gameObject.activeSelf == false)
            {
                itemInventory.ItemsInventoryWindow.gameObject.SetActive(true);
                itemInventory.ItemsInventoryWindow.AfterItemUse(isUse);
            }
        }
    }
}
