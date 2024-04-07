using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public GameObject target;
    private NavMeshAgent agent;
    private Animator anim;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        agent.SetDestination(target.transform.position);
        if(agent.remainingDistance < 2){
            anim.SetBool("isMoving", false);
        }
        else{
            anim.SetBool("isMoving", true);
        }
        
    }
    
}
