using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningSystem : MonoBehaviour
{

    public ItemObject wood;
    public ItemObject[] itemAccepted;
    public ItemObject coal;

    public bool isRun;

    public ParticleSystem fireParticle;

    public float speedBurn;

    private InventorySlot gettedItem;

    public bool ContainItems()
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
        while(gettedItem.amount > 0)
        {
            yield return new WaitForSeconds(speedBurn);
            gettedItem.RemoveFromStack(3);
            this.GetComponent<Furnace>().PrimaryInventorySystem.AddItem(coal,1);
            this.GetComponent<Furnace>().PrimaryInventorySystem.UpdateUISlots();

        }
        gettedItem.ClearSlot();
        InventoryUIController.instance.RefreshUIFurnace(this.GetComponent<Furnace>().PrimaryInventorySystem);
        isRun = false;
        Stop();

    }

    public bool Run()
    {
        if (ContainItems())
        {
            Debug.Log("Start Corrutina");
            StartCoroutine("Burn");
            fireParticle.Play();
            isRun = true;
            return true;
        }
        
        return false;

    }

    public void Stop()
    {
        StopAllCoroutines();
        fireParticle.Stop();
    }
}
