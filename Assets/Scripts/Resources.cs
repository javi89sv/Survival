using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Resources : MonoBehaviour
{

    public int hardness;
    public float health;
    public float maxhealth;
    public GameObject[] drop;
    public GameObject healthBar;

    public ParticleSystem particles;

    public Vector3 offsetSpawn= new Vector3(0f, 0.5f, 0f);



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

            for (int i = 0; i < drop.Length; i++)
            {

                GameObject loot = Instantiate(drop[i], transform.position + offsetSpawn, Quaternion.identity);
                loot.GetComponent<InteractiveItem>().quantity = Random.Range(50, 100);
            }

            Destroy(this.gameObject);

        }
    }


    public void TakeDamage(int damage)
    {
        health -= damage;

    }    
    

}
