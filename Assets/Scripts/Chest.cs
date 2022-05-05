using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public int slotCount;

    public bool isOpen;

    public GameObject chestUI;

    public ChestScript chestScript;

  
    public List<Item> items = new List<Item>();


    private void Start()
    {
        StoreItems();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            AddItems();
        }
    }

    public void AddItems()
    {
        foreach (var slot in chestScript.slots)
        {
            if (!slot.empty)
            {
                return;
            }
            else
            {
                foreach (var item in items)
                {
                    slot.item = item;
                    slot.UpdateSlot();
                }
            }
        }
    }

    public void StoreItems()
    {
        foreach (Slot slot in chestScript.slots)
        {
            if (!slot.empty)
            {
                items.Add(slot.item);
            }
        }
    }

    public void ClearList()
    {


        
        
    }


}
