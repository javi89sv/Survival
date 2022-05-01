using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{

    public Transform slotParent;
    public Slot[] slots;

    public GameObject showMenu;

    public ItemCollection itemcollection;

    public bool isOpen;

    private void Awake()
    {
        slots = slotParent.GetComponentsInChildren<Slot>();
        //showMenu.SetActive(false);
    }

    public void OpenContainer()
    {
        InventoryManager.instance.openChestCurrent = this;
        showMenu.SetActive(true);
    }
    
    public void CloseContainer()
    {
        InventoryManager.instance.openChestCurrent = null;
        showMenu.SetActive(false);
    }


}
