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

    //public void Use()
    //{
    //    restoreHealth += Player.instance.healthPlayer;
    //    restoreHungry += Player.instance.hungryPlayer;
    //    restoreThirst += Player.instance.thirstPlayer;
    //}
}
