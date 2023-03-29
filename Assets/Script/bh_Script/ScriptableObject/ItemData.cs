using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemTag
{
    Food = 0,
    Material,
    Tool,
    Etc
}

[CreateAssetMenu(menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    // Start is called before the first frame update
    [SerializeField]
    protected int amountOfHungerRecovery;
    public int AmountOfHungerRecovery { get => amountOfHungerRecovery; }
    [SerializeField]
    protected string itemName;
    public string ItemName { get => itemName; }
    [SerializeField]
    protected string explan;
    public string Explan { get => explan; }
    [SerializeField]
    protected ItemTag tag;
    public ItemTag Tag { get => tag; }
    [SerializeField]
    protected Sprite iconSprite;
    public Sprite IconSprite { get => iconSprite; }
}
