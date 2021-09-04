using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA : MonoBehaviour
{
    public int health;
    public int damage;

    private float timerAttack;
    public float cooldownAttack;

    public float speed;
    public float rangeDetection;
    public float rangeAttack;

    public ParticleSystem particles;

    public bool isDead;

    public GameObject[] drop;

    GameObject player;

    Vector3 initialPosition;

    public void TakeDamage(int damage)
    {
        health -= damage;
    }


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        timerAttack += Time.deltaTime;

        Vector3 target = initialPosition;

        float dist = Vector3.Distance(player.transform.position, transform.position);

        if(dist < rangeDetection)
        {
            target = player.transform.position;
            float fixSpeed = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, fixSpeed);
            Vector3 position = player.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(position);
            transform.rotation = rotation;

        }

        if(health <= 0)
        {
            isDead = true;
            for(int i = 0; i < drop.Length; i++)
            {
                Instantiate(drop[i], transform.position, transform.rotation);
            }
            Destroy(this.gameObject);
        }

        if(dist < rangeAttack && timerAttack >= cooldownAttack)
        {
            Debug.Log("Attacking");
           // GameManager.instance.TakeDamage(damage);
            timerAttack = 0;
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeDetection);
    }
}
