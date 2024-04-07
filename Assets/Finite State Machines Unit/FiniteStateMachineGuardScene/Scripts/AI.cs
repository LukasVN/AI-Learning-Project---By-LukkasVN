using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    public Transform player;
    private State currentState;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentState = new Idle(gameObject, agent, animator, player);
    }

    void Update(){
        currentState = currentState.Process();
    }
}
