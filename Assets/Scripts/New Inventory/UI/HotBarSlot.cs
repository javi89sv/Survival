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

        if (Input.GetKey(KeyCode.Alpha1))
        {
            Deseqquip();
            Eqquip(0);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            Deseqquip();
            Eqquip(1);
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            Deseqquip();
            Eqquip(2);
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            Deseqquip();
            Eqquip(3);
        }
        if (Input.GetKey(KeyCode.Alpha5))
        {
            Deseqquip();
            Eqquip(4);
        }
        if (Input.GetKey(KeyCode.Alpha6))
        {
            Deseqquip();
            Eqquip(5);
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
            }
        }
    }
}
