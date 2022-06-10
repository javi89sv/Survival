using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DynamicInventoryDisplay : InventoryDisplay
{

    [SerializeField] protected InventorySlotUI slotPrefab;

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        
    }

    public void RefreshDynamicInventory(InventorySystem invSystem)
    {
        ClearSlots();
        inventorySystem = invSystem;
        if (inventorySystem != null)
        {
            inventorySystem.OnInventorySlotChanged += UpdateSlot;
        }
        AssignSlot(invSystem);
    }


    public override void AssignSlot(InventorySystem invToDisplay)
    {
        ClearSlots();
        slotsDictionary = new Dictionary<InventorySlotUI, InventorySlot>();

        if (invToDisplay == null)
        {
            return;
        }

        for (int i = 0; i < invToDisplay.inventorySize; i++)
        {
            var uiSlot = Instantiate(slotPrefab, transform);
            slotsDictionary.Add(uiSlot, invToDisplay.InventorySlots[i]);
            uiSlot.Init(invToDisplay.InventorySlots[i]);
            uiSlot.UpdateSlotUI();
        }
    }
    private void ClearSlots()
    {
        foreach (var item in transform.GetComponentsInChildren<InventorySlotUI>())
        {
            Destroy(item.gameObject);
        }

        if (slotsDictionary != null)
        {
            slotsDictionary.Clear();
        }
    }

    private void OnDisable()
    {
        if (inventorySystem != null)
        {
            inventorySystem.OnInventorySlotChanged -= UpdateSlot;
        }
    }
}
