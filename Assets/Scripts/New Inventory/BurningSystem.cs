using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningSystem : MonoBehaviour
{

    public ItemObject wood;
    public ItemObject[] itemAccepted;
    public ItemObject coal;

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
        while(gettedItem.amount >= 1f)
        {
            yield return new WaitForSeconds(speedBurn);
            gettedItem.RemoveFromStack(3);
            this.GetComponent<Furnace>().PrimaryInventorySystem.AddItem(coal,3);
            this.GetComponent<Furnace>().PrimaryInventorySystem.UpdateUISlots();

        }
        gettedItem.ClearSlot();
        InventoryUIController.instance.RefreshUIChest(this.GetComponent<Chest>().PrimaryInventorySystem);
        Run();

    }

    public bool Run()
    {
        if (ContainItems())
        {
            Debug.Log("Start Corrutina");
            StartCoroutine("Burn");
            fireParticle.Play();
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
