using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] MouseItemData mouseItemData;

    protected InventorySystem inventorySystem;
    protected Dictionary<InventorySlotUI, InventorySlot> slotsDictionary = new Dictionary<InventorySlotUI, InventorySlot>(); //Emparejamos el sloUI con el slot del inventario
    public InventorySystem InventorySystem => inventorySystem;
    public Dictionary<InventorySlotUI, InventorySlot> SlotsDictionary => slotsDictionary;

    public abstract void AssignSlot(InventorySystem invToDisplay);

    protected virtual void Start()
    {

    }

    protected virtual void UpdateSlot(InventorySlot updatedSlot)
    {
        foreach (var slot in slotsDictionary)
        {
            if (slot.Value == updatedSlot)
            {
                slot.Key.UpdateSlotUI(updatedSlot);
            }
        }
    }

    public void SlotClicked(InventorySlotUI clickedSlot)
    {
        bool isShiftPressed = Input.GetKey(KeyCode.LeftShift);


        //Si el slot que hacemos click tiene item asignado, y el mouse slot no tiene item asignado
        if (clickedSlot.asiggnedInventorySlot.item != null && mouseItemData.assignedInventorySlot.item == null)
        {
            //Si presionamos SHIFT hacemos split del stack
            if (isShiftPressed && clickedSlot.asiggnedInventorySlot.SplitStack(out InventorySlot halfStack))
            {
                mouseItemData.UpdateMouseSlot(halfStack);
                clickedSlot.UpdateSlotUI();
                return;
            }
            else //Colocamos el item del slot que clickamos en el mouse slot
            {
                mouseItemData.UpdateMouseSlot(clickedSlot.asiggnedInventorySlot);
                clickedSlot.ClearSLot();
                return;
            }

        }

        //Si el slot clickado tiene el slot vacio y en el mouse slot llevamos un slot
        if (clickedSlot.asiggnedInventorySlot.item == null && mouseItemData.assignedInventorySlot.item != null)
        {
            clickedSlot.asiggnedInventorySlot.AssignItem(mouseItemData.assignedInventorySlot);
            clickedSlot.UpdateSlotUI();
            mouseItemData.ClearSlot();
            return;

        }

        //Si ambas ranuras tienen un item...
        if (clickedSlot.asiggnedInventorySlot.item != null && mouseItemData.assignedInventorySlot.item != null)
        {
            bool isSameItem = clickedSlot.asiggnedInventorySlot.item == mouseItemData.assignedInventorySlot.item;

            //Si ambos items son iguales, los apilamos
            if (isSameItem && clickedSlot.asiggnedInventorySlot.CheckStack(mouseItemData.assignedInventorySlot.amount))
            {
                clickedSlot.asiggnedInventorySlot.AssignItem(mouseItemData.assignedInventorySlot);
                clickedSlot.UpdateSlotUI();
                mouseItemData.ClearSlot();
                return;
            }
           
            else if (isSameItem && !clickedSlot.asiggnedInventorySlot.CheckStack(mouseItemData.assignedInventorySlot.amount, out int leftInStack))
            {
                if (leftInStack < 1) // Si el stack esta full, intercambiamos items.
                {
                    SwapSlots(clickedSlot);
                }
                else // Si el stack no esta full, tomamos lo que nos falta del mouse slot y lo agregamos al stack del click slot, dejamos el resto en el mouse slot
                {
                    int remainingOnMouse = mouseItemData.assignedInventorySlot.amount - leftInStack;
                    clickedSlot.asiggnedInventorySlot.AddToStack(leftInStack);
                    clickedSlot.UpdateSlotUI();

                    var newItem = new InventorySlot(mouseItemData.assignedInventorySlot.item, remainingOnMouse);
                    mouseItemData.ClearSlot();
                    mouseItemData.UpdateMouseSlot(newItem);
                    return;

                }
            }

            else if (!isSameItem) // si no es el mismo item, itercambiamos uno por otro
            {
                SwapSlots(clickedSlot);
                return;
            }

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
