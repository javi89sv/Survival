using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum type { melee, ranged, axe, pickaxe }

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{

    public string name;
    public type type;
    public int damage;

    public float firerate;
    public float bloom;
    public float recoil;
    public float kickback;
    public float aimSpeed;
    public GameObject prefab;
    

}

