using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string itemName;
    public int id;
    public string description;
    public int maxStack;
    public int maxDegradable;
    public Sprite icon;
    public Type type;
    public GameObject prefab;


    [HideInInspector]
    public bool isStackable { get { return (maxStack > 1); } }
    public bool isDegradable { get { return (maxDegradable > -1); } }


    public enum Type
    {
        consumable,
        equippable,
        resource,
        ammo
    }

    
}



