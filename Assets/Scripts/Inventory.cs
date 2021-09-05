using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;



public class Inventory : Photon.Pun.MonoBehaviourPun
{
    public static Inventory instance;

    public Slot[] slots;
    public List<InteractiveItem> items;
    public GameObject slotHolder;

    public float distanceCollect;
    LayerMask ignoreLayer;

    GameObject itemPickedUp;




    void Start()
    {
        instance = this;


        slots = slotHolder.transform.GetComponentsInChildren<Slot>();

    }

    private void Update()
    {

        CollectItems();

    }

    //[PunRPC]
    private void CollectItems()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, distanceCollect, ~ignoreLayer))
        {
            if (hit.collider.tag == "Item")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    itemPickedUp = hit.collider.gameObject;

                    InteractiveItem item = itemPickedUp.GetComponent<InteractiveItem>();


                    AddItem(itemPickedUp, item.item, item.id, item.quantity, item.item.maxDegradable);
                    Debug.Log("Add Item!!");
                }
            }
        }
    }


    public void AddItem(GameObject prefab, Item item, int id, int quantity, int condition)
    {

        for (int i = 0; i < slots.Length; i++)
        {

            if (!slots[i].empty && slots[i].id == id && item.isStackable && slots[i].maxStackSize != true)
            {

                slots[i].amount += quantity;
                slots[i].condition = condition;
                prefab.SetActive(false);
                slots[i].UpdateSlot();
                GameManager.instance.UpdateText(item.name, quantity);

                return;
            }
            else if (slots[i].empty)
            {
                slots[i].prefab = prefab;
                items.Add(prefab.GetComponent<InteractiveItem>());
                prefab.transform.parent = slots[i].transform;
                slots[i].item = item;
                slots[i].id = id;
                slots[i].amount = quantity;
                slots[i].condition = condition;
                slots[i].empty = false;
                slots[i].UpdateSlot();
                GameManager.instance.UpdateText(item.name, quantity);
                PhotonNetwork.Destroy(prefab.gameObject);
                return;
            }

        }

        Vector3 instantiatePos = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        GameObject goReyected = Instantiate(prefab, instantiatePos, Quaternion.identity);
        Destroy(prefab);
        goReyected.GetComponent<Rigidbody>().AddForce(transform.forward * 200);
    }




    public int GetItemAmount(int id)
    {
        int num = 0;

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].GetComponent<Slot>().id == id)
            {

                num += slots[i].amount;

            }

        }
        return num;
    }

    public void CheckMaxStack()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].empty)
            {
                if (slots[i].GetComponent<Slot>().amount == slots[i].GetComponent<Slot>().item.maxStack)
                {

                    slots[i].GetComponent<Slot>().maxStackSize = true;

                }
            }

        }
    }

    public bool ContainItem(Item item, int amount)
    {

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].GetComponent<Slot>().item == item && slots[i].GetComponent<Slot>().amount >= amount)
            {


                return true;

            }
        }

        return false;
    }

    public void RemoveItem(Item item, int amount)
    {

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].GetComponent<Slot>().item == item && slots[i].GetComponent<Slot>().amount >= amount)
            {

                slots[i].GetComponent<Slot>().amount -= amount;
            }
        }

    }

    [PunRPC]
    public void DestroyItems(GameObject item)
    {
        PhotonNetwork.Destroy(item.gameObject);
    }



    //public void DamageItem(Item item)
    //{
    //    for (int i = 0; i < slots.Length; i++)
    //    {
    //        if (item.id == slots[i].GetComponent<Slot>().item.id)
    //        {
    //            slots[i].TakeDamage(10);
    //            slots[i].UpdateSlot();

    //        }

    //    }
    //}




}
