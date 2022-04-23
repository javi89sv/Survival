using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class LootWarehouse : MonoBehaviour
{

    public List <GameObject> loot;
    public Slot[] slots;

    public GameObject menuUI;
    public GameObject slotHolder;

    private TextMeshProUGUI nameContainer;

    private GameObject canvas;

    private GameObject player;

    public bool isOpen = false;

    public float radius;

    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");

        menuUI = GameObject.Find("Big Loot Menu");

        slotHolder = menuUI.transform.GetChild(0).gameObject;

        slots = slotHolder.transform.GetComponentsInChildren<Slot>();

        Transform canvasParent = canvas.transform.GetChild(4);

        player = GameObject.FindGameObjectWithTag("Player");

        //nameContainer.text = this.name;

    }

    private void Update()
    {

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance > radius && isOpen)
        {
            Debug.Log("fuera de radio");
            menuUI.SetActive(false);
            isOpen = false;
            GameManager.instance.inventoryEnable = false;

        }
        if (GameManager.instance.inventoryEnable == false)
        {
            isOpen = false;
            menuUI.SetActive(false);
        }

        if(Input.GetKey(KeyCode.I) && isOpen)
        {
            CloseContainer();

        }

    }

    public void OpenContainer()
    {

        foreach (GameObject loot in loot)
        {
            if(loot != null)
            {
                for (int i = 0; i < slots.Length; i++)
                {
                    if (slots[i].empty)
                    {
                        slots[i].item = loot.GetComponent<InteractiveItem>().item;
                        slots[i].amount = loot.GetComponent<InteractiveItem>().quantity;
                        slots[i].empty = false;
                        slots[i].UpdateSlot();
                        break;
                        
                    }                 
                }               
            }
        }
    }

    public void CloseContainer()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].empty)
            {
                slots[i].CleanSlot();
            }

        }
    }

    private void AddItem()
    {

        foreach (Slot slot in slots)
        {
            if (slot.empty == false)
            {

                for (int i = 0; i < loot.Count; i++)
                {
                    if (loot[i] == null)
                    {

                        loot[i] = slot.prefab;
                        break;
                    }
                }
            }
        }
    }


   

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        Debug.Log("Collision");


    //        menuUI.gameObject.SetActive(true);           
    //        isOpen = true;


    //    }
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        Debug.Log("Collision");


    //        menuUI.gameObject.SetActive(false);
    //        DeleteItem();
    //        isOpen = false;


    //    }
    //}


}
