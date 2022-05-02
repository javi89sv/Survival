using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public int slotCount;

    public bool isOpen;

    public GameObject chestUI;

    public List<Item> items = new List<Item>();

    public void AddItems()
    {
        if(items != null)
        {
            foreach (var item in items)
            {

            }
        }
    }

    public void StoreItems()
    {
        
    }


}
