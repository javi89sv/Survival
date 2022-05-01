using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public enum size { big, small }

public class LootWarehouse : MonoBehaviour
{

    public List<GameObject> database;
    public Slot[] slots;

    public GameObject menuUI;
    public GameObject slotHolder;

    private TextMeshProUGUI nameContainer;
    private GameObject player;

    public bool isOpen = false;

    public float radius;

    public size size;

    private void Awake()
    {

    }

    private void Start()
    {
        if (size == size.big)
        {
            menuUI = ChestManager.instance.bigContainerUI;
        }
        if (size == size.small)
        {
            menuUI = ChestManager.instance.smallContainerUI;
        }

        slotHolder = menuUI.transform.GetChild(0).gameObject;

        slots = menuUI.GetComponentsInChildren<Slot>();

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

        if (Input.GetKey(KeyCode.I) && isOpen)
        {
            CloseContainer();

        }

    }

    public void OpenContainer()
    {
       // InventoryManager.instance.openChestCurrent = this;
        ChestManager.instance.openChestCurrent = this.gameObject;

        foreach (GameObject loot in database)
        {

            if (loot != null)
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
        InventoryManager.instance.openChestCurrent = null;

        for (int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].empty)
            {
                slots[i].CleanSlot();
            }

        }
    }

    private void RefreshItems()
    {

        foreach (Slot slot in slots)
        {
            if (slot.empty == false)
            {

                database.Add(slot.prefab);

            }
        }

    }

    public void AddStack(GameObject go)
    {
        for (int i = 0; i < database.Count; i++)
        {
            if (database[i] == go)
            {
                Debug.Log("añadimos stack");
            }
        }
    }

    public GameObject GetPrefab(string name)
    {
        foreach (GameObject item in database)
        {
            if (item.name == name)
            {
                return item;
            }
        }
        return null;
    }

    public void RemoveItem(GameObject go)
    {
        database.Remove(go);
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
