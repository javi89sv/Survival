using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public ItemObject objectSpawnPlayer;

    private PlayerInventoryHolder inventoryHolder;

    private void Awake()
    {
        inventoryHolder = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventoryHolder>();
    }
    void Start()
    {
        instance = this;

        inventoryHolder.PrimaryInventorySystem.AddItem(objectSpawnPlayer, 1);

    }


    void Update()
    {
        

    }




}
