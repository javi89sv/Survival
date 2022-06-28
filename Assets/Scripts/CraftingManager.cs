using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingManager : MonoBehaviour
{
    public static CraftingManager instance;

    private InventorySystem inventorySelected;
    private CraftingRecipe currentRecipe;

    private float remainingTime;
    public TextMeshProUGUI textTimer;

    public Queue<CraftingRecipe> craftQueue = new Queue<CraftingRecipe>();
    private bool isCrafting = false;

    private IEnumerator coroutineCraft;
    private IEnumerator coroutineTimer;

    private void Awake()
    {
        instance = this;

    }

    private void Start()
    {
        coroutineCraft = CraftItem();
        coroutineTimer = Timer();
    }

    public bool CanCraft(CraftingRecipe recipe)
    {

        foreach (var item in recipe._requirements)
        {
            if (PlayerInventoryHolder.instance.PrimaryInventorySystem.ContainIngredients(item.item, item.amount, out InventorySystem inv1))
            {
                inventorySelected = inv1;
                Craft(recipe, inventorySelected);
                return true;

            }
            else if (PlayerInventoryHolder.instance.SecondaryInventorySystem.ContainIngredients(item.item, item.amount, out InventorySystem inv2))
            {
                inventorySelected = inv2;
                Craft(recipe, inventorySelected);
                return true;
            }

        }

        Debug.Log("Not enought material");
        return false;
    }

    public void Craft(CraftingRecipe recipe, InventorySystem inventory)
    {

        foreach (var item in recipe._requirements)
        {
            inventory.RemoveItems(item.item, item.amount);
        }

        // StartCoroutine("Countdown");
        StartCoroutine(Timer());

    }

    IEnumerator Countdown()
    {
        currentRecipe = craftQueue.Dequeue();
        yield return new WaitForSeconds(currentRecipe.time);
        Crafting();
        isCrafting = false;
        if (craftQueue.Count > 0)
        {
            CanCraft(currentRecipe);
        }
    }

    private void Crafting()
    {
        var inventoryHolder = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventoryHolder>();

        if (!inventoryHolder)
        {
            //Drop item
            return;
        }
        if (inventoryHolder.AddToInventory(currentRecipe.result, currentRecipe.amountResult))
        {

            HudUI.instance.UpdateText(currentRecipe.result.itemName, currentRecipe.amountResult);

        }
    }

    public IEnumerator Timer()
    {
        remainingTime = currentRecipe.time;

        while (remainingTime > 0)
        {
            remainingTime--;
            textTimer.text = remainingTime.ToString();
            if (remainingTime < 0)
            {
                textTimer.text = 0.ToString();
            }
            yield return new WaitForSeconds(1f);
        }

    }

    public void AddCraftingItem(CraftingRecipe recipe)
    {
        craftQueue.Enqueue(recipe);

        if (!isCrafting)
        {
            isCrafting = true;
            StartCoroutine(CraftItem());
        }
    }

    public IEnumerator CraftItem()
    {
        if (craftQueue.Count == 0)
        {
            isCrafting = false;
            yield break;
        }

        currentRecipe = craftQueue.Dequeue();

        if (!CanCraft(currentRecipe))
        {
            craftQueue.Clear();
            isCrafting = false;
            yield break;
        }

        CraftingUI.instance.ShowItemImage(currentRecipe);
        yield return new WaitForSeconds(currentRecipe.time * 1.1f);


        Crafting();
        CraftingUI.instance.HideItemImage();

        if (craftQueue.Count > 0)
        {
            yield return StartCoroutine(CraftItem());
        }
        else
        {
            isCrafting = false;
        }

    }

    public void CancelCraft()
    {
        if (isCrafting)
        {
            StopCoroutine(coroutineCraft);
            StopCoroutine(coroutineTimer);
            textTimer.text = "";
            isCrafting = false;
        }
        if (craftQueue.Count > 0)
        {
            StartCoroutine(CraftItem());
        }
    }



}





