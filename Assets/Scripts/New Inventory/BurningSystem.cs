using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    public Light lightEffect;

    public float speedBurn;

    private InventorySlot gettedItem;



    public bool ContainWood()
    {
        var inventorySystem = this.GetComponent<Furnace>().PrimaryInventorySystem;

        if (inventorySystem.ContainItem(wood, 3, out InventorySlot getItem))
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
        //Si hay madera en el horno entra
        while (ContainWood())
        {
            yield return new WaitForSeconds(speedBurn);
            gettedItem.RemoveFromStack(3);
            if(gettedItem.amount < 0)
            {
                gettedItem.ClearSlot();
            }
            this.GetComponent<Furnace>().PrimaryInventorySystem.AddItem(coal, 3);
            this.GetComponent<Furnace>().PrimaryInventorySystem.UpdateUISlots();

        }

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
                // Si hay item para quemar entra
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
                //Si no hay item para quemar rompe el bucle
                yield return null;
            }

        }

    }

    public bool Run()
    {
        if (ContainWood())
        {
            isRun = true;
            lightEffect.enabled = true;
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
        lightEffect.enabled = false;
        StopAllCoroutines();
        fireParticle.Stop();
    }

}
