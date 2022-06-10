using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventoryHolder : InventoryHolder
{
    public static PlayerInventoryHolder instance;

    [SerializeField] protected int secondaryInventorySize;
    [SerializeField] protected InventorySystem secondaryInventorySystem;

    public InventorySystem SecondaryInventorySystem => secondaryInventorySystem;

    public static UnityAction<InventorySystem> OnPlayerInventoryDisplayRequested;

    protected override void Awake()
    {
        instance = this;

        base.Awake();

        secondaryInventorySystem = new InventorySystem(secondaryInventorySize);
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            OnPlayerInventoryDisplayRequested?.Invoke(secondaryInventorySystem);
            Interactor.isInteraction = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public bool AddToInventory(ItemObject data, int amount)
    {
        if (primaryInventorySystem.AddItem(data, amount))
        {
            return true;
        }
        else if (secondaryInventorySystem.AddItem(data, amount))
        {
            return true;
        }

        return false;
    }
}
