using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    
    private NavMeshAgent agent;
    private Animator anim;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(agent.remainingDistance < 2){
            anim.SetBool("isMoving", false);
        }
        else{
            anim.SetBool("isMoving", true);
        }
        
    }
    
}
