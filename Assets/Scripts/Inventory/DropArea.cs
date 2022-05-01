using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedItem = eventData.pointerDrag;
        ItemContainer chest = InventoryManager.instance.openChestCurrent;


        if (!this.GetComponent<Slot>())
        {

            if (droppedItem.CompareTag("SlotContainer"))
            {
              // InventoryManager.instance.openChestCurrent.GetComponent<ItemCollection>().Remove(InventoryManager.instance.openChestCurrent.GetPrefab(droppedItem.GetComponent<Slot>().item.prefab.name));
            }

            droppedItem.GetComponent<Slot>().DropItem();
            
            return;
        }

        if (!this.GetComponent<Slot>().empty)
        {
            return;
        }


        // Si ya tenemmos este item, lo stackeamos.

        if (this.GetComponent<Slot>().id == droppedItem.GetComponent<Slot>().id && droppedItem.GetComponent<Slot>().item.isStackable)
        {
            if (chest)
            {
                
            }
            this.GetComponent<Slot>().amount += droppedItem.GetComponent<Slot>().amount;
            this.GetComponent<Slot>().empty = false;
            this.GetComponent<Slot>().UpdateSlot();
            droppedItem.GetComponent<Slot>().CleanSlot();
        }

        //Si aun no tenemos el item, lo añadimos.

        if (this.GetComponent<Slot>().empty && !this.GetComponent<Slot>().maxStackSize)
        {
            if (GameManager.instance.weapon != null)
            {
                GameManager.instance.weapon.gameObject.SetActive(false);
            }

            if (this.CompareTag("SlotContainer"))
            {
                InventoryManager.instance.openChestCurrent.GetComponent<ItemCollection>().Add(droppedItem.GetComponent<Slot>().item, droppedItem.GetComponent<Slot>().amount);
            }


            if (this.GetComponent<Slot>() && droppedItem.CompareTag("SlotContainer"))
            {
               // InventoryManager.instance.openChestCurrent.database.Remove(InventoryManager.instance.openChestCurrent.GetPrefab(droppedItem.GetComponent<Slot>().item.prefab.name));
            }

            this.GetComponent<Slot>().item = droppedItem.GetComponent<Slot>().item;
            this.GetComponent<Slot>().amount = droppedItem.GetComponent<Slot>().amount;
            this.GetComponent<Slot>().empty = false;
            this.GetComponent<Slot>().UpdateSlot();

            

            if (droppedItem.GetComponent<Slot>().maxStackSize)
            {
                droppedItem.GetComponent<Slot>().maxStackSize = false;
            }

            droppedItem.GetComponent<Slot>().CleanSlot();

        }


    }
}

