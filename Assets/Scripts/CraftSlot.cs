using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftSlot : MonoBehaviour
{

    public CraftingRecipe recipe;

    public void SelectRecipe()
    {
        UICrafting.instance.recipe = recipe;
        UICrafting.instance.UpdateSlot();
    }

    private void Start()
    {
        //Si hay recipe, muestra el nombre
        if (this.recipe != null)
        {
            this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = recipe.name;
        }
        else
        {
            this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }


    }

}
