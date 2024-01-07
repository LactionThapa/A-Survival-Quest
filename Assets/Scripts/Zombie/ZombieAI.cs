using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    private NavMeshAgent agent = null;
    private Animator animator = null;
    private float timeOfLastAttack = 0;
    private bool hasStopped = false;
    private ZombieStats stats = null;
    private GameObject target;
    private Vector3 spawnPoint;
    private float maxX;
    private float minX;
    private float maxZ;
    private float minZ;
    private float radius = 5;
    //[SerializeField] private Transform target;

    private Vector3 walkPoint;


    // Start is called before the first frame update
    void Start()
    {
        GetReference();
        target = GameObject.FindGameObjectWithTag("Player");
        spawnPoint = agent.transform.position;
        maxX = spawnPoint.x + radius;
        minX = spawnPoint.x - radius;
        maxZ = spawnPoint.z + radius;
        minZ = spawnPoint.z - radius;

    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }

    private void GetReference()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        stats = GetComponent<ZombieStats>();
    }

    private void MoveToTarget()
    {
        agent.SetDestination(target.transform.position);
        animator.SetFloat("Speed", 1f, 0.0f, Time.deltaTime);
        RotateToTarget();

        float distanceToTarget = Vector3.Distance(target.transform.position, transform.position);

        if (distanceToTarget <= agent.stoppingDistance)
        {
            animator.SetFloat("Speed", 0f, 0.0f, Time.deltaTime);
            if (!hasStopped)
            {
                hasStopped = true;
                timeOfLastAttack = Time.time;

            }
            if (Time.time >= timeOfLastAttack + stats.attackSpeed)
            {
                timeOfLastAttack = Time.time;

                Attack();
            }
        }
        else
        {
            if (hasStopped)
            {
                hasStopped = false;
            }
        }


    }

    private void RotateToTarget()
    {
        Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        Vector3 direction = targetPosition - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = rotation;
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
        //stats.playerController.DealDamage(stats.damage);
    }

    private void Patrol()
    {
        float randomZ = Random.Range(-5, 5);
        float randomX = Random.Range(-5, 5);

        checkRadius(randomX, randomZ);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        float distanceToWalk = Vector3.Distance(walkPoint, transform.position);

        float distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
        if (distanceToTarget > 15)
        {
            agent.SetDestination(walkPoint);
            animator.SetFloat("Speed", 0.25f, 0.0f, Time.deltaTime);

        }
        else if (distanceToTarget > 6 && distanceToTarget < 15)
        {
            agent.SetDestination(walkPoint);
            animator.SetFloat("Speed", 0.5f, 0.0f, Time.deltaTime);
        }
        else
        {
            MoveToTarget();
        }



    }
    private void checkRadius(float x, float z)
    {
        float nextXPosition = transform.position.x + x;
        float nextZPosition = transform.position.z + z;
        while (nextXPosition > maxX || nextZPosition > maxZ || nextXPosition < minX || nextZPosition < minZ)
        {
            float randomZ = Random.Range(-5, 5);
            float randomX = Random.Range(-5, 5);
            nextXPosition = transform.position.x + randomX;
            nextZPosition = transform.position.z + randomZ;

        }

    }


}

