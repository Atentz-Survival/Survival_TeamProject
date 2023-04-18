using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CraftingTable : ItemInventory
{
    Button equip;
    Button housing;
    Button boat;
    Button sickle;
    Button axe;
    Button pickaxe;
    Button fishing;

    private void Awake()
    {
        equip = GameObject.Find("Equip").GetComponent<Button>();
        housing = GameObject.Find("Housing").GetComponent<Button>();
        boat = GameObject.Find("Boat").GetComponent<Button>();

        sickle = equip.transform.GetChild(1).GetComponent<Button>();
        axe = equip.transform.GetChild(2).GetComponent<Button>();
        pickaxe = equip.transform.GetChild(3).GetComponent<Button>();
        fishing = equip.transform.GetChild(4).GetComponent<Button>();
    }

    private void Start()
    {
        gameObject.SetActive(true);

        equip.onClick.AddListener(CallLowMenu);
    }

    private void CallLowMenu()
    {
        equip.gameObject.SetActive(false);
        housing.gameObject.SetActive(false);
        boat.gameObject.SetActive(false);

        sickle.gameObject.SetActive(true);
        axe.gameObject.SetActive(true);
        pickaxe.gameObject.SetActive(true);
        fishing.gameObject.SetActive(true);
    }
}
