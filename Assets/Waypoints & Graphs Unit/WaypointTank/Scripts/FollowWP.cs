using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWP : MonoBehaviour
{
    private Transform goal;
    private float speed = 5.0f;
    private float accuracy = 5.0f;
    private float rotationSpeed = 2.0f;

    public GameObject wpManager;
    private GameObject[] wps;
    private GameObject currentNode;
    int currentWp = 0;
    Graph g;

    void Start()
    {
        wps = wpManager.GetComponent<WPManager>().waypoints;
        g = wpManager.GetComponent<WPManager>().graph;
        currentNode = wps[0];

    }

    public void GoToStart(){
        g.AStar(currentNode, wps[0]);
        currentWp = 0;
    }

    public void GoToHelicopterZone(){
        g.AStar(currentNode, wps[10]);
        currentWp = 0;
    }

    public void GoToBuild(){
        g.AStar(currentNode, wps[16]);
        currentWp = 0;
    }

    public void GoToRocks(){
        g.AStar(currentNode, wps[15]);
        currentWp = 0;
    }

    void LateUpdate()
    {
        if(g.pathList.Count == 0 || currentWp == g.pathList.Count){
            return;
        }

        if(Vector3.Distance(g.pathList[currentWp].getId().transform.position, transform.position) < accuracy){
            currentNode = g.pathList[currentWp].getId();
            currentWp++;
        }

        if(currentWp < g.pathList.Count){
            goal = g.pathList[currentWp].getId().transform;
            Vector3 lookAtGoal = new Vector3(goal.position.x,transform.position.y, goal.position.z);
            Vector3 direction = lookAtGoal - transform.position;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);

            transform.Translate(0,0, speed * Time.deltaTime);

        }
    }
}
