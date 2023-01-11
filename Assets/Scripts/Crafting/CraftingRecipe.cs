using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct Requirements
{
    public ItemObject item;
    [Range(1, 999)]
    public int amount;

}


[CreateAssetMenu]
public class CraftingRecipe : ScriptableObject
{
    public List<Requirements> _requirements;
    public ItemObject result;
    public int amountResult;
    public float time;

}














