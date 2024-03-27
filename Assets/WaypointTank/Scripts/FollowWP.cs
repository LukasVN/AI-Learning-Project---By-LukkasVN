using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWP : MonoBehaviour
{
    private Transform goal;
    private float speed = 5.0f;
    private float accuracy = 1.0f;
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

    public void GoToRocks(){
        g.AStar(currentNode, wps[15]);
        currentWp = 0;
    }

    void Update()
    {
        
    }
}
