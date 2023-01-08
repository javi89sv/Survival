using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUIController : MonoBehaviour
{
    public static InventoryUIController instance;

    public DynamicInventoryDisplay chestInventoryPanel;
    public DynamicInventoryDisplay furnaceInventoryPanel;
    public DynamicInventoryDisplay playerInventoryPanel;
    public DynamicInventoryDisplay lootBoxInventoryPanel;

    [SerializeField] public GameObject panelInventoryPlayer;
    [SerializeField] public GameObject panelChestInventory;
    [SerializeField] public GameObject panelFurnaceInventory;

    public TextMeshProUGUI namePanelText;

    private void Awake()
    {
        instance = this;

       panelChestInventory.SetActive(false);
       panelInventoryPlayer.SetActive(false);  
       panelFurnaceInventory.SetActive(false);

    }

    private void OnEnable()
    {

        Furnace.OnFurnaceInventoryDisplayRequested += DisplayFurnaceInventory;
        PlayerInventoryHolder.OnPlayerInventoryDisplayRequested += DisplayPlayerInventory;
        Chest.OnChestInventoryDisplayRequested += DisplayInventory;
        BoxLoot.OnBoxInventoryDisplayRequested += DisplayInventory;
    }


    private void OnDisable()
    {
        Furnace.OnFurnaceInventoryDisplayRequested -= DisplayFurnaceInventory;
        PlayerInventoryHolder.OnPlayerInventoryDisplayRequested -= DisplayPlayerInventory;
        Chest.OnChestInventoryDisplayRequested -= DisplayInventory;
        BoxLoot.OnBoxInventoryDisplayRequested -= DisplayInventory;
    }

    public void UpdateDisplay(InventorySystem inventory)
    {
        
    }

    public void DisplayInventory(InventorySystem invToDisplay)
    {
        panelChestInventory.gameObject.SetActive(true);
        chestInventoryPanel.RefreshDynamicInventory(invToDisplay);

    }
    public void DisplayPlayerInventory(InventorySystem invToDisplay)
    {
        panelInventoryPlayer.gameObject.SetActive(true);
        playerInventoryPanel.RefreshDynamicInventory(invToDisplay);
    }

    public void DisplayFurnaceInventory(InventorySystem invToDisplay)
    {
        panelFurnaceInventory.gameObject.SetActive(true);
        furnaceInventoryPanel.RefreshDynamicInventory(invToDisplay);
    }

    public void RefreshUIChest(InventorySystem invToDisplay)
    {
        chestInventoryPanel.RefreshDynamicInventory(invToDisplay);
    }
    public void RefreshUIFurnace(InventorySystem invToDisplay)
    {
        furnaceInventoryPanel.RefreshDynamicInventory(invToDisplay);
    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            panelChestInventory.SetActive(false);
            panelInventoryPlayer.SetActive(false);
            panelFurnaceInventory.SetActive(false);
            HudUI.instance.panelInfo.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Interactor.isInteraction = false;

        }

    }
}
