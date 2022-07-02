using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningSystem : MonoBehaviour
{
    [Serializable]
    public struct ListItemsCooked
    {
        public ItemObject normalObject;
        public ItemObject cookedObject;
        public ItemObject burnObject;

        public float timeBurned;

    }

    public List<ListItemsCooked> listItems;

    public ItemObject wood;
    public ItemObject coal;

    public bool isRun;

    public ParticleSystem fireParticle;

    public float speedBurn;

    private InventorySlot gettedItem;


    public bool ContainWood()
    {
        var inventorySystem = this.GetComponent<Furnace>().PrimaryInventorySystem;

        if (inventorySystem.ContainItem(wood, out InventorySlot getItem))
        {
            gettedItem = getItem;
            Debug.Log("podemos encender el horno");
            return true;
        }
        else
        {
            Debug.Log("no hay madera");
            return false;
        }
    }

    IEnumerator Burn()
    {
        while (gettedItem.amount > 0)
        {
            yield return new WaitForSeconds(speedBurn);
            gettedItem.RemoveFromStack(3);
            this.GetComponent<Furnace>().PrimaryInventorySystem.AddItem(coal, 2);
            this.GetComponent<Furnace>().PrimaryInventorySystem.UpdateUISlots();

        }
        gettedItem.ClearSlot();
        InventoryUIController.instance.RefreshUIFurnace(this.GetComponent<Furnace>().PrimaryInventorySystem);
        isRun = false;
        Stop();

    }

    IEnumerator Timer()
    {
        while (isRun)
        {
            

            var inventorySystem = this.GetComponent<Furnace>().PrimaryInventorySystem;

            for (int i = 0; i < listItems.Count; i++)
            {
                if (listItems[i].normalObject == inventorySystem.ContainItem(listItems[i].normalObject, out InventorySlot myItem))
                {
                    yield return new WaitForSeconds(listItems[i].timeBurned);

                    inventorySystem.RemoveItems(listItems[i].normalObject, 1);
                    if(myItem.amount < 1)
                    {
                        myItem.ClearSlot();
                    }
                    inventorySystem.AddItem(listItems[i].cookedObject, 1);
                    this.GetComponent<Furnace>().PrimaryInventorySystem.UpdateUISlots();


                }
            }

          //  StartCoroutine("Timer");
        }

    }

    public bool Run()
    {
        if (ContainWood())
        {
            isRun = true;
            StartCoroutine("Burn");
            StartCoroutine("Timer");
            fireParticle.Play();

            return true;
        }

        return false;

    }

    public void Stop()
    {
        isRun = false;
        StopAllCoroutines();
        fireParticle.Stop();
    }

}
