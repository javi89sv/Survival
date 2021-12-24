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
    private bool craftingEnable;
    public GameObject menuInventory;
    public GameObject menuCrafting;


    void Start()
    {
        instance = this;
        weaponManager = GameObject.FindGameObjectWithTag("WeaponManager");
        Instantiate(playerPrefab, spawnPlayer, Quaternion.identity);
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
            //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().horizontalSpeed = 0;
            //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().verticalSpeed = 0;
        }
        else
        {
            menuInventory.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().horizontalSpeed = 500;
            //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().verticalSpeed = 500;
        }

    }

    

    //Muestra en pantalla el objeto que obtenemos
    public void UpdateText(string name, int amount)
    {
        counterText.CrossFadeAlpha(1f, 0f, false);
        counterText.text = "+ " + amount + " " + name;
        counterText.CrossFadeAlpha(0.0f, 5f, false);
    }

    //public void SelectWeapon(int id)
    //{
    //    weaponManager = GameObject.FindGameObjectWithTag("WeaponManager");

    //    int allWeapons = weaponManager.transform.childCount;

    //    for (int i = 0; i < allWeapons; i++)
    //    {

    //        if (weaponManager.transform.GetChild(i).gameObject.GetComponent<InteractiveItem>().id == id)
    //        {
    //            weapon = weaponManager.transform.GetChild(i).gameObject;
    //        }

    //    }

    //}



}
