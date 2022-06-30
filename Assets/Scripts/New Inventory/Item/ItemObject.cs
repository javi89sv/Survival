using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Consumable,
    Equipment,
    Default,
    Placeable
}


public abstract class ItemObject : ScriptableObject
{

    public GameObject prefab;
    public ItemType type;
    public Sprite icon;
    public int id;
    public string itemName;
    [TextArea(4, 4)]
    public string description;
    public int maxStackSize;

    public virtual void UseItem() { }
}
