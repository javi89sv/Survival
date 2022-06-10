using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Furnace : InventoryHolder, IInterectable
{
   
    public UnityAction<IInterectable> OnInteractionComplete { get; set; }

    public static UnityAction<InventorySystem> OnFurnaceInventoryDisplayRequested;

    public void Interact(Interactor interactor, out bool interactSucessful)
    {
        OnFurnaceInventoryDisplayRequested?.Invoke(primaryInventorySystem);
        InventoryUIController.instance.furnaceInventoryPanel.GetComponent<FurnaceUI>().furnaceCurrent = this;
        interactSucessful = true;
    }

    public void EndInteraction()
    {
        throw new System.NotImplementedException();
    }

}
