using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ToolItemTag
{
    Axe = 0,
    Pickaxe,
    Sickle,
    Fishingrod
}

[CreateAssetMenu(menuName = "Scriptable Object/ToolItemData")]
public class ToolItemData : ItemData
{
    // Start is called before the first frame update
    [SerializeField]
    ToolItemTag toolType;
    public ToolItemTag ToolType { get => toolType; }
    [SerializeField]
    int level;
    public int Level { get => level; }
}
