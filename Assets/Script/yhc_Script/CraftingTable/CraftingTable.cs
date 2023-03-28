using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CraftingTable : ItemInventory
{
    public GameObject equip_Button;
    public GameObject housing_Button;
    public GameObject boat_Button;
    public GameObject sickle_Menu;
    public GameObject axe_Menu;
    public GameObject pickaxe_Menu;
    public GameObject fishing_Menu;
    public GameObject housing_Menu;
    public GameObject boat_Menu;
    public GameObject sickle_Button;
    public GameObject axe_Button;
    public GameObject pickaxe_Button;
    public GameObject fishing_Button;

    private void Awake()
    {
        Button equip = equip_Button.GetComponent<Button>();
        Button housing = housing_Button.GetComponent<Button>();
        Button boat = boat_Button.GetComponent<Button>();
        Button sickle = sickle_Button.GetComponent<Button>();
        Button axe = axe_Button.GetComponent<Button>();
        Button pickaxe = pickaxe_Button.GetComponent<Button>();
        Button fishing = fishing_Button.GetComponent<Button>();

        equip.onClick.AddListener(ActiveSubCate);
        housing.onClick.AddListener(ActiveHousing);
        boat.onClick.AddListener(ActiveBoat);
        sickle.onClick.AddListener(ActiveSickleMenu);
        axe.onClick.AddListener(ActiveAxeMenu);
        pickaxe.onClick.AddListener(ActivePickaxeMenu);
        fishing.onClick.AddListener(ActiveFishingMenu);
    }

    private void Start()
    {
    }

    protected virtual void MenuInitialize()
    {
        sickle_Menu.gameObject.SetActive(false);
        axe_Menu.gameObject.SetActive(false);
        pickaxe_Menu.gameObject.SetActive(false);
        fishing_Menu.gameObject.SetActive(false);
        housing_Menu.gameObject.SetActive(false);
        boat_Menu.gameObject.SetActive(false);
        sickle_Button.gameObject.SetActive(false);
        axe_Button.gameObject.SetActive(false);
        pickaxe_Button.gameObject.SetActive(false);
        fishing_Button.gameObject.SetActive(false);
    }
    private void ActiveSubCate()
    {
        MenuInitialize();
        sickle_Button.gameObject.SetActive(true);
        axe_Button.gameObject.SetActive(true);
        pickaxe_Button.gameObject.SetActive(true);
        fishing_Button.gameObject.SetActive(true);
        Debug.Log("활성화");
    }

    private void ActiveHousing()
    {
        MenuInitialize();
        housing_Menu.gameObject.SetActive(true);
        Debug.Log("하우징 메뉴 활성화");
    }
    private void ActiveBoat()
    {
        MenuInitialize();
        boat_Menu.gameObject.SetActive(true);
        Debug.Log("보트 메뉴 활성화");
    }


    private void ActiveSickleMenu()
    {
        MenuInitialize();
        sickle_Menu.gameObject.SetActive(true);
        Debug.Log("낫 메뉴 활성화");
    }

    private void ActiveAxeMenu()
    {
        MenuInitialize();
        axe_Menu.gameObject.SetActive(true);
        Debug.Log("도끼 메뉴 활성화");
    }

    private void ActivePickaxeMenu()
    {
        MenuInitialize();
        pickaxe_Menu.gameObject.SetActive(true);
        Debug.Log("곡괭이 메뉴 활성화");
    }

    private void ActiveFishingMenu()
    {
        MenuInitialize();
        fishing_Menu.gameObject.SetActive(true);
        Debug.Log("낚싯대 메뉴 활성화");
    }
}
