using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class AgentScript : MonoBehaviour
{
    NavMeshAgent agent;


    [Header("Patrol")]
    [SerializeField] Transform[] patrolPoints;
    [SerializeField] float arrivalDistance = 1f;


    [Header("Chase")]
    [SerializeField] Transform player;
    [SerializeField] Transform rayOrigin;
    [SerializeField] float rayLength = 10f;
    [SerializeField] float lostTime = 2f;
    [SerializeField] float catchDistance = 1.5f;


    Animator animator = null;


    int currentPatrolIndex = 0;
    Transform currentDestination;


    bool isChasing = false;
    float lastSeenTime = -Mathf.Infinity;


    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }


    void Start()
    {
        if (patrolPoints != null && patrolPoints.Length > 0)
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
            if (player != null)
                agent.destination = player.position;


            float distanceToPlayer = player != null ? Vector3.Distance(transform.position, player.position) : Mathf.Infinity;
            if (distanceToPlayer <= catchDistance)
            {
                PlayerCaught();
            }


            if (animator != null)
                animator.SetBool("isWalking", true);
        }
        else
        {
            if (!agent.pathPending && agent.remainingDistance <= arrivalDistance)
            {
                currentPatrolIndex++;
                if (patrolPoints != null && currentPatrolIndex >= patrolPoints.Length)
                    currentPatrolIndex = 0;


                currentDestination = patrolPoints[currentPatrolIndex];
                agent.destination = currentDestination.position;
            }


            if (animator != null)
            {
                if (!agent.pathPending && agent.remainingDistance <= arrivalDistance)
                    animator.SetBool("isWalking", false);
                else
                    animator.SetBool("isWalking", true);
            }
        }


        if (rayOrigin != null)
            Debug.DrawRay(rayOrigin.position, rayOrigin.forward * rayLength, Color.red);
    }


    void DetectPlayer()
    {
        if (rayOrigin == null || player == null) return;


        RaycastHit hit;
        if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out hit, rayLength))
        {
          
            if (hit.transform == player || hit.transform.IsChildOf(player))
            {
                isChasing = true;
                lastSeenTime = Time.time;
                return;
            }
        }


       
        if (isChasing && Time.time - lastSeenTime > lostTime)
        {
            isChasing = false;


            if (patrolPoints != null && patrolPoints.Length > 0)
            {
                currentPatrolIndex = Random.Range(0, patrolPoints.Length);
                currentDestination = patrolPoints[currentPatrolIndex];
                agent.destination = currentDestination.position;
            }
        }
    }


    void PlayerCaught()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("GameOverScene");
    }
}




