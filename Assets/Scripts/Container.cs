using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{

    public int health;
    public GameObject[] drop;
    public GameObject boxBroken;

    public ParticleSystem particles;

    public float forceBrokekBox;

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
            Instantiate(drop[Random.Range(0, numberItems)], transform.position, transform.rotation);
            Instantiate(drop[Random.Range(0, numberItems)], transform.position, transform.rotation);
            Destroy(this.gameObject);

        }
    }


}
