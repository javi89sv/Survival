using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotBarSlot : MonoBehaviour
{
    public GameObject[] eqquipment;
    public InventorySlotUI[] slots;

    //public KeyCode key;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Deseqquip();
            Eqquip(0);
            ActionSlot(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Deseqquip();
            Eqquip(1);
            ActionSlot(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Deseqquip();
            Eqquip(2);
            ActionSlot(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Deseqquip();
            Eqquip(3);
            ActionSlot(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Deseqquip();
            Eqquip(4);
            ActionSlot(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Deseqquip();
            Eqquip(5);
            ActionSlot(5);
        }
    }

    private void Eqquip(int index)
    {
        for (int j = 0; j < slots.Length; j++)
        {
            if(slots[index].asiggnedInventorySlot.item != null && slots[index].asiggnedInventorySlot.item.type == ItemType.Equipment)
            {
                for (int i = 0; i < eqquipment.Length; i++)
                {
                    if (eqquipment[i].name == slots[index].asiggnedInventorySlot.item.name)
                    {
                        eqquipment[i].gameObject.SetActive(true);
                        EqquipmentManager.Instance.currentWeapon = eqquipment[i];
                    }

                }
            }

        }
 
    }
    private void Deseqquip()
    {
        for (int i = 0; i < eqquipment.Length; i++)
        {
            if (eqquipment[i].activeInHierarchy)
            {
                eqquipment[i].SetActive(false);
                EqquipmentManager.Instance.currentWeapon = null;
            }
        }
    }

    private void ActionSlot(int index)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(slots[index].asiggnedInventorySlot.item != null && slots[index].asiggnedInventorySlot.item.type == ItemType.Consumable)
            {
                slots[index].UseItemSlot();
                return;
            }

        }
    }
}
