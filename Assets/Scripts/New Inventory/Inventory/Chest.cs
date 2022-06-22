using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Chest : InventoryHolder, IInterectable
{
    public string textInfo;

    public static UnityAction<InventorySystem> OnChestInventoryDisplayRequested;

    public UnityAction<IInterectable> OnInteractionComplete { get; set; }

    public void Interact(Interactor interactor)
    {
        OnChestInventoryDisplayRequested?.Invoke(primaryInventorySystem);
        InventoryUIController.instance.namePanelText.text = nameContainer.ToUpper();
        PlayerInventoryHolder.OnPlayerInventoryDisplayRequested?.Invoke(PlayerInventoryHolder.instance.SecondaryInventorySystem);
        Interactor.isInteraction = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public string TextInfo()
    {
        return textInfo;
    }
}
