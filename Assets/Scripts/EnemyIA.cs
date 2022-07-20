using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyIA : MonoBehaviour, IHitable
{
    public int health;
    public int maxhealth;
    public int damage;

    private float timerAttack;
    public float cooldownAttack;

    public float rangeDetection;
    public float rangeAttack;

    public float moveSpeed;

    Vector3 stopPosition;

    private float walkTime;
    private float walkCounter;
    private float waitTime;
    private float waitCounter;

    int WalkDirection;

    public bool isPatrol;
    public bool isWalking;
    public bool isAttack;
    public bool isDead;

    public ParticleSystem particles;

    public LootTable lootTable;

    private Transform playerTransform;

    private Animator animator;

    void Start()
    {

        health = maxhealth;

        walkTime = Random.Range(10, 20);
        waitTime = Random.Range(5, 7);

        waitCounter = waitTime;
        walkCounter = walkTime;

        ChooseDirection();

        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();

    }


    void Update()
    {
        //ATTACKING

        timerAttack += Time.deltaTime;

        Vector3 posPlayer = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);

        float dist = Vector3.Distance(playerTransform.transform.position, transform.position);

        if (dist < rangeDetection && !isDead)
        {
            animator.SetFloat("Speed", 1);
            isPatrol = false;
            isAttack = true;
            transform.position = Vector3.MoveTowards(transform.position, posPlayer, moveSpeed * Time.deltaTime);
            transform.LookAt(posPlayer);
            

        }
        else
        {
            isPatrol = true;
            isAttack = false;
        }

        if (dist < rangeAttack && timerAttack >= cooldownAttack)
        {
            
            GetDamage();

        }


        //MOVEMENT
        if (!isDead && isPatrol)
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

        if (isPatrol)
        {
            animator.SetFloat("Speed", 0.5f);
        }




    }

    private void Die()
    {
        if (health <= 0)
        {
            animator.SetTrigger("Death");
            isDead = true;
            LootSystem.instance.Loot(lootTable, transform.position);
            //GetComponent<Rigidbody>().AddForce(this.transform.forward * 5f, ForceMode.Impulse);
            Destroy(this.gameObject, 3f);
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

    public void TakeDamage(int damage, Vector3 pointhit)
    {
        health -= damage;
        particles.transform.position = pointhit;
        particles.Play();
        Die();
    }

    public void GetDamage()
    {

        playerTransform.GetComponent<PlayerManager>().TakeDamage(damage);
        timerAttack = 0;
        animator.SetTrigger("Attack");

    }

    public int Health()
    {
        return health;
    }
}
