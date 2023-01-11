using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoxLoot : InventoryHolder, IInterectable
{
    [Serializable]
    public struct RandomItem
    {
        public int chanceLoot;
        public ItemObject prefabLoot;
        public int minAmount;
        public int maxAmount;
        public Vector3 SpawnOffset;
    }

    public RandomItem[] loot;

    public string textInfo;

    public static UnityAction<InventorySystem> OnBoxInventoryDisplayRequested;

    private void Start()
    {
        RandomLoot();
    }

    private void Update()
    {
        if (this.PrimaryInventorySystem.CheckEmpty())
        {
            Destroy(gameObject, 3f);
        }
    }

    public void Interact(Interactor interactor)
    {
        OnBoxInventoryDisplayRequested?.Invoke(primaryInventorySystem);
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

    public void RandomLoot()
    {
        foreach(var slot in loot)
        {
            this.PrimaryInventorySystem.AddItem(slot.prefabLoot, UnityEngine.Random.Range(slot.minAmount,slot.maxAmount));
        }
    }



}
