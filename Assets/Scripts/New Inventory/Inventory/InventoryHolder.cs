using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryHolder : MonoBehaviour
{
    public string nameContainer;

    [SerializeField] protected InventorySystem primaryInventorySystem;

    public InventorySystem PrimaryInventorySystem => primaryInventorySystem;

    public static UnityAction<InventorySystem> OnDynamicInventoryDisplayRequested;

    protected virtual void Awake()
    {
       // primaryInventorySystem = new InventorySystem(sizeInventory); // Crea un nuevo sistema de inventario con el tamaño que le pasamos

    }
}
