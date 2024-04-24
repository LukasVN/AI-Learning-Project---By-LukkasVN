using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CrowdSimulation
{
    public class AIControl : MonoBehaviour
    {
        public Transform goal;
        private NavMeshAgent agent;
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.SetDestination(goal.position);
        }

        void Update()
        {
            
        }
    }
}
