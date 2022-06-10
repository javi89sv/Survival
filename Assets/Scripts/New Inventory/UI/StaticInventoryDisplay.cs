using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInventoryDisplay : InventoryDisplay
{
    [SerializeField] private InventoryHolder inventoryHolder;
    [SerializeField] private InventorySlotUI[] slots;

    protected override void Start()
    {
        base.Start();
        if (inventoryHolder != null)
        {
            inventorySystem = inventoryHolder.PrimaryInventorySystem;
            inventorySystem.OnInventorySlotChanged += UpdateSlot;

        }
        else
        {
            Debug.LogWarning("No inventory assigned");
        }

        AssignSlot(InventorySystem);
    }
    public override void AssignSlot(InventorySystem invToDisplay)
    {
        slotsDictionary = new Dictionary<InventorySlotUI, InventorySlot>();

        if(slots.Length != inventorySystem.inventorySize)
        {
            Debug.Log("Inventory slots out of sync");
        }
        for (int i = 0; i < inventorySystem.inventorySize; i++)
        {
            slotsDictionary.Add(slots[i], inventorySystem.InventorySlots[i]);
            slots[i].Init(inventorySystem.InventorySlots[i]);
        }
    }
}
