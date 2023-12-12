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
    //[SerializeField] private Transform target;

    private Vector3 walkPoint;


    // Start is called before the first frame update
    void Start()
    {
        GetReference();
        target = GameObject.FindGameObjectWithTag("Player");
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
        animator.SetFloat("Velocity", 1f, 0.0f, Time.deltaTime);
        RotateToTarget();

        float distanceToTarget = Vector3.Distance(target.transform.position, transform.position);

        if (distanceToTarget <= agent.stoppingDistance)
        {
            animator.SetFloat("Velocity", 0f);
            animator.SetFloat("Speed", 0f);
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
    }

    private void Patrol()
    {
        float randomZ = Random.Range(-100, 100);
        float randomX = Random.Range(-100, 100);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        float distanceToWalk = Vector3.Distance(walkPoint, transform.position);

        float distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
        if (distanceToTarget > 5)
        {
            agent.SetDestination(walkPoint);
            animator.SetFloat("Speed", 1f, 0.0f, Time.deltaTime);

        }
        else
        {
            MoveToTarget();
        }



    }


}

