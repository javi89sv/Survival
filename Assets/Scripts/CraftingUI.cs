using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingUI : MonoBehaviour
{
    public static CraftingUI instance;

    public GameObject mainPanel;
    private bool isOpenMainPanel;

    public TextMeshProUGUI nameItem;
    public TextMeshProUGUI description;
    public TextMeshProUGUI countdown;

    public TextMeshProUGUI textIngredient1;
    public TextMeshProUGUI textamount1;
    public TextMeshProUGUI textTotal1;
    public TextMeshProUGUI textIngredient2;
    public TextMeshProUGUI textamount2;    
    public TextMeshProUGUI textTotal2;    
    public TextMeshProUGUI textIngredient3;
    public TextMeshProUGUI textamount3;
    public TextMeshProUGUI textTotal3;

    public TextMeshProUGUI noMaterial;

    private GameObject panelItems;
    private CraftingRecipe selectRecipe;

    [SerializeField] Image itemCraftingIcon;



    private void Awake()
    {
        instance = this;
        mainPanel.gameObject.SetActive(false);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            mainPanel.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Interactor.isInteraction = true;
            isOpenMainPanel = true;
        }

        if(isOpenMainPanel && Input.GetKeyDown(KeyCode.Escape))
        {
            mainPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Interactor.isInteraction = false;
            isOpenMainPanel = false;
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
        ShowIngredients();
    }

    public void ButtonCraft()
    {
        if (selectRecipe)
        {
            CraftingManager.instance.AddCraftingItem(selectRecipe);
        }

    }

    public void ShowIngredients()
    {
        var requirement = selectRecipe._requirements;

        var Amount1 = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventoryHolder>().PrimaryInventorySystem.GetTotal(requirement[0].item);
        var Amount2 = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventoryHolder>().SecondaryInventorySystem.GetTotal(requirement[0].item);
        var totalAmount = Amount1 + Amount2;

        textIngredient1.text = requirement[0].item.itemName;
        textamount1.text = requirement[0].amount.ToString();
        textTotal1.text = totalAmount.ToString();

        if(requirement.Count > 1)
        {
            Amount1 = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventoryHolder>().PrimaryInventorySystem.GetTotal(requirement[1].item);
            Amount2 = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventoryHolder>().SecondaryInventorySystem.GetTotal(requirement[1].item);
            totalAmount = Amount1 + Amount2;

            textIngredient2.text = requirement[1].item.itemName;
            textamount2.text = requirement[1].amount.ToString();
            textTotal2.text = totalAmount.ToString();
        }
        else
        {
            textIngredient2.text = "";
            textamount2.text = "";
        }
        if(requirement.Count > 2)
        {
            Amount1 = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventoryHolder>().PrimaryInventorySystem.GetTotal(requirement[2].item);
            Amount2 = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventoryHolder>().SecondaryInventorySystem.GetTotal(requirement[2].item);
            totalAmount = Amount1 + Amount2;

            textIngredient3.text = requirement[2].item.itemName;
            textamount3.text = requirement[2].amount.ToString();
            textTotal3.text = totalAmount.ToString();
        }
        else
        {
            textIngredient3.text = "";
            textamount3.text = "";
        }

        //foreach(var item in selectRecipe._requirements)
        //{
        //    textIngredient1.text = item.item.itemName;
        //    textamount1.text = item.amount.ToString();
        //}
        
    }

    public void TestQueue()
    {
        foreach (var item in CraftingManager.instance.craftQueue)
        {
            Debug.Log(item);
        }
    }

    public void ShowItemImage(CraftingRecipe recipe)
    {
        itemCraftingIcon.enabled = true;
        itemCraftingIcon.sprite = recipe.result.icon;
    }

    public void HideItemImage()
    {
        itemCraftingIcon.enabled = false;
    }

    public void ShowTextNoMaterial()
    {
        noMaterial.CrossFadeAlpha(1f, 0f, false);
        noMaterial.text = "Faltan Materiales";
        noMaterial.CrossFadeAlpha(0.0f, 3f, false);
    }


}
