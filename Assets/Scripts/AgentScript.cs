using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentScript : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Transform[] patrolPoints;
    [SerializeField] bool isPatroling = true;
    [SerializeField] float arrivalDistance = 1;
    [SerializeField] Animator anim;
    [SerializeField] float velocity;
    [SerializeField] Transform currentDestination;
    [SerializeField] int currntPatrolPointIndex;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        currentDestination = patrolPoints[0];
        currntPatrolPointIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.hasPath && agent.remainingDistance <= arrivalDistance)
        {
            if (currntPatrolPointIndex < patrolPoints.Length -1)
            {
                currntPatrolPointIndex++;
            }
            else
            {
                currntPatrolPointIndex = 0;
            }

            currentDestination = patrolPoints[currntPatrolPointIndex];
        }
        agent.destination = currentDestination.position;
        velocity = agent.velocity.magnitude;
        anim.SetFloat("Speed",velocity);
    }
}
