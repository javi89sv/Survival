using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = ("RingElement"), menuName = ("RingMenu/Element"), order = 2)]
public class RingElement : ScriptableObject
{
    [Header ("General")]
    public string nameElement;
    public Sprite icon;
    public Ring nextRing;

    [Header ("Material Need")]
    public ItemObject itemObject;
    public int amount;

    [Header("Build Spawn")]
    public PlaceableObject buildObject;


}
