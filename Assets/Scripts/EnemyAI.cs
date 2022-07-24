using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public int health;
    public int maxhealth;
    public int damage;

    public float walkSpeed;
    public float runSpeed;

    private float timerAttack;
    public float cooldownAttack;

    public float rangeDetection;
    public float rangeAttack;

    public float waitTime;
    public float waitCounter;

    public float walkDirection;

    bool isPatrol;
    bool isChase;

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
        SetWaypoint();
        waitTime = Random.Range(5, 7);
        waitCounter = waitTime;
    }

    private void Update()
    {
        Patrol();
        Chasing();


    }

    public void Chasing()
    {
        if (isChase)
        {
            timerAttack += Time.deltaTime;

            Move(runSpeed);

            agent.SetDestination(playerTransform.position);

            Vector3 distancePlayer = transform.position - playerTransform.position;
            animator.SetFloat("Speed", 1);

            if (distancePlayer.magnitude < 1f)
            {
                //Attack
                agent.speed = 0;
                agent.isStopped = true;

            }
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

        timerAttack = 0;
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

    //private void Die()
    //{
    //    if (health <= 0)
    //    {
    //        animator.SetTrigger("Death");
    //        isDead = true;
    //        LootSystem.instance.Loot(lootTable, transform.position);
    //        Destroy(this.gameObject, 3f);
    //    }
    //}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeDetection);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, rangeAttack);
    }


}
