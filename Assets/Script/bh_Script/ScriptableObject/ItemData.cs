using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemTag
{
    Food = 0,
    Material,
    Tool
}

[CreateAssetMenu(menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    // Start is called before the first frame update
    [SerializeField]
    int amountOfHungerRecovery;
    public int AmountOfHungerRecovery { get => amountOfHungerRecovery; }
    [SerializeField]
    string itemName;
    public string ItemName { get => itemName; }
    [SerializeField]
    string explan;
    public string Explan { get => explan; }
    [SerializeField]
    ItemTag tag;
    public ItemTag Tag { get => tag; }
    [SerializeField]
    Sprite iconSprite;
    public Sprite IconSprite { get => iconSprite; }
}
