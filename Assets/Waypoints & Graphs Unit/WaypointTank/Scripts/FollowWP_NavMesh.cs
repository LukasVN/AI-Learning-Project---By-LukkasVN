using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

namespace WaypointsAndGraphs{
    public class FollowWP_NavMesh : MonoBehaviour
    {
        public GameObject wpManager;
        private GameObject[] wps;
        private GameObject currentNode;
        private NavMeshAgent agent;

        void Start()
        {
            wps = wpManager.GetComponent<WPManager>().waypoints;
            currentNode = wps[0];

            agent = GetComponent<NavMeshAgent>();

        }

        public void GoToStart(){
            // It can be a given transform when using NavMesh
            agent.SetDestination(wps[0].transform.position);
        }

        public void GoToHelicopterZone(){
            agent.SetDestination(wps[10].transform.position);
        }

        public void GoToBuild(){
            agent.SetDestination(wps[16].transform.position);
        }

        public void GoToRocks(){
            agent.SetDestination(wps[15].transform.position);
        }

        void LateUpdate()
        {
            
        }
    }
}