using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPersonControl : MonoBehaviour
{
    public GameObject[] goalLocations;
    private NavMeshAgent agent;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        goalLocations = GameObject.FindGameObjectsWithTag("goal");
        int locationIndex = Random.Range(0, goalLocations.Length);
        agent.SetDestination(goalLocations[locationIndex].transform.position);
        animator.SetTrigger("isWalking");
        animator.SetFloat("wOffset", Random.Range(0.01f,1.0f));
        float speedMultiplier = Random.Range(0.5f, 2f);
        animator.SetFloat("speedMult", speedMultiplier);
        agent.speed *= speedMultiplier;
    }

    void Update()
    {
        if(agent.remainingDistance < 1){
            int locationIndex = Random.Range(0, goalLocations.Length);
            agent.SetDestination(goalLocations[locationIndex].transform.position);
        }
    }
}
