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
    public ParticleSystem particles;

    public Vector3 offsetSpawn = new Vector3(0f, 0.5f, 0f);

    public Loot[] loot;



    // Start is called before the first frame update
    void Start()
    {
        health = maxhealth;
        Debug.Log(loot.Length);
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

            for (int i = 0; i < loot.Length; i++)
            {

                GameObject lootPrefab = Instantiate(loot[i].lootItem, transform.position + offsetSpawn, Quaternion.identity);
                lootPrefab.GetComponent<InteractiveItem>().quantity = (int)Random.Range(loot[i].minSpawn, loot[i].maxSpawn);

            }

            Destroy(this.gameObject);

        }
    }


    public void TakeDamage(int damage)
    {
        health -= damage;

    }


}
