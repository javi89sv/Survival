using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingUI : MonoBehaviour
{

    public GameObject mainPanel;
    private bool isOpenMainPanel;

    public TextMeshProUGUI nameItem;
    public TextMeshProUGUI description;
    public TextMeshProUGUI countdown;

    private GameObject panelItems;
    private CraftingRecipe selectRecipe;

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isOpenMainPanel = !isOpenMainPanel;
        }

        if (isOpenMainPanel)
        {
            mainPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Interactor.isInteraction = true;
        }
        if (!isOpenMainPanel)
        {
            mainPanel.SetActive(false);
           // Cursor.lockState = CursorLockMode.Locked;
           // Cursor.visible = false;
           // Interactor.isInteraction = false;
        }

    }

    public void OpenPanel(GameObject panel)
    {
        if (panelItems)
        {
            if (panelItems.activeInHierarchy)
            {
                panelItems.SetActive(false);
            }
        }

        panel.SetActive(true);
        panelItems = panel;
    }

    public void SelectItem(CraftingRecipe recipe)
    {
        selectRecipe = recipe;
        nameItem.text = recipe.name.ToUpper();
        description.text = recipe.result.description.ToString();
    }

    public void ButtonCraft()
    {
        if (selectRecipe)
        {
            CraftingManager.instance.CanCraft(selectRecipe);
        }
        
    }



}
