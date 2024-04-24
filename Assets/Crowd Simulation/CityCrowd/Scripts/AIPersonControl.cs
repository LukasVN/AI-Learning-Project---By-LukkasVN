using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CrowdSimulation
{
        

    public class AIPersonControl : MonoBehaviour
    {
        public GameObject[] goalLocations;
        private NavMeshAgent agent;
        private Animator animator;
        private float detectionRadius = 20;
        private float fleeRadius = 10;
        void Start()
        {
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            goalLocations = GameObject.FindGameObjectsWithTag("goal");
            int locationIndex = Random.Range(0, goalLocations.Length);
            agent.SetDestination(goalLocations[locationIndex].transform.position);
            animator.SetTrigger("isWalking");
            animator.SetFloat("wOffset", Random.Range(0.01f,1.0f));
            ResetAgent();
        }

        void ResetAgent(){
            float speedMultiplier = Random.Range(0.5f, 2f);
            animator.SetFloat("speedMult", speedMultiplier);
            agent.speed *= speedMultiplier;
            animator.SetTrigger("isWalking");
            agent.angularSpeed = 120;
            agent.ResetPath();
        }

        public void DetectNewObstacle(Vector3 position){
            if(Vector3.Distance(position, transform.position) < detectionRadius){
                Vector3 fleeDirection = (transform.position - position).normalized;
                Vector3 newgoal = transform.position + fleeDirection * fleeRadius;

                NavMeshPath path  = new NavMeshPath();
                agent.CalculatePath(newgoal,path);
                if(path.status != NavMeshPathStatus.PathInvalid){
                    agent.SetDestination(path.corners[path.corners.Length - 1]);
                    animator.SetTrigger("isRunning");
                    agent.speed = 10;
                    agent.angularSpeed = 500;
                }

            }
        }

        void Update()
        {
            if(agent.remainingDistance < 1){
                ResetAgent();
                int locationIndex = Random.Range(0, goalLocations.Length);
                agent.SetDestination(goalLocations[locationIndex].transform.position);
            }
        }
    }

}
