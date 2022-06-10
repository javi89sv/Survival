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

    public GameObject menuInventory;
    public bool inventoryEnable;

    public GameObject menuCrafting;
    public GameObject menuItemInfo;
    public GameObject menuLoot;
    public TextMeshProUGUI textInfo;
    public Image imageInfo;


    void Start()
    {
        instance = this;
        //weaponManager = GameObject.FindGameObjectWithTag("WeaponManager");
        playerPrefab = GameObject.FindGameObjectWithTag("Player");

    }


    void Update()
    {
        

        //if (Input.GetKeyDown(KeyCode.Tab))
        //{
        //    inventoryEnable = !inventoryEnable;
        //}

        //if (inventoryEnable)
        //{

        //    menuInventory.SetActive(true);
        //    Cursor.lockState = CursorLockMode.None;

        //}
        //else
        //{

        //    menuInventory.SetActive(false);
        //    Cursor.lockState = CursorLockMode.Locked;
        //    textInfo.text = "";
        //    imageInfo.sprite = null;
        //    menuItemInfo.SetActive(false);

        //}

    }

    

    //Muestra en pantalla el objeto que recogemos
    public void UpdateText(string name, int amount)
    {
        counterText.CrossFadeAlpha(1f, 0f, false);
        counterText.text = "+ " + amount + " " + name;
        counterText.CrossFadeAlpha(0.0f, 5f, false);
    }





}
