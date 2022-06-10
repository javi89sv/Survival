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

    public TextMeshProUGUI namePanelText;

    private void Awake()
    {
        instance = this;
        chestInventoryPanel.gameObject.SetActive(false);
        playerInventoryPanel.gameObject.SetActive(false);
        furnaceInventoryPanel.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Furnace.OnFurnaceInventoryDisplayRequested += DisplayFurnaceInventory;
        PlayerInventoryHolder.OnPlayerInventoryDisplayRequested += DisplayPlayerInventory;
        Chest.OnChestInventoryDisplayRequested += DisplayInventory;
    }


    private void OnDisable()
    {
        Furnace.OnFurnaceInventoryDisplayRequested -= DisplayFurnaceInventory;
        PlayerInventoryHolder.OnPlayerInventoryDisplayRequested -= DisplayPlayerInventory;
        Chest.OnChestInventoryDisplayRequested -= DisplayInventory;
    }

    public void DisplayInventory(InventorySystem invToDisplay)
    {
        chestInventoryPanel.gameObject.SetActive(true);
        chestInventoryPanel.RefreshDynamicInventory(invToDisplay);
     
    }
    public void DisplayPlayerInventory(InventorySystem invToDisplay)
    {
        playerInventoryPanel.gameObject.SetActive(true);
        playerInventoryPanel.RefreshDynamicInventory(invToDisplay);
    }

    public void DisplayFurnaceInventory(InventorySystem invToDisplay)
    {
        furnaceInventoryPanel.gameObject.SetActive(true);
        furnaceInventoryPanel.RefreshDynamicInventory(invToDisplay);
    }

    public void RefreshUIChest(InventorySystem invToDisplay)
    {
        chestInventoryPanel.RefreshDynamicInventory(invToDisplay);
    }


    void Update()
    {

        if (chestInventoryPanel.gameObject.activeInHierarchy && Input.GetKey(KeyCode.Escape))
        {
            chestInventoryPanel.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Interactor.isInteraction = false;
        }
        if (playerInventoryPanel.gameObject.activeInHierarchy && Input.GetKey(KeyCode.Escape))
        {
            playerInventoryPanel.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            //Interactor.isInteraction = false;
        }
        if (furnaceInventoryPanel.gameObject.activeInHierarchy && Input.GetKey(KeyCode.Escape))
        {
            furnaceInventoryPanel.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Interactor.isInteraction = false;
        }

    }
}