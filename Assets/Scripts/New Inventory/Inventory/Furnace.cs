using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Furnace : InventoryHolder, IInterectable
{
    public string textInfo;
    public UnityAction<IInterectable> OnInteractionComplete { get; set; }

    public static UnityAction<InventorySystem> OnFurnaceInventoryDisplayRequested;

    public void Interact(Interactor interactor)
    {
        OnFurnaceInventoryDisplayRequested?.Invoke(primaryInventorySystem);
        InventoryUIController.instance.furnaceInventoryPanel.GetComponent<FurnaceUI>().furnaceCurrent = this;
        InventoryUIController.instance.namePanelText.text = nameContainer.ToUpper();
        PlayerInventoryHolder.OnPlayerInventoryDisplayRequested?.Invoke(PlayerInventoryHolder.instance.SecondaryInventorySystem);
        Interactor.isInteraction = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    string IInterectable.TextInfo()
    {
        return textInfo;
    }
}
