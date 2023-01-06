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
    public int amount;
    public int maxAmount;
    public ParticleSystem particlesGather;
    public GameObject destroyPrefab;
    public LootTable lootTable;

    // Start is called before the first frame update
    void Start()
    {
        amount = maxAmount;
    }


    private void Die()
    {
        if (amount <= 0)
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
        amount -= damage;
        particlesGather.transform.position = pointHit;
        particlesGather.Play();
        Die();

    }

    public int CurrentHealth()
    {
        return amount;
    }

    public int MaxHealth()
    {
        return maxAmount;
    }

}
