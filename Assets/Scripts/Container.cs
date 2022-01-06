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

    public float forceBrokekBox;

    public LootContainer[] lootContainer;

    public void TakeDamage(int damage)
    {
        health -= damage;

    }

    private void Update()
    {

        if (health <= 0)
        {

            int numberItems = drop.Length;
            GameObject go = Instantiate(boxBroken, transform.position, transform.rotation);
            go.GetComponent<Rigidbody>().AddExplosionForce(forceBrokekBox, transform.position, 1f);

            for (int i = 0; i < lootContainer.Length; i++)
            {

                GameObject lootPrefab = Instantiate(lootContainer[i].lootItem, transform.position + lootContainer[i].offsetSpawn, Quaternion.identity);
                lootPrefab.GetComponent<InteractiveItem>().quantity = (int)Random.Range(lootContainer[i].minSpawn, lootContainer[i].maxSpawn);

            }

            Destroy(this.gameObject);

        }
    }


}
