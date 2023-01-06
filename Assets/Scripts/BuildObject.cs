using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildObject : MonoBehaviour, IHitable
{

    public int durability;
    public int maxDurability;

    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private GameObject destroyPrefab;

    private void Start()
    {
        durability = maxDurability;
    }

    public void TakeDamage(int damage, Vector3 pointHit)
    {
        durability -= damage;
        hitEffect.transform.position = pointHit;
        Remove();
    }

    private void Remove()
    {
        if(durability <= 0)
        {
            Destroy(gameObject);
        }
    }

    public int CurrentHealth()
    {
        return durability;
    }

    public int MaxHealth()
    {
        return maxDurability;
    }
}


