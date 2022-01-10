using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName ="LootData",menuName = "Loot Table")]
public class LootTable : ScriptableObject
{
    [Serializable]
    public struct Chance
    {
        public int chanceLoot;
        public GameObject prefabLoot;
        public int minAmount;
        public int maxAmount;
        public Vector3 SpawnOffset;
    }

    public Chance[] chance;

}
