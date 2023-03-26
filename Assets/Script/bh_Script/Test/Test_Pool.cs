using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Test_Pool : Test_Base
{
    ItemInventoryWindow itemInventoryWindow;
    ItemInventoryWindowExplanRoom explanRoom;
    //test

    void Start() 
    {
        itemInventoryWindow = FindObjectOfType<ItemInventoryWindow>();
        explanRoom = FindObjectOfType<ItemInventoryWindowExplanRoom>();
    }

    protected override void DoAction1(InputAction.CallbackContext _)
    {
        GameObject obj = ItemManager.Instance.GetObject(ItemType.Shark); // Tomato 게임오브젝트를 ItemManager에서 가져와 활성화
        obj.transform.position = Vector3.up * 9;
    }

    protected override void DoAction2(InputAction.CallbackContext _)
    {
        //GameObject obj = ItemManager.Instance.GetObject(ItemType.Avocado); // Strawberry 게임오브젝트를 ItemManager에서 가져와 활성화
        //obj.transform.position = Vector3.up * 9;
        if (itemInventoryWindow.gameObject.activeSelf == true)
        {
            itemInventoryWindow.PreOnDisble();
            itemInventoryWindow.gameObject.SetActive(false);
            explanRoom.gameObject.SetActive(false);
        }
        else 
        {
            itemInventoryWindow.gameObject.SetActive(true);
            explanRoom.gameObject.SetActive(true);
        }
    }

    protected override void DoAction3(InputAction.CallbackContext _)
    {
        //GameObject obj = ItemManager.Instance.GetObject(ItemType.Peanut); // Potato 게임오브젝트를 ItemManager에서 가져와 활성화
        //obj.transform.position = Vector3.up * 9;

        if (ItemManager.Instance.itemInventory.emptySpaceStartIndex < ItemManager.Instance.itemInventoryMaxSpace) 
        {
            ItemManager.Instance.itemInventory.ItemTypeArray[ItemManager.Instance.itemInventory.emptySpaceStartIndex] = ItemType.StoneAxe;
            ItemManager.Instance.itemInventory.ItemAmountArray[ItemManager.Instance.itemInventory.emptySpaceStartIndex] += 1;
            ItemManager.Instance.itemInventory.emptySpaceStartIndex++;
        }
        
        itemInventoryWindow.RefreshItemInventory();
    }

    protected override void DoAction4(InputAction.CallbackContext _)
    {

        bool inventoryAlreadyhave = false; // 아이템 인벤토리에 특정 아이템이 있는지 여부를 확인하는 bool 변수
        for (int i = 0; i < ItemManager.Instance.itemInventory.emptySpaceStartIndex; i++) { 
            if (ItemManager.Instance.itemInventory.ItemTypeArray[i] == ItemType.Strawberry) 
            {
                ItemManager.Instance.itemInventory.ItemAmountArray[i] += 1;
                inventoryAlreadyhave = true;
                break;
            } //아이템 인벤토리에서 Strawberry가 있는지 검사하고 있다면, 그 위치에 개수 1개 추가
        }
        if (!inventoryAlreadyhave) // 만약 아이템 인벤토리에 Strawberry가 없다면
        {
            if (ItemManager.Instance.itemInventory.emptySpaceStartIndex < ItemManager.Instance.itemInventoryMaxSpace) // 그리고 만약 아이템 인벤토리가 꽉 차지 않았다면
            {
                ItemManager.Instance.itemInventory.ItemTypeArray[ItemManager.Instance.itemInventory.emptySpaceStartIndex] = ItemType.Strawberry; 
                ItemManager.Instance.itemInventory.ItemAmountArray[ItemManager.Instance.itemInventory.emptySpaceStartIndex] += 1;
                ItemManager.Instance.itemInventory.emptySpaceStartIndex++; // 아이템 인벤토리에 Strawberry를 추가한 뒤  그 Strawberry의 개수를 1개 추가
            }
        }
        
        itemInventoryWindow.RefreshItemInventory();
        Debug.Log(ItemManager.Instance.itemInventory.ItemAmountArray[0]); // 아이템 인벤토리 0번째 칸에 있는 아이템의 개수 출력
        Debug.Log(ItemManager.Instance[ItemType.Strawberry].ItemName); // Strawberry의 (한글)이름 출력
        Debug.Log(ItemManager.Instance[ItemType.Strawberry].Explan); // Strawberry의 설명 출력
        Debug.Log(ItemManager.Instance[ItemType.Strawberry].AmountOfHungerRecovery); // Strawberry의 허기회복량 출력
    }

    protected override void DoAction5(InputAction.CallbackContext _)
    {
        bool inventoryAlreadyhave = false; // 아이템 인벤토리에 특정 아이템이 있는지 여부를 확인하는 bool 변수
        for (int i = 0; i < ItemManager.Instance.itemInventory.emptySpaceStartIndex; i++)
        {
            if (ItemManager.Instance.itemInventory.ItemTypeArray[i] == ItemType.Avocado)
            {
                ItemManager.Instance.itemInventory.ItemAmountArray[i] += 1;
                inventoryAlreadyhave = true;
                break;
            } //아이템 인벤토리에서 Avocado가 있는지 검사하고 있다면, 그 위치에 개수 1개 추가
        }
        if (!inventoryAlreadyhave) // 만약 아이템 인벤토리에 Avocado가 없다면
        {
            if (ItemManager.Instance.itemInventory.emptySpaceStartIndex < ItemManager.Instance.itemInventoryMaxSpace) // 그리고 만약 아이템 인벤토리가 꽉 차지 않았다면
            {
                ItemManager.Instance.itemInventory.ItemTypeArray[ItemManager.Instance.itemInventory.emptySpaceStartIndex] = ItemType.Avocado;
                ItemManager.Instance.itemInventory.ItemAmountArray[ItemManager.Instance.itemInventory.emptySpaceStartIndex] += 1;
                ItemManager.Instance.itemInventory.emptySpaceStartIndex++; // 아이템 인벤토리에 Avocado를 추가한 뒤  그 Avocado의 개수를 1개 추가
            }
        }
        
        itemInventoryWindow.RefreshItemInventory();
        //SceneManager.LoadScene("SampleScene2");
    }
}
