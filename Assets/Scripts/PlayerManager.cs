using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [HideInInspector]
    public float currentHealth, currentHungry, currentThirst;
    [Header("--Player Stats--")]
    public float maxHealth;
    public float maxHungry, maxThirst;
    public float healthIncreaseRate, drinkIncreaseRate, foodIncreaseRate;

    private PlayerController playerController;


    private void Awake()
    {
        instance = this;
        playerController = GetComponent<PlayerController>();

    }

    void Start()
    {

        currentHealth = maxHealth;
        currentHungry = maxHungry;
        currentThirst = maxThirst;


    }

    private void Update()
    {
        ManagerStatesPlayer();

    }


    public void TakeDamage(int damage)
    {

        currentHealth -= damage;
        Debug.Log(currentHealth);
        if (currentHealth < 0)
        {
            
            //MUERE

        }

    }

    private void ManagerStatesPlayer()
    {
        currentHungry -= foodIncreaseRate * Time.deltaTime;
        currentThirst -= drinkIncreaseRate * Time.deltaTime;

        if (currentHungry <= 0 || currentThirst < 0)
        {
            currentHealth -= healthIncreaseRate * Time.deltaTime;
        }

        if (currentThirst <= 0)
        {
            playerController.runSpeed = playerController.walkSpeed;
        }
        else
        {
            //playerController.runSpeed
        }

        if(currentHealth < 0)
        {
            currentHealth = 0;
        }
        if(currentHungry < 0)
        {
            currentHungry = 0;
        }
        if(currentThirst < 0)
        {
            currentThirst = 0;
        }




    }

}
