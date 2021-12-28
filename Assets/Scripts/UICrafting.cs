using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICrafting : MonoBehaviour
{
    public static UICrafting instance;

    public GameObject uiRecipe;
    public GameObject slotMaterial;
    public TextMeshProUGUI[] uiMaterial;
    public TextMeshProUGUI textMaterial;
    public CraftingRecipe recipe;
    public Button buttonCraft;


    private void Start()
    {
        instance = this;
        uiMaterial = slotMaterial.GetComponentsInChildren<TextMeshProUGUI>();
    }

    public void UpdateSlot()
    {
        if (recipe)
        {
            CleanText();          

            for (int i = 0; i < recipe.Material.Count; i++)
            {
                
                uiMaterial[i].text = recipe.Material[i].item.name + " " + recipe.Material[i].amount;

            }
        }
    }

    public void CleanText()
    {
        for (int i = 0; i < uiMaterial.Length; i++)
        {
            uiMaterial[i].text = "";

        }
    }

    public void ButtonCraft()
    {
        if (recipe)
        {
            recipe.Craft();
        }
        
    }

    

}





