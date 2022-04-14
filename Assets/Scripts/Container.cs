using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

[Serializable]
public struct LootContainer
{
    public GameObject lootItem;
    public int amount;
    public float minSpawn;
    public float maxSpawn;
    public Vector3 offsetSpawn;
}

public class Container : MonoBehaviour
{

    public int health;
    public GameObject[] drop;
    public GameObject boxBroken;
    public ParticleSystem particles;
    public float forceBrokenBox;
    public LootTable lootTable;

    public void TakeDamage(int damage)
    {
        health -= damage;

    }

    private void Update()
    {

        if (health <= 0)
        {
            if (boxBroken)
            {
                GameObject go = Instantiate(boxBroken, transform.position, transform.rotation);
                go.GetComponent<Rigidbody>().AddExplosionForce(forceBrokenBox, transform.position, 1f);
                Destroy(go, 3f);
            }
            
            LootSystem.instance.Loot(lootTable, transform.position);
            Destroy(this.gameObject);
            

        }
    }


}
