using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class MouseItemData : MonoBehaviour
{

    public TextMeshProUGUI itemCount;
    public Image itemIcon;

    public InventorySlot assignedInventorySlot;

    private void Awake()
    {
        itemCount.text = "";
        itemIcon.sprite = null;
        itemIcon.color = Color.clear;
    }
    public void UpdateMouseSlot(InventorySlot invSlot)
    {
        assignedInventorySlot.AssignItem(invSlot);

        if (assignedInventorySlot.amount > 1)
        {
            itemCount.text = assignedInventorySlot.amount.ToString();
        }
        else
        {
            itemCount.text = "";
        }

        itemIcon.sprite = assignedInventorySlot.item.icon;
        itemIcon.color = Color.white;
    }
    private void Update()
    {
        if (assignedInventorySlot != null) // Si tiene un item asignado que siga al cursor
        {
            this.gameObject.transform.position = Input.mousePosition;

            if (Input.GetMouseButton(0) && !isPointerOverUIObject())
            {
                ClearSlot();
                //Añadir el drop del objeto al suelo
            }

        }
    }

    public void ClearSlot()
    {
        assignedInventorySlot.ClearSlot();
        itemCount.text = "";
        itemIcon.color = Color.clear;
        itemIcon.sprite = null;
    }

    public static bool isPointerOverUIObject() // Checkea si tocamos un objeto de la UI
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}

