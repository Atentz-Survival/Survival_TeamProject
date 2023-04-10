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
        //GameObject obj = ItemManager.Instance.GetObject(ItemType.Gold); // Tomato ���ӿ�����Ʈ�� ItemManager���� ������ Ȱ��ȭ
        //obj.transform.position = Vector3.up * 9;
        ItemManager.Instance.itemInventory.AddItem(ItemType.StonePickaxe, 1);
    }

    protected override void DoAction2(InputAction.CallbackContext _)
    {
        //GameObject obj = ItemManager.Instance.GetObject(ItemType.Avocado); // Strawberry ���ӿ�����Ʈ�� ItemManager���� ������ Ȱ��ȭ
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
        //if (ItemManager.Instance.itemInventory.emptySpaceStartIndex < ItemManager.Instance.itemInventoryMaxSpace) 
        //{
        //    ItemManager.Instance.itemInventory.ItemTypeArray[ItemManager.Instance.itemInventory.emptySpaceStartIndex] = ItemType.StoneAxe;
        //    ItemManager.Instance.itemInventory.ItemAmountArray[ItemManager.Instance.itemInventory.emptySpaceStartIndex] += 1;
        //    ItemManager.Instance.itemInventory.emptySpaceStartIndex++;
        //}

        ItemManager.Instance.itemInventory.AddItem(ItemType.StoneAxe, 1);
        
    }

    protected override void DoAction4(InputAction.CallbackContext _)
    {

        //bool inventoryAlreadyhave = false; // ������ �κ��丮�� Ư�� �������� �ִ��� ���θ� Ȯ���ϴ� bool ����
        //for (int i = 0; i < ItemManager.Instance.itemInventory.emptySpaceStartIndex; i++) { 
        //    if (ItemManager.Instance.itemInventory.ItemTypeArray[i] == ItemType.Strawberry) 
        //    {
        //        ItemManager.Instance.itemInventory.ItemAmountArray[i] += 1;
        //        inventoryAlreadyhave = true;
        //        break;
        //    } //������ �κ��丮���� Strawberry�� �ִ��� �˻��ϰ� �ִٸ�, �� ��ġ�� ���� 1�� �߰�
        //}
        //if (!inventoryAlreadyhave) // ���� ������ �κ��丮�� Strawberry�� ���ٸ�
        //{
        //    if (ItemManager.Instance.itemInventory.emptySpaceStartIndex < ItemManager.Instance.itemInventoryMaxSpace) // �׸��� ���� ������ �κ��丮�� �� ���� �ʾҴٸ�
        //    {
        //        ItemManager.Instance.itemInventory.ItemTypeArray[ItemManager.Instance.itemInventory.emptySpaceStartIndex] = ItemType.Strawberry; 
        //        ItemManager.Instance.itemInventory.ItemAmountArray[ItemManager.Instance.itemInventory.emptySpaceStartIndex] += 1;
        //        ItemManager.Instance.itemInventory.emptySpaceStartIndex++; // ������ �κ��丮�� Strawberry�� �߰��� ��  �� Strawberry�� ������ 1�� �߰�
        //    }
        //}
        ItemManager.Instance.itemInventory.SubtractItem(ItemType.GoldFishingrod, 3);
        //itemInventoryWindow.RefreshItemInventory();
        Debug.Log(ItemManager.Instance.itemInventory.ItemAmountArray[0]); // ������ �κ��丮 0��° ĭ�� �ִ� �������� ���� ���
        Debug.Log(ItemManager.Instance[ItemType.Strawberry].ItemName); // Strawberry�� (�ѱ�)�̸� ���
        Debug.Log(ItemManager.Instance[ItemType.Strawberry].Explan); // Strawberry�� ���� ���
        Debug.Log(((FoodItemData)(ItemManager.Instance[ItemType.Strawberry])).AmountOfHungerRecovery); // Strawberry�� ���ȸ���� ���
        //Debug.Log(((ToolItemData)(ItemManager.Instance[ItemType.IronAxe])).Level); 
    }

    protected override void DoAction5(InputAction.CallbackContext _)
    {
        ItemManager.Instance.itemInventory.AddItem(ItemType.StoneFishingrod, 1);
        ItemManager.Instance.itemInventory.AddItem(ItemType.IronFishingrod, 1);
        ItemManager.Instance.itemInventory.AddItem(ItemType.GoldFishingrod, 1);
        ItemManager.Instance.itemInventory.AddItem(ItemType.StoneAxe, 1);
        ItemManager.Instance.itemInventory.AddItem(ItemType.IronAxe, 1);
        ItemManager.Instance.itemInventory.AddItem(ItemType.GoldAxe, 1);
        ItemManager.Instance.itemInventory.AddItem(ItemType.StonePickaxe, 1);
        ItemManager.Instance.itemInventory.AddItem(ItemType.IronPickaxe, 1);
        ItemManager.Instance.itemInventory.AddItem(ItemType.GoldPickaxe, 1);
        ItemManager.Instance.itemInventory.AddItem(ItemType.StoneSickle, 1);
        ItemManager.Instance.itemInventory.AddItem(ItemType.IronSickle, 1);
        ItemManager.Instance.itemInventory.AddItem(ItemType.GoldSickle, 1);
        //Debug.Log($"Me:{ItemManager.Instance.itemInventory.FindItem(ItemType.Avocado, 70)}");
        //SceneManager.LoadScene("SampleScene2");
    }
}
