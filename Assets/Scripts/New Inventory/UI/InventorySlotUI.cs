using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InventorySlotUI : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerClickHandler
{
    public Image itemIcon;
    public Image durability;
    public TextMeshProUGUI itemAmount;

    public InventorySlot asiggnedInventorySlot;

    public bool empty;

    private void Awake()
    {
        ClearSLot();

    }
    public void Init(InventorySlot invSlot)
    {
        asiggnedInventorySlot = invSlot;
        UpdateSlotUI(invSlot);
    }

    public void UpdateSlotUI(InventorySlot invslot)
    {
        if (invslot.item != null)
        {
            itemIcon.sprite = invslot.item.icon;
            itemIcon.color = Color.white;

            if (invslot.amount > 1)
            {
                itemAmount.text = invslot.amount.ToString();
            }
            else
            {
                itemAmount.text = "";
            }

            if(invslot.item.type == ItemType.Equipment)
            {
                durability.enabled = true;
            }
            else
            {
               
            }

        }

        else
        {
            itemAmount.text = "";
            itemIcon.sprite = null;
            itemIcon.color = Color.clear;
            durability.enabled = false;
        }
    }

    public void UpdateSlotUI()
    {
        if (asiggnedInventorySlot != null)
        {
            UpdateSlotUI(asiggnedInventorySlot);
        }
    }

    //public void OnUiSlotClick()
    //{
    //    parentInventory?.SlotClicked(this);
    //}

    public void UseItemSlot()
    {
        if (asiggnedInventorySlot.item != null)
        {
            asiggnedInventorySlot.item.UseItem();
            if(asiggnedInventorySlot.item.type == ItemType.Consumable)
            {
                asiggnedInventorySlot.RemoveFromStack(1);

            }
            if(asiggnedInventorySlot.item.type == ItemType.Placeable)
            {
                BuildSystem.instance.itemAssignedSlot = this;
            }
            if (asiggnedInventorySlot.amount < 1)
            {
                asiggnedInventorySlot.ClearSlot();
                UpdateSlotUI();
            }


        }

    }

    public void ClearSLot()
    {
        asiggnedInventorySlot?.ClearSlot();
        itemAmount.text = "";
        itemIcon.sprite = null;
        itemIcon.color = Color.clear;
        durability.enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (asiggnedInventorySlot.item != null)
        {
            HudUI.instance.panelInfo.SetActive(true);
            HudUI.instance.UpdateInfo(asiggnedInventorySlot.item.name, asiggnedInventorySlot.item.description, itemIcon);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (MouseItemData.instance.assignedInventorySlot.item != null && asiggnedInventorySlot.item == null)
        {
            asiggnedInventorySlot.AssignItem(MouseItemData.instance.assignedInventorySlot);
            UpdateSlotUI();
            MouseItemData.instance.ClearSlot();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (asiggnedInventorySlot.item != null)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                MouseItemData.instance.UpdateMouseSlot(this.asiggnedInventorySlot);
                asiggnedInventorySlot.ClearSlot();
                UpdateSlotUI();
            }
            else if (Input.GetKey(KeyCode.Mouse1)) //grab half of stack
            {
                asiggnedInventorySlot.SplitStack(out InventorySlot halfStack);
                MouseItemData.instance.UpdateMouseSlot(halfStack);
                UpdateSlotUI();
            }

        }
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            MouseItemData.instance.DropItem();
        }

    }

    public void OnDrop(PointerEventData eventData)
    {
        if (asiggnedInventorySlot.item == null)
        {
            asiggnedInventorySlot.AssignItem(MouseItemData.instance.assignedInventorySlot);
            UpdateSlotUI();
            MouseItemData.instance.ClearSlot();
            return;
        }

        //Si ambas ranuras tienen un item...
        if (asiggnedInventorySlot != null)
        {
            bool isSameItem = asiggnedInventorySlot.item == MouseItemData.instance.assignedInventorySlot.item;

            //Si ambos items son iguales, los apilamos
            if (isSameItem && asiggnedInventorySlot.CheckStack(MouseItemData.instance.assignedInventorySlot.amount))
            {
                asiggnedInventorySlot.AssignItem(MouseItemData.instance.assignedInventorySlot);
                UpdateSlotUI();
                MouseItemData.instance.ClearSlot();
                return;
            }
            else if (isSameItem && !asiggnedInventorySlot.CheckStack(MouseItemData.instance.assignedInventorySlot.amount, out int leftInStack))
            {
                if (leftInStack < 1) // Si el stack esta full, intercambiamos items.
                {
                    SwapSlots();
                }
                else // Si el stack no esta full, tomamos lo que nos falta del mouse slot y lo agregamos al stack del click slot, dejamos el resto en el mouse slot
                {
                    int remainingOnMouse = MouseItemData.instance.assignedInventorySlot.amount - leftInStack;
                    asiggnedInventorySlot.AddToStack(leftInStack);
                    UpdateSlotUI();

                    var newItem = new InventorySlot(MouseItemData.instance.assignedInventorySlot.item, remainingOnMouse);
                    MouseItemData.instance.ClearSlot();
                    MouseItemData.instance.UpdateMouseSlot(newItem);
                    return;

                }
            }

            else if (!isSameItem) // si no es el mismo item, itercambiamos uno por otro
            {
                SwapSlots();
                return;
            }
        }
    }

    public void SwapSlots()
    {
        var cloneSlot = new InventorySlot(MouseItemData.instance.assignedInventorySlot.item, MouseItemData.instance.assignedInventorySlot.amount);
        MouseItemData.instance.ClearSlot();

        MouseItemData.instance.UpdateMouseSlot(asiggnedInventorySlot);

        ClearSLot();
        asiggnedInventorySlot.AssignItem(cloneSlot);
        UpdateSlotUI();

    }


}

