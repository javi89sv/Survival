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

    public float rangeDetection;
    public float rangeAttack;

    public float moveSpeed = 0.2f;

    Vector3 stopPosition;

    private float walkTime;
    private float walkCounter;
    private float waitTime;
    private float waitCounter;

    int WalkDirection;

    public bool isWalking;
    public bool isDead;

    public HealthBar healthBar;

    public ParticleSystem particles;

    public LootTable lootTable;

    public GameObject healthImage;

    GameObject playerPrefab;
    Rigidbody rb;

    Vector3 initialPosition;

    public void TakeDamage(int damage)
    {
        health -= damage;
      //  healthBar.UpdateHealth((float)health / (float)maxhealth);
    }


    void Start()
    {
        walkTime = Random.Range(3, 6);
        waitTime = Random.Range(5, 7);

        waitCounter = waitTime;
        walkCounter = walkTime;

        ChooseDirection();

        playerPrefab = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        //ATTACKING

        timerAttack += Time.deltaTime;

        Vector3 target = initialPosition;

        float dist = Vector3.Distance(playerPrefab.transform.position, transform.position);

        if (dist < rangeDetection && !isDead)
        {
            target = playerPrefab.transform.position;
            float fixSpeed = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, playerPrefab.transform.position, fixSpeed);
            Vector3 position = playerPrefab.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(position);
            transform.rotation = rotation;

        }

        if (health <= 0)
        {
            isDead = true;
            LootSystem.instance.Loot(lootTable, transform.position);
            health = 1;
            healthImage.SetActive(false);
            Destroy(this.gameObject, 3f);
        }

        if (dist < rangeAttack && timerAttack >= cooldownAttack)
        {
            Debug.Log("Attacking");
            playerPrefab.GetComponent<PlayerManager>().TakeDamage(damage);
            timerAttack = 0;
        }

        //MOVEMENT
        if (isWalking)
        {

            walkCounter -= Time.deltaTime;

            switch (WalkDirection)
            {
                case 0:
                    transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                    break;
                case 1:
                    transform.localRotation = Quaternion.Euler(0f, 90, 0f);
                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                    break;
                case 2:
                    transform.localRotation = Quaternion.Euler(0f, -90, 0f);
                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                    break;
                case 3:
                    transform.localRotation = Quaternion.Euler(0f, 180, 0f);
                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                    break;
            }

            if (walkCounter <= 0)
            {
                stopPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                isWalking = false;
                //stop movement
                transform.position = stopPosition;
                //reset the waitCounter
                waitCounter = waitTime;
            }

        }
        else
        {

            waitCounter -= Time.deltaTime;


            if (waitCounter <= 0)
            {
                ChooseDirection();
            }

        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeDetection);
    }

    public void ChooseDirection()
    {

        WalkDirection = Random.Range(0, 4);

        isWalking = true;
        walkCounter = walkTime;

    }
}
