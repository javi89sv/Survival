using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Build Object", menuName = "Build")]
public class BuildObject : ScriptableObject
{

    [SerializeField] string nameObject;
    public GameObject prefab;
    public int durability;

}
