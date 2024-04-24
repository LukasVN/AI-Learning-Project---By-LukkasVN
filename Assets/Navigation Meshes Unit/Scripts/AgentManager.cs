using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace NavigationMeshesUnit{
    public class AgentManager : MonoBehaviour
    {
        List<NavMeshAgent> agentList = new List<NavMeshAgent>();
        void Start()
        {
            GameObject[] a = GameObject.FindGameObjectsWithTag("AI");
            foreach (GameObject go in a){
                agentList.Add(go.GetComponent<NavMeshAgent>());
            }
        }

        void Update()
        {
            if(Input.GetMouseButtonDown(0)){
                RaycastHit hit;
                if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)){
                    foreach (NavMeshAgent navAgent in agentList)
                    {
                        navAgent.SetDestination(hit.point);
                    }
                }
            }
        }
        
    }
}
