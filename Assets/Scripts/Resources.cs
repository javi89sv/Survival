using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum typeResources
{
    wood,stone,iron
}

[Serializable]
public struct Loot
{
    public GameObject lootItem;
    public float minSpawn;
    public float maxSpawn;
    public Vector3 spawn;
}

public class Resources : MonoBehaviour
{
    public typeResources typeResources;
    public int hardness;
    public float health;
    public float maxhealth;
    public GameObject healthBar;
    public ParticleSystem particlesGather;
    public ParticleSystem particlesDestroy;

    public LootTable lootTable;

    // Start is called before the first frame update
    void Start()
    {
        health = maxhealth;
    }

    public float GetHealth()
    {
        return health / maxhealth;

    }


    void Update()
    {
        if (health <= 0)
        {
            LootSystem.instance.Loot(lootTable,transform.position);
            particlesDestroy.Play();
            Destroy(this.gameObject);
        }
    }


    public void TakeDamage(int damage)
    { 
        health -= damage;
    }


}
