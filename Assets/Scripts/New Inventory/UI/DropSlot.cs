using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    InventorySlotUI clickedSlot;
    MouseItemData mouseItemData;

    private void Awake()
    {
        // clickedSlot = this.GetComponent<InventorySlotUI>();
        mouseItemData = MouseItemData.instance;
    }

    public void OnDrop(PointerEventData eventData)
    {
        var draggedSLot = eventData.pointerDrag.GetComponent<InventorySlotUI>() != null;
       
        Debug.Log("Drop!!");


    }
    public void SwapSlots(InventorySlotUI clickedUISlot)
    {
        var cloneSlot = new InventorySlot(mouseItemData.assignedInventorySlot.item, mouseItemData.assignedInventorySlot.amount);
        mouseItemData.ClearSlot();

        mouseItemData.UpdateMouseSlot(clickedUISlot.asiggnedInventorySlot);

        clickedUISlot.ClearSLot();
        clickedUISlot.asiggnedInventorySlot.AssignItem(cloneSlot);
        clickedUISlot.UpdateSlotUI();

    }


}

