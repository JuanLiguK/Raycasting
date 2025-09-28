using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentScript : MonoBehaviour
{
    NavMeshAgent agent;

    [Header("Patrol")]
    [SerializeField] Transform[] patrolPoints; // Points to patrol
    [SerializeField] float arrivalDistance = 1f;

    [Header("Chase")]
    [SerializeField] Transform player; // Player transform
    [SerializeField] Transform rayOrigin; // Object from which the raycast is fired
    [SerializeField] float rayLength = 10f;
    [SerializeField] float lostTime = 2f; // Time to lose sight before returning to patrol

    Animator animator = null; // Animator of the NPC

    int currentPatrolIndex = 0;
    Transform currentDestination;

    bool isChasing = false;
    float lastSeenTime;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        if (patrolPoints.Length > 0)
        {
            currentPatrolIndex = 0;
            currentDestination = patrolPoints[currentPatrolIndex];
            agent.destination = currentDestination.position;
        }
    }

    void Update()
    {
        DetectPlayer();

        if (isChasing)
        {
            agent.destination = player.position;
        }
        else
        {
            // Normal patrol
            if (!agent.pathPending && agent.remainingDistance <= arrivalDistance)
            {
                currentPatrolIndex++;
                if (currentPatrolIndex >= patrolPoints.Length)
                {
                    currentPatrolIndex = 0;
                }

                currentDestination = patrolPoints[currentPatrolIndex];
                agent.destination = currentDestination.position;
            }
        }


        Debug.DrawRay(rayOrigin.position, rayOrigin.forward * rayLength, Color.red);
    }

    void DetectPlayer()
    {
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out hit, rayLength))
        {
            if (hit.transform == player)
            {
                isChasing = true;
                lastSeenTime = Time.time;
            }
        }
        else
        {
            if (Time.time - lastSeenTime > lostTime)
            {
                isChasing = false;
            }
        }
    }
}