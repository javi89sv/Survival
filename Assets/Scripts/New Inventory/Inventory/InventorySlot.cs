using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class InventorySlot
{

    public ItemObject item;
    public int amount;

    public InventorySlot(ItemObject _item, int _amount) // Constructor para hacer un slot ocupado
    {

        item = _item;
        amount = _amount;
    }

    public InventorySlot() // Constructor para hacer un slot vacio
    {
        ClearSlot();
    }

    public bool CheckStack(int amountToAdd, out int amountRemaining) // Habria sufuciente espacio en el stack para añadir el valor que le estamos pasando?
    {
        amountRemaining = item.maxStackSize - amount;
        return CheckStack(amountToAdd);
    }

    public bool CheckStack(int amountToAdd) // Supera el stack maximo, la suma del stack y el valor que le estamos pasando?
    {
        if (item == null || item != null && amount + amountToAdd <= item.maxStackSize)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AssignItem(InventorySlot invSlot) // Asignar un item a un slot
    {
        if (item == invSlot.item) // Si la ranura ya contiene el mismo item, añadimos el stack
        {
            AddToStack(invSlot.amount);
        }
        else // Si la ranura no contiene el item, sobreescribimos el item y el stack que le estamos pasando
        {
            item = invSlot.item;
            amount = 0;
            AddToStack(invSlot.amount);
        }
    }

    public void AddToStack(int value) // Añadimos el valor al stack
    {
        amount += value;
    }

    public void RemoveFromStack(int value) // Restamos el valor al stack
    {
        amount -= value;
    }

    public void ClearSlot() // Limpiamos el slot
    {
        item = null;
        amount = 0;
    }


    public void UpdateSlot(ItemObject _item, int _amount) // Actualiza valores del slot
    {
        item = _item;
        amount = _amount;
    }

    public bool SplitStack(out InventorySlot splitStack) // Dividimos el stack actual a la mitad (redondeado arriba)
    {
        if (amount <= 1)
        {
            splitStack = null;
            return false;
        }

        int halfStack = Mathf.RoundToInt(amount / 2);
        RemoveFromStack(halfStack);

        splitStack = new InventorySlot(item, halfStack);

        return true;
    }
}
