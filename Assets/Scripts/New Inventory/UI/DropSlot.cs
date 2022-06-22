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
        Debug.Log("Drop!!");
        bool isShiftPressed = Input.GetKey(KeyCode.LeftShift);

        clickedSlot = eventData.pointerDrag.GetComponent<InventorySlotUI>();

        if(mouseItemData.assignedInventorySlot.item != null && this.GetComponent<InventorySlotUI>().asiggnedInventorySlot.item == null)
        {
            this.GetComponent<InventorySlotUI>().asiggnedInventorySlot = mouseItemData.assignedInventorySlot;
            this.GetComponent<InventorySlotUI>().UpdateSlotUI();
            clickedSlot.asiggnedInventorySlot.ClearSlot();
        }
        

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

