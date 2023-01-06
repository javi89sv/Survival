using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TypeEquiped
{
    Melee,
    Range
}

[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Equipment")]
public class EquipmentObject : ItemObject
{
    public TypeEquiped typeEquiped;

    public int atkBonus;
    public int defBonus;
    public int durability;
    public float fireRate;
    public float range;

    [Header("Farming")]
    [SerializeField] public int minFarmWood;
    [SerializeField] public int maxFarmWood;
    [SerializeField] public int minFarmMineral;
    [SerializeField] public int maxFarmMineral;

    [HideInInspector]
    public int farmWood;
    [HideInInspector]
    public int farmMineral;

    public override void UseItem()
    {
        //Equipar este item     
        Debug.Log("Equipamos " + this.name);
    }

    public int CalculateDamageWood()
    {
        farmWood = Random.Range(minFarmWood, maxFarmWood);
        return farmWood;
    }

    public int CalculateFarmMineral()
    {
        farmMineral = Random.Range(minFarmMineral, maxFarmMineral);
        return farmMineral;
    }

    private void Awake()
    {
        type = ItemType.Equipment;

    }
}
