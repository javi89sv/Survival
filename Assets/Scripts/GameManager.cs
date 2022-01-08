using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TextMeshProUGUI counterText;

    [HideInInspector]
    public GameObject weaponManager;
    public GameObject weapon;

    public GameObject playerPrefab;
    public Vector3 spawnPlayer;

    public bool isWeaponEquipped;

    // UI
    private bool inventoryEnable;
    public GameObject menuInventory;
    public GameObject menuCrafting;
    public GameObject menuItemInfo;
    public TextMeshProUGUI textInfo;
    public Image imageInfo;


    void Start()
    {
        instance = this;
        weaponManager = GameObject.FindGameObjectWithTag("WeaponManager");

    }


    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryEnable = !inventoryEnable;
        }

        if (inventoryEnable)
        {
            
            menuInventory.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Look>().ySensitivity = 0;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Look>().xSensitivity = 0;
        }
        else
        {
            
            menuInventory.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Look>().ySensitivity = 300;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Look>().xSensitivity = 300;
            textInfo.text = "";
            imageInfo.sprite = null;
            menuItemInfo.SetActive(false);
        }

    }

    

    //Muestra en pantalla el objeto que obtenemos
    public void UpdateText(string name, int amount)
    {
        counterText.CrossFadeAlpha(1f, 0f, false);
        counterText.text = "+ " + amount + " " + name;
        counterText.CrossFadeAlpha(0.0f, 5f, false);
    }





}
