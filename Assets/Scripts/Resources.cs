using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Resources : MonoBehaviour
{
    public string type;
    public int quantity;
    public int hardness;
    private float health;
    public float maxhealth;
    public GameObject[] drop;
    public GameObject healthBar;
    public GameObject typeResource;

    public ParticleSystem particles;

    public Vector3 offset = new Vector3(0f, 0.5f, 0f);



    // Start is called before the first frame update
    void Start()
    {
        
        health = maxhealth;
        healthBar.gameObject.SetActive(true);
    }

    public float GetHealth()
    {
        return health / maxhealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (quantity <= 0)
        {
            Destroy(gameObject);
        }


        if (health <= 0)
        {

            for (int i = 0; i < drop.Length; i++)
            {
                Instantiate(drop[i], transform.position + offset, Quaternion.identity);
            }

            Destroy(this.gameObject);

        }
    }

    public void Gathering(int amount)
    {
        if (quantity > 0)
        {
            quantity -= amount;
        }
    } 
    

    public void TakeDamage(int damage)
    {
        health -= damage;

    }    
    

}
