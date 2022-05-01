using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using System;

public class ItemCollection : MonoBehaviour
{
    
    public List<Item> m_Items = new List<Item>();
    public List<int> m_Amounts = new List<int>();

    public string nameWindow;

    public void Add(Item item, int count)
    {
        m_Items.Add(item);
        m_Amounts.Add(count);
    }
    public void Remove(Item item, int count)
    {
        m_Items.Remove(item);
        m_Amounts.Remove(count);
    }


}


