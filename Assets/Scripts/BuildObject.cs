using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildObject : MonoBehaviour, IHitable
{

    public GameObject prefab;
    public int durability;

    public void TakeDamage(int damage, Vector3 pointHit)
    {
        durability -= damage;
        //particlesGather.transform.position = pointHit;
        //particlesGather.Play();
        Remove();
    }

    private void Remove()
    {
        if(durability <= 0)
        {
            Destroy(gameObject);
        }
    }

    public int Health()
    {
        return durability;
    }
}


