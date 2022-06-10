using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{

    public ItemObject item;
    public int amount;


    //private void OnTriggerEnter(Collider other)
    //{
    //    var inventory = other.transform.GetComponent<PlayerInventoryHolder>();

    //    if (!inventory)
    //    {
    //        return;
    //    }


    //    if (inventory.AddToInventory(item, amount))
    //    {
    //        Destroy(this.gameObject);
    //    }


    //}

}
