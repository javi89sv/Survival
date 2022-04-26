using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using System;


[CreateAssetMenu]
public class InventorySO : ScriptableObject
{

    public List<ListItems> listItems;


}

[Serializable]
public struct ListItems
{
    public int quantity;
    public Item item;

    public bool isEmpty;
}
