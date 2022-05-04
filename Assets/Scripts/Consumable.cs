using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    
   // public enum type { food, drink, kitMedical}
   // public type _type;

    public int health, food, drink;

    public void UseConsumable()
    {
        PlayerManager.instance.currentHealth += health;
        PlayerManager.instance.currentFood += food;
        PlayerManager.instance.currentDrink += drink;
    }



}
