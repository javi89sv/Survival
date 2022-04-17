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

    public bool isOpen = false;

    private void Start()
    {
        slots = slotHolder.transform.GetComponentsInChildren<Slot>();

    }

    private void Update()
    {

    }

    private void UpdateSlots()
    {
        
        foreach (Slot slot in slots)
        {
            if (slot.empty)
            {
                for (int i = 0; i < loot.Length; i++)
                {
                    Debug.Log("Item Selected");
                    slot.item = loot[i].GetComponent<InteractiveItem>().item;
                    

                }

            }
            
        }
    }

    private void AddItemSlot()
    {
        foreach (GameObject item in loot)
        {
            if (item != null)
            {
                for (int i = 0; i < slots.Length; i++)
                {

                    slots[i].item = item.GetComponent<InteractiveItem>().item;
                    slots[i].prefab = item.GetComponent<InteractiveItem>().item.prefab;
                    slots[i].amount = item.GetComponent<InteractiveItem>().quantity;
                    slots[i].empty = false;
                    slots[i].UpdateSlot();
                    
                }
                
            }

        }
    }   

    private void AddItem()
    {
        foreach (Slot slot in slots)
        {
            
        }
    }

    private void DeleteItem()
    {
        foreach (Slot slot in slots)
        {
            slot.item = null;
            slot.prefab = null;
            slot.amount = 0;
            slot.UpdateSlot();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collision");

            
            menuUI.gameObject.SetActive(true);
            AddItemSlot();
            isOpen = true;
            

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collision");

            
            menuUI.gameObject.SetActive(false);
            DeleteItem();
            isOpen = false;
            

        }
    }


}
