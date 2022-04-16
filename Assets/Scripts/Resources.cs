using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using Random = UnityEngine.Random;

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

    public int hardness;
    public float health;
    public float maxhealth;
    public GameObject healthBar;
    public ParticleSystem particlesGather;
    public ParticleSystem particlesDestroy;

    public LootTable lootTable;

    private int penalty;

    // Start is called before the first frame update
    void Start()
    {
        health = maxhealth;
    }

    public float GetHealth()
    {
        return health / maxhealth;

    }

    // Update is called once per frame
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
