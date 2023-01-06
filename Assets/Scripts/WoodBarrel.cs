using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct LootContainer
{
    public GameObject lootItem;
    public int amount;
    public float minSpawn;
    public float maxSpawn;
    public Vector3 offsetSpawn;
}

public class WoodBarrel : MonoBehaviour, IHitable
{

    public int health;
    public int maxHealth;
    public GameObject brokenPrefab;
    public ParticleSystem hitParticles;
    public LootTable lootTable;

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage, Vector3 pointHit)
    {
        health -= damage;
        hitParticles.transform.position = pointHit;
        hitParticles.Play();
    }

    public int CurrentHealth()
    {
        return health;
    } 

    public int MaxHealth()
    {
        return maxHealth;
    }

    private void Update()
    {

        if (health <= 0)
        {
            if (brokenPrefab)
            {
                GameObject go = Instantiate(brokenPrefab, transform.position, transform.rotation);
                go.GetComponent<Rigidbody>().AddExplosionForce(1f, transform.position, 1f);
                LootSystem.instance.Loot(lootTable, transform.position);
                Destroy(go, 3f);
            }
            
            Destroy(this.gameObject);
            
        }
    }


}
