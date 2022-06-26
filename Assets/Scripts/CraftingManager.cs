using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingManager : MonoBehaviour
{
    public static CraftingManager instance;

    private InventorySystem inventorySelected;
    private CraftingRecipe recipeSelected;

    private float remainingTime;


    private void Awake()
    {
        instance = this;
    }

    public bool CanCraft(CraftingRecipe recipe)
    {


        foreach (var item in recipe._requirements)
        {
            if (PlayerInventoryHolder.instance.PrimaryInventorySystem.ContainIngredients(item.item, item.amount, out InventorySystem inv1))
            {
                inventorySelected = inv1;
                Craft(recipe, inventorySelected);
                Debug.Log("Can Craft!");
                return true;

            }
            else if (PlayerInventoryHolder.instance.SecondaryInventorySystem.ContainIngredients(item.item, item.amount, out InventorySystem inv2))
            {
                inventorySelected = inv2;
                Craft(recipe, inventorySelected);
                Debug.Log("Can Craft!");
                return true;
            }


        }

        Debug.Log("Not enought material");
        return false;
    }

    public void Craft(CraftingRecipe recipe, InventorySystem inventory)
    {
        recipeSelected = recipe;
        foreach (var item in recipe._requirements)
        {
            inventory.RemoveItems(item.item, item.amount);
        }

        StartCoroutine("Countdown");

    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(recipeSelected.time);
        Crafting();
    }

    private void Crafting()
    {
        var inventoryHolder = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventoryHolder>();

        if (!inventoryHolder)
        {
            //Drop item
            return;
        }
        if (inventoryHolder.AddToInventory(recipeSelected.result, recipeSelected.amountResult))
        {
            HudUI.instance.UpdateText(recipeSelected.result.itemName, recipeSelected.amountResult);

        }
    }

    public IEnumerator Timer(TextMeshProUGUI text)
    {
        remainingTime = recipeSelected.time;

        while(remainingTime >= 0)
        {

        }
        remainingTime--;
        text.text = remainingTime.ToString();
        yield return new WaitForSeconds(1f);
    }


}





