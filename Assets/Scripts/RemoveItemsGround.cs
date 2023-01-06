using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveItemsGround : MonoBehaviour
{
    public ItemObject item;
    public int amount = 1;

    public string textInfo;

    public void Interact(Interactor interactor)
    {

        var inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventoryHolder>();

        if (!inventory)
        {
            return;
        }

        if (CanRemove())
        {
            if (inventory.AddToInventory(item, amount))
            {
                HudUI.instance.UpdateText(item.itemName, amount);
                Destroy(this.gameObject);
            }
        }


    }

    public string TextInfo()
    {
        return textInfo;
    }

    public bool CanRemove()
    {
        if (GetComponent<InventoryHolder>() != null)
        {
            if (GetComponent<InventoryHolder>().PrimaryInventorySystem.CheckEmpty())
            {
                return true;
                }
        }
        Debug.Log("You must clean objects");
        return false;
    }


}
