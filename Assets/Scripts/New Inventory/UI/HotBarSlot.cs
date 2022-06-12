using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotBarSlot : MonoBehaviour
{
    public GameObject[] eqquipment;
    public GameObject[] slots;

    //public KeyCode key;

    private void Update()
    {

        if (Input.GetKey(KeyCode.Alpha1))
        {
            Eqquip(0);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            Eqquip(1);
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            //equipamos slot[3];
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            //equipamos slot[4];
        }
        if (Input.GetKey(KeyCode.Alpha5))
        {
            //equipamos slot[5];
        }
        if (Input.GetKey(KeyCode.Alpha6))
        {
            //equipamos slot[6];
        }
    }

    private void Eqquip(int index)
    {
        var slot = GetComponent<InventorySlotUI>();

        if (slot.asiggnedInventorySlot.item != null)
        {
            if (slot.asiggnedInventorySlot.item.type == ItemType.Equipment)
            {
                for (int i = 0; i < eqquipment.Length; i++)
                {
                    if (eqquipment[index].name == slot.asiggnedInventorySlot.item.name)
                    {
                        eqquipment[index].gameObject.SetActive(true);
                    }
                }
            }
        }
    }
}
