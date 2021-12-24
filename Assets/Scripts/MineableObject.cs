using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum RequiredTool { Axe, Pick }

[Serializable]
public struct LootItems
{
    public GameObject[] listItem;
    public string itemName;
    [Range(1, 100)]
    public int spawnChance;
    public int minAmount;
    public int maxAmount;

}

public class MineableObject : MonoBehaviour
{
    public RequiredTool requiredTool;
    [Range(0, 1)]
    public float resistance;

    public GameObject destroyObjectSpawn;

    public Vector3 spawnPosition;

    public List<LootItems> loot;

    private int health;

    private int maxHealth = 100;

    private void Start()
    {
        health = maxHealth;
    }

    private void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }





}
