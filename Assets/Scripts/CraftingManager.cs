using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Progress;
using System.Net;
using System.Linq;

public class CraftingManager : MonoBehaviour
{
    public static CraftingManager instance;

    private InventorySystem inventorySelected;

    private CraftingRecipe currentRecipe;

    private float remainingTime;

    public Queue<CraftingRecipe> craftQueue = new Queue<CraftingRecipe>();

    private bool isCrafting = false;

    private InventorySlot returnItem;

    private void Awake()
    {
        instance = this;
    }

    public bool CanCraft(CraftingRecipe recipe)
    {

        foreach (var item in recipe._requirements)
        {
            if (!PlayerInventoryHolder.instance.PrimaryInventorySystem.ContainIngredients(item.item, item.amount, out InventorySystem inv1))
            {
                if(!PlayerInventoryHolder.instance.SecondaryInventorySystem.ContainIngredients(item.item, item.amount, out InventorySystem inv2))
                {
                    CraftingUI.instance.ShowTextNoMaterial();
                    return false;
                }

            }
        }

        return true;

    }

    public void StartCraft(CraftingRecipe recipe)
    {
        if (CanCraft(recipe) == false)
        {
            return;
        }

        AddCraftingItem(recipe);
        Debug.Log(craftQueue.Count);
    }

    public void AddCraftingItem(CraftingRecipe recipe)
    {
        craftQueue.Enqueue(recipe);

        if (!isCrafting)
        {
            isCrafting = true;
            StartCoroutine(CraftCoroutine());
            StartCoroutine(Timer());

        }
    }

    public IEnumerator CraftCoroutine()
    {
        currentRecipe = craftQueue.Peek();

        if (!CanCraft(currentRecipe))
        {
            craftQueue.Dequeue();
            isCrafting = false;
            yield break;
        }

        foreach (var item in currentRecipe._requirements)
        {
            PlayerInventoryHolder.instance.RemoveToInventory(item.item, item.amount);
        }

        while (isCrafting)
        {
            CraftingUI.instance.ShowItemImage(currentRecipe);

            yield return new WaitForSeconds(currentRecipe.time);

            if (PlayerInventoryHolder.instance.PrimaryInventorySystem.AddItem(currentRecipe.result, currentRecipe.amountResult))
            {
                HudUI.instance.UpdateText(currentRecipe.result.itemName, currentRecipe.amountResult);
            }

            craftQueue.Dequeue();

            isCrafting = false;

        }

        if (craftQueue.Count > 0)
        {
            isCrafting = true;

            StartCoroutine(CraftCoroutine());
            remainingTime = currentRecipe.time;

        }
        else
        {
            CraftingUI.instance.HideItemImage();
            isCrafting = false;
        }

    }


    public IEnumerator Timer()
    {
        remainingTime = currentRecipe.time;

        while (remainingTime > 0)
        {
            remainingTime--;
            CraftingUI.instance.countdown.text = remainingTime.ToString();

            if (remainingTime < 0)
            {
                CraftingUI.instance.countdown.text = 0.ToString();
            }
            yield return new WaitForSeconds(1f);
        }

    }

    public void CancelCraft()
    {
        if (isCrafting)
        {
            isCrafting = false;
            remainingTime = 0;
            ReturnItems();
            StopAllCoroutines();
            CraftingUI.instance.countdown.text = "";
            CraftingUI.instance.HideItemImage();
            craftQueue.Dequeue();
        }
        if (craftQueue.Count > 0)
        {
            isCrafting= true;
            StartCoroutine(CraftCoroutine());
            StartCoroutine(Timer());
        }
    }

    private void ReturnItems()
    {
        // Obtenemos la cantidad de items necesarios para el crafting
        int requiredItems = currentRecipe._requirements.Count;

        // Iteramos sobre cada ingrediente
        for (int i = 0; i < requiredItems; i++)
        {
            // Obtenemos el ingrediente y su cantidad necesaria
            ItemObject ingredient = currentRecipe._requirements[i].item;
            int quantity = currentRecipe._requirements[i].amount;

            // Devolvemos los items al inventario del jugador
            
                PlayerInventoryHolder.instance.AddToInventory(ingredient, quantity);                  
        }


    }



}





