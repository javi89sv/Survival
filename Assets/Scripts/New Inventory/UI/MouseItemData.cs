using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class MouseItemData : MonoBehaviour
{
    public static MouseItemData instance;

    public TextMeshProUGUI itemCount;
    public Image itemIcon;

    private Transform playerTransform;

    public InventorySlot assignedInventorySlot;

    private void Awake()
    {

        instance = this;

        itemCount.text = "";
        itemIcon.sprite = null;
        itemIcon.color = Color.clear;

        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

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
        if (assignedInventorySlot.item != null) // Si tiene un item asignado que siga al cursor
        {
            this.gameObject.transform.position = Input.mousePosition;
        }
    }

    public void DropItem()
    {

        if (!isPointerOverUIObject())
        {
            if (assignedInventorySlot.item.prefab != null)
            {
                GameObject go = Instantiate(assignedInventorySlot.item.prefab, playerTransform.position + playerTransform.forward * 1f, Quaternion.identity);
                go.GetComponent<Rigidbody>().AddForce(playerTransform.forward * 5f, ForceMode.Impulse);
                go.name = go.name.Replace("(Clone)", "");
                go.GetComponent<ItemPickUp>().amount = assignedInventorySlot.amount;

                ClearSlot();
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

