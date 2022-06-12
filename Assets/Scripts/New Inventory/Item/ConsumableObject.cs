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
        player.currentHealth += restoreHealth;
        player.currentFood += restoreHungry;
        player.currentDrink += restoreThirst;
        Debug.Log("Usamos item");
    }

}
