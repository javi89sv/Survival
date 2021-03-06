using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum typeResources
{
    wood, mineral
}

[Serializable]
public struct Loot
{
    public GameObject lootItem;
    public float minSpawn;
    public float maxSpawn;
    public Vector3 spawn;
}

public class Resources : MonoBehaviour, IHitable
{
    public typeResources typeResources;
    public int hardness;
    public int health;
    public int maxhealth;
    public ParticleSystem particlesGather;
    public GameObject destroyPrefab;
    public LootTable lootTable;

    // Start is called before the first frame update
    void Start()
    {
        health = maxhealth;
    }


    private void Die()
    {
        if (health <= 0)
        {
            if (destroyPrefab)
            {
                GameObject go = Instantiate(destroyPrefab, transform.position, transform.rotation);
                go.GetComponent<Rigidbody>().AddExplosionForce(1f, transform.position, 1f);
                LootSystem.instance.Loot(lootTable, transform.position);
                Destroy(go, 3f);
            }

            Destroy(this.gameObject);
        }
    }

    public void TakeDamage(int damage, Vector3 pointHit)
    {
        health -= damage;
        particlesGather.transform.position = pointHit;
        particlesGather.Play();
        Die();

    }

    public int Health()
    {
        return health;
    }

}
