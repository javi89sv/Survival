using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct ItemAmount
{
    public Item item;
    [Range(1, 999)]
    public int amount;

}


[CreateAssetMenu]
public class CraftingRecipe : ScriptableObject
{
    public List<ItemAmount> Material;
    public Item result;
    public int amountResult;

    public float time;



    private bool CanCraft()
    {
        foreach (ItemAmount itemAmount in Material)
        {
            if (!InventoryManager.instance.ContainItem(itemAmount.item, itemAmount.amount))
            {
                return false;
            }

        }
        return true;


    }

    public void Craft()
    {
        if (CanCraft() == true)
        {
            

            foreach (ItemAmount itemAmount in Material)
            {
                InventoryManager.instance.RemoveItem(itemAmount.item, itemAmount.amount);
            }
            Crafting();
        }
        else
        {
            Debug.Log("not enought material");
        }

    }

    private void Crafting()
    {
        
        GameObject go = Instantiate(result.prefab);
        InventoryManager.instance.AddItem(go, result, result.id, amountResult);
    }




}














