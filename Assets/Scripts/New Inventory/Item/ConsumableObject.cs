using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food Object", menuName = "Inventory System/Items/Food")]
public class ConsumableObject : ItemObject
{

    public int restoreHealth;
    public int restoreHungry;
    public int restoreThirst;

    public void Awake()
    {
        type = ItemType.Consumable; 
    }

    public override void UseItem()
    {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        if(player.currentHealth + restoreHealth > player.maxHealth)
        {
            player.currentHealth = player.maxHealth;
        }
        else
        {
            player.currentHealth += restoreHealth;
        }  
        
        if(player.currentHungry + restoreHungry > player.maxHungry)
        {
            player.currentHungry = player.maxHungry;
        }
        else
        {
            player.currentHungry += restoreHungry;
        } 

        if(player.currentThirst + restoreThirst > player.maxThirst)
        {
            player.currentThirst = player.maxThirst;
        }
        else
        {
            player.currentThirst += restoreThirst;
        }
        
        Debug.Log("Usamos item");
    }

}
