using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum typePlace { normal, edge }

[CreateAssetMenu(fileName = "New Placeable Object", menuName = "Inventory System/Items/Placeable")]
public class PlaceableObject : ItemObject
{
    public typePlace typePlace;

    public int durability;

    public override void UseItem()
    {
        Debug.Log("use Placeable!");
        PlaceObjectSystem.instance.currentGO = this;
        PlaceObjectSystem.instance.isBuilding = true;

    }

}
