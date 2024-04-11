using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCylinder : MonoBehaviour
{
    public GameObject obstacle;
    private GameObject[] agents;
    void Start()
    {
        agents = GameObject.FindGameObjectsWithTag("agent");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray.origin, ray.direction, out hitInfo)){
                Instantiate(obstacle, hitInfo.point, obstacle.transform.rotation);
                foreach (GameObject ag in agents){
                    ag.GetComponent<AIPersonControl>().DetectNewObstacle(hitInfo.point);
                }
            }
        }
    }
}
