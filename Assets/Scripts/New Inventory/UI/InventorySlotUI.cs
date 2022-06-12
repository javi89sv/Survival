using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InventorySlotUI : MonoBehaviour, IPointerDownHandler
{
    public Image itemIcon;
    public TextMeshProUGUI itemAmount;

    public InventorySlot asiggnedInventorySlot;

    private Button button;
    private InventoryDisplay parentInventory;

    public bool empty;

    private void Awake()
    {
        ClearSLot();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnUiSlotClick);
        parentInventory = transform.parent.GetComponent<InventoryDisplay>();
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
        }

        else
        {
            itemAmount.text = "";
            itemIcon.sprite = null;
            itemIcon.color = Color.clear;
        }
    }

    public void UpdateSlotUI()
    {
        if (asiggnedInventorySlot != null)
        {
            UpdateSlotUI(asiggnedInventorySlot);
        }
    }

    public void OnUiSlotClick()
    {
        parentInventory?.SlotClicked(this);
    }

    public void UseItemSlot()
    {
        if (asiggnedInventorySlot.item != null && asiggnedInventorySlot.item.type == ItemType.Consumable)
        {
            asiggnedInventorySlot.item.UseItem();
            asiggnedInventorySlot.RemoveFromStack(1);
            if(asiggnedInventorySlot.amount < 1)
            {
                asiggnedInventorySlot.ClearSlot();
            }
            UpdateSlotUI();
        }

    }

    public void ClearSLot()
    {
        asiggnedInventorySlot?.ClearSlot();
        itemAmount.text = "";
        itemIcon.sprite = null;
        itemIcon.color = Color.clear;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
            UseItemSlot();

    }
}
