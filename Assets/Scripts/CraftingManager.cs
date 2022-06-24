using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingManager : MonoBehaviour
{
    public static CraftingManager instance;

    public GameObject uiRecipe;
    public GameObject slotMaterial;
    public TextMeshProUGUI[] uiMaterial;
    public TextMeshProUGUI textMaterial;
    public TextMeshProUGUI textCraftingItem;
    public Image countDownBar;
    public CraftingRecipe recipe;
    public Button buttonCraft;

    float remainingTime;


    //private void Start()
    //{
    //    instance = this;
    //    uiMaterial = slotMaterial.GetComponentsInChildren<TextMeshProUGUI>();
    //}

    //public void UpdateSlot()
    //{
    //    if (recipe)
    //    {
    //        CleanText();

    //        for (int i = 0; i < recipe.Material.Count; i++)
    //        {

    //            uiMaterial[i].text = recipe.Material[i].item.name + " " + recipe.Material[i].amount;

    //        }
    //    }
    //}

    //public void CleanText()
    //{
    //    for (int i = 0; i < uiMaterial.Length; i++)
    //    {
    //        uiMaterial[i].text = "";

    //    }
    //}


    public bool CanCraft()
    {
        foreach (var item in recipe._requirements)
        {
            if (!PlayerInventoryHolder.instance.PrimaryInventorySystem.ContainIngredients(item.item, item.amount) || !PlayerInventoryHolder.instance.SecondaryInventorySystem.ContainIngredients(item.item, item.amount))
            {
                Debug.Log("FALSE");
                return false;

            }


        }
        Debug.Log("TRUE");
        return true;

    }

    //public void Craft()
    //{
    //    if (CanCraft() == true)
    //    {

    //        foreach (ItemAmount itemAmount in recipe.Material)
    //        {
    //            InventoryManager.instance.RemoveItemsCraft(itemAmount.item, itemAmount.amount);
    //        }
    //        textCraftingItem.text = recipe.name;
    //        remainingTime = recipe.time;
    //        StartCoroutine(Countdown(recipe.time));
    //        StartCoroutine(Timer());
    //    }
    //    else
    //    {
    //        Debug.Log("Not enought material");
    //    }

    //}

    //IEnumerator Countdown(float value)
    //{
    //    yield return new WaitForSeconds(value);
    //    Crafting();
    //    textCraftingItem.text = "";
    //}

    //IEnumerator Timer()
    //{
    //    while(remainingTime >= 0)
    //    {

    //        countDownBar.fillAmount = Mathf.InverseLerp(0, recipe.time, remainingTime);
    //        remainingTime--;
    //        yield return new WaitForSeconds(1f);
    //    }      
    //}

    //private void Crafting()
    //{

    //    GameObject go = Instantiate(recipe.result.prefab);
    //    go.GetComponent<MeshRenderer>().enabled = false;
    //    go.GetComponent<Collider>().enabled = false;
    //    go.GetComponent<Rigidbody>().isKinematic = true;
    //    InventoryManager.instance.AddItem(go, recipe.result, recipe.result.id, recipe.amountResult);
    //}

    public void Test()
    {
        CanCraft();
    }

}





