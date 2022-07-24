using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour, IHitable
{
    private int health;
    public int maxhealth;
    public int damage;

    public float walkSpeed;
    public float runSpeed;

    private float waitAttack;
    public float timeAttack;

    public float rangeDetection;
    public float rangeAttack;

    private float waitTime;
    private float waitCounter;

    public float walkDirection;

    private bool isPatrol;
    private bool isChase;
    private bool isAttack;
    private bool isDead;

    public ParticleSystem particlesHit;
    public LootTable loot;

    Transform playerTransform;
    Animator animator;
    NavMeshAgent agent;


    Vector3 wayPoint;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();

    }

    private void Start()
    {
        health = maxhealth;
        SetWaypoint();
        waitTime = Random.Range(5, 7);
        waitCounter = waitTime;
        waitAttack = timeAttack;
    }

    private void Update()
    {
        if (!isDead)
        {
            if (Vector3.Distance(transform.position, playerTransform.position) <= rangeDetection)
            {
                isChase = true;
                isPatrol = false;
            }
            else
            {
                isChase = false;
                isPatrol = true;
            }

            if (Vector3.Distance(transform.position, playerTransform.position) <= rangeAttack)
            {
                isPatrol = false;
                isChase = false;

                if (waitAttack <= 0)
                {
                    //Attack
                    Stop();
                    GetDamage();
                }

            }

            Patrol();
            Chasing();
        }

    }

    public void Chasing()
    {
        if (isChase)
        {
            waitAttack -= Time.deltaTime;

            Move(runSpeed);

            agent.SetDestination(playerTransform.position);

            Vector3 distancePlayer = transform.position - playerTransform.position;
            animator.SetFloat("Speed", 1);

        }
    }

    private void Patrol()
    {
        if (isPatrol)
        {
            Move(walkSpeed);

            agent.SetDestination(wayPoint);

            Vector3 distanceToWalkPoint = transform.position - wayPoint;

            if (distanceToWalkPoint.magnitude < 1f)
            {
                Stop();
                // isPatrol = false;
                waitCounter -= Time.deltaTime;

            }

            if (waitCounter < 0)
            {
                SetWaypoint();
                waitCounter = waitTime;

            }

        }

    }

    private void Stop()
    {
        agent.speed = 0;
        agent.isStopped = true;
        animator.SetFloat("Speed", 0f);
    }

    private void Move(float speed)
    {
        agent.speed = speed;
        agent.isStopped = false;
        animator.SetFloat("Speed", 0.5f);
    }

    public void GetDamage()
    {

        waitAttack = timeAttack;
        playerTransform.GetComponent<PlayerManager>().TakeDamage(damage);
        animator.SetTrigger("Attack");

    }

    private void SetWaypoint()
    {
        float randomZ = Random.Range(-walkDirection, walkDirection);
        float randomX = Random.Range(-walkDirection, walkDirection);

        wayPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        isPatrol = true;
    }

    private void Die()
    {
        if (health <= 0)
        {
            isDead = true;
            Stop();
            animator.SetTrigger("Death");
            LootSystem.instance.Loot(loot, transform.position);
            Destroy(this.gameObject, 2f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeDetection);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, rangeAttack);
    }

    public void TakeDamage(int damage, Vector3 pointHit)
    {
        health -= damage;
        particlesHit.transform.position = pointHit;
        particlesHit.Play();
        Die();
    }

    public int Health()
    {
        return health;
    }
}
