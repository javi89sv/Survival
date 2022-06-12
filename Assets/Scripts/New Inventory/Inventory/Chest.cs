using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Chest : InventoryHolder, IInterectable
{
    public static UnityAction<InventorySystem> OnChestInventoryDisplayRequested;

    public UnityAction<IInterectable> OnInteractionComplete { get; set; }

    public void Interact(Interactor interactor, out bool interactSucessful)
    {
        OnChestInventoryDisplayRequested?.Invoke(primaryInventorySystem);
        interactSucessful = true;

    }
    public void EndInteraction()
    {
        
    }

}
