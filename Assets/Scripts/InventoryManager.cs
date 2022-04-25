using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AddItem
{
    public Item item;
    public int amount;

}

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public Slot[] slot;
    //public List<GameObject> items;
    public Dictionary<string, int> listItems = new Dictionary<string, int>();
    
    public GameObject slotHolder;

    public float distanceCollect;
    LayerMask ignoreLayer;

    GameObject itemPickedUp;




    void Start()
    {
        instance = this;
        slot = slotHolder.transform.GetComponentsInChildren<Slot>();

    }

    private void Update()
    {
        CollectItems();
    }


    private void CollectItems()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, distanceCollect, ~ignoreLayer))
        {
            if (hit.collider.tag == "Item")
            {
                if (Input.GetKey(KeyCode.E))
                {
                    itemPickedUp = hit.collider.gameObject;

                    InteractiveItem item = itemPickedUp.GetComponent<InteractiveItem>();
                    itemPickedUp.GetComponent<Renderer>().enabled = false;
                    itemPickedUp.GetComponent<Collider>().enabled = false;
                    itemPickedUp.GetComponent<Rigidbody>().isKinematic = true;

                    AddItem(itemPickedUp, item.item, item.id, item.quantity);
                    Debug.Log("Add Item!!");
                }
            }
        }
    }


    public void AddItem(GameObject prefab, Item item, int id, int quantity)
    {

        for (int i = 0; i < slot.Length; i++)
        {

            if (!slot[i].empty && slot[i].id == id && item.isStackable && slot[i].maxStackSize != true)
            {

                slot[i].amount += quantity;
                slot[i].UpdateSlot();
                GameManager.instance.UpdateText(item.name, quantity);


                return;
            }
            else if (slot[i].empty)
            {
                listItems.Add(prefab.name,quantity);
                slot[i].item = item;
                slot[i].id = id;
                slot[i].amount = quantity;
                slot[i].empty = false;
                slot[i].UpdateSlot();
                GameManager.instance.UpdateText(item.name, quantity);
                Destroy(prefab);

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

        for (int i = 0; i < slot.Length; i++)
        {
            if (slot[i].GetComponent<Slot>().id == id)
            {

                num += slot[i].amount;

            }

        }
        return num;
    }

    public void CheckMaxStack()
    {
        for (int i = 0; i < slot.Length; i++)
        {
            if (!slot[i].empty)
            {
                if (slot[i].GetComponent<Slot>().amount == slot[i].GetComponent<Slot>().item.maxStack)
                {

                    slot[i].GetComponent<Slot>().maxStackSize = true;

                }
            }

        }
    }

    public bool ContainItem(Item item, int amount)
    {

        for (int i = 0; i < slot.Length; i++)
        {
            if (slot[i].GetComponent<Slot>().item == item && slot[i].GetComponent<Slot>().amount >= amount)
            {


                return true;

            }
        }

        return false;
    }

    public void RemoveItem(Item item, int amount)
    {

        for (int i = 0; i < slot.Length; i++)
        {
            if (slot[i].GetComponent<Slot>().item == item && slot[i].GetComponent<Slot>().amount >= amount)
            {

                slot[i].GetComponent<Slot>().amount -= amount;
                GameObject prefab = slot[i].prefab.gameObject;
                slot[i].UpdateSlot();
            }
        }

    }

    IEnumerator CraftTimer(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("CRAFT ITEM");
    }

    public void StartCraft(float time)
    {
        StartCoroutine(CraftTimer(time));
    }

}
