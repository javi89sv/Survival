using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyIA : MonoBehaviour
{
    public int health;
    public int maxhealth;
    public int damage;

    private float timerAttack;
    public float cooldownAttack;

    public float speed;
    public float rangeDetection;
    public float rangeAttack;

    public HealthBar healthBar;

    public ParticleSystem particles;

    public bool isDead;

    public GameObject[] drop;

    Player player;
    GameObject playerPrefab;

    Vector3 initialPosition;

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.UpdateHealth((float)health / (float)maxhealth);
    }


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        playerPrefab = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        timerAttack += Time.deltaTime;

        Vector3 target = initialPosition;

        float dist = Vector3.Distance(playerPrefab.transform.position, transform.position);

        if (dist < rangeDetection)
        {
            target = playerPrefab.transform.position;
            float fixSpeed = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, playerPrefab.transform.position, fixSpeed);
            Vector3 position = playerPrefab.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(position);
            transform.rotation = rotation;

        }

        if (health <= 0)
        {
            isDead = true;
            for (int i = 0; i < drop.Length; i++)
            {
                Instantiate(drop[i], transform.position, transform.rotation);
            }
            Destroy(this.gameObject);
        }

        if (dist < rangeAttack && timerAttack >= cooldownAttack)
        {
            Debug.Log("Attacking");
            playerPrefab.GetComponent<Player>().TakeDamage(damage);
            timerAttack = 0;
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeDetection);
    }
}
