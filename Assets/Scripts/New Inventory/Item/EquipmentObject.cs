using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Equipment")]
public class EquipmentObject : ItemObject
{

    public int atkBonus;
    public int defBonus;
    public int durability;
    public float fireRate;

    [Header("Farming")]
    public int farmWood;
    public int farmMineral;


    public override void UseItem()
    {
        //Equipar este item     
        Debug.Log("Equipamos " + this.name);
    }

    private void Awake()
    {
        type = ItemType.Equipment;
    }
}
