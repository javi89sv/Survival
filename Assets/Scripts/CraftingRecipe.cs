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
            if (!Inventory.instance.ContainItem(itemAmount.item, itemAmount.amount))
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
                Inventory.instance.RemoveItem(itemAmount.item, itemAmount.amount);
            }
            Debug.Log("CRAFT ITEM");
            GameObject go = Instantiate(result.prefab);
            Inventory.instance.AddItem(go, result, result.id, amountResult, go.GetComponent<Item>().maxDegradable);
        }
        else
        {
            Debug.Log("not enought material");
        }

    }


    //IEnumerator CraftTimer(float time)
    //{
    //    yield return new WaitForSeconds(time);
    //    Debug.Log("CRAFT ITEM");
    //    Inventory.instance.RemoveItem(item, amount);
    //    GameObject go = Instantiate(result.prefab);
    //    Inventory.instance.AddItem(go, result, result.id, amountResult);

    //}

}














