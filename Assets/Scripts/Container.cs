using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Container : MonoBehaviourPun
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
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }

        if (health <= 0)
        {

            int numberItems = drop.Length;
            GameObject go = Instantiate(boxBroken, transform.position, transform.rotation);
            go.GetComponent<Rigidbody>().AddExplosionForce(forceBrokekBox, transform.position, 1f);
            PhotonNetwork.Instantiate("Consumables/" + drop[Random.Range(0, numberItems)].name, transform.position, transform.rotation);
            PhotonNetwork.Instantiate("Consumables/" + drop[Random.Range(0, numberItems)].name, transform.position, transform.rotation);
            PhotonNetwork.Destroy(this.gameObject);

        }
    }


}
