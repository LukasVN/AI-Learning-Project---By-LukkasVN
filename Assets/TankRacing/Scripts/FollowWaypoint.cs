using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWaypoint : MonoBehaviour
{
    public GameObject[] waypoints;
    private int currentWP = 0;
    
    public float speed = 12f;
    public float rotationSpeed = 10f;
    public float lookAhead = 20f;

    private GameObject tracker;

    private void Start() {
        tracker = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        DestroyImmediate(tracker.GetComponent<Collider>());
        tracker.GetComponent<MeshRenderer>().enabled = false;
        tracker.transform.position = transform.position;
        tracker.transform.rotation = transform.rotation;
    }

    private void ProgressTracker(){
        if(Vector3.Distance(tracker.transform.position,transform.position) > lookAhead ){
            return;
        }

        if(Vector3.Distance(tracker.transform.position,waypoints[currentWP].transform.position) < 3){
            currentWP++;
        }

        if(currentWP >= waypoints.Length){
            currentWP = 0;
        }

        tracker.transform.LookAt(waypoints[currentWP].transform.position);
        tracker.transform.Translate(0,0, (speed + 20) * Time.deltaTime);
    }


    void Update()
    {
        ProgressTracker();

        // Progressively look towards the waypoint
        Quaternion lookAtTracker = Quaternion.LookRotation(tracker.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookAtTracker, rotationSpeed * Time.deltaTime);

        transform.Translate(0,0, speed * Time.deltaTime);


    }
}
