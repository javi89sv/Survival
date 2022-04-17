using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LootWarehouse : MonoBehaviour
{

    public GameObject[] loot;
    public Slot[] slots;

    public GameObject slotHolder;

    public GameObject menuUI;

    private void Start()
    {
        slots = slotHolder.transform.GetComponentsInChildren<Slot>();
        UpdateSlots();
        AddItem();
    }

    private void Update()
    {

    }

    private void UpdateSlots()
    {
        foreach (Slot slot in slots)
        {
            if (!slot.item)
            {
                slot.empty = true;
            }
        }
    }

    private void AddItem()
    {
        foreach(Slot slot in slots)
        {
            if (slot.empty)
            {
                for(int i = 0; i < loot.Length; i++)
                {

                    loot[i] = slot.prefab;

                }
                return;
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collision");

                menuUI.gameObject.SetActive(true);
          
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collision");

            menuUI.gameObject.SetActive(false);


        }
    }

}
