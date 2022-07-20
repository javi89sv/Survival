using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class InventorySystem
{

    [SerializeField] private List<InventorySlot> inventorySlots;

    public List<InventorySlot> InventorySlots => inventorySlots;
    public int inventorySize => InventorySlots.Count;

    public UnityAction<InventorySlot> OnInventorySlotChanged;



    public InventorySystem(int size) // Constructor para setear la cantidad de slots de inventario
    {
        inventorySlots = new List<InventorySlot>(size);

        for (int i = 0; i < size; i++)
        {
            inventorySlots.Add(new InventorySlot());
        }
    }


    public bool AddItem(ItemObject _item, int _amount)
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i].item == _item)
            {
                if (inventorySlots[i].CheckStack(_amount))
                {
                    InventorySlots[i].AddToStack(_amount);
                    OnInventorySlotChanged?.Invoke(inventorySlots[i]);
                    return true;
                }

            }
            if (inventorySlots[i].item == null)
            {
                if (inventorySlots[i].CheckStack(_amount))
                {
                    inventorySlots[i].UpdateSlot(_item, _amount);
                    OnInventorySlotChanged?.Invoke(inventorySlots[i]);
                    return true;
                }

            }
        }
        return false;
    }

    public void UpdateUISlots()
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            OnInventorySlotChanged?.Invoke(inventorySlots[i]);
        }
    }

    public bool ContainItem(ItemObject item, out InventorySlot getItem) // Checkeamos si el item que estamos pasando es el mismo del slot
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i].item == item)
            {
                getItem = inventorySlots[i];
                return true;
            }
        }
        getItem = null;
        return false;
    }

    public bool ContainItem(ItemObject item, int amount, out InventorySlot getItem) // Checkeamos si el item que estamos pasando es el mismo del slot
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i].item == item && inventorySlots[i].amount >= amount)
            {
                getItem = inventorySlots[i];
                return true;
            }
        }
        getItem = null;
        return false;
    }

    public bool ContainIngredients(ItemObject item, int amount, out InventorySystem inv)
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i].item == item && inventorySlots[i].amount >= amount)
            {
                inv = this;
                return true;
            }
        }
        inv = null;
        return false;
    }

    public void RemoveItems(ItemObject item, int amount)
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i].item == item)
            {
                inventorySlots[i].RemoveFromStack(amount);
                OnInventorySlotChanged?.Invoke(inventorySlots[i]);

            }
        }
    }

    public int GetTotal(ItemObject item)
    {
        var total = 0;

        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i].item == item)
            {
                total = inventorySlots[i].amount;
            }
        }

        return total;
    }

    public bool CheckEmpty()
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i].item != null)
            {
                return false;
            }
        }

        return true;
    }


}

