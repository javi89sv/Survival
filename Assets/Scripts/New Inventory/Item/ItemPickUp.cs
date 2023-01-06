using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemPickUp : MonoBehaviour, IInterectable
{

    public ItemObject item;
    public int amount;

    public string textInfo;

    public void Interact(Interactor interactor)
    {

        var inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventoryHolder>();

        if (!inventory)
        {
            return;
        }

        if (inventory.AddToInventory(item, amount))
        {
            HudUI.instance.UpdateText(item.itemName, amount);
            Destroy(this.gameObject);
        }

    }

    public string TextInfo()
    {
        return textInfo;
    }
}
