using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    private float speed;
    void Start()
    {
        speed = Random.Range(FlockingManager.instance.minSpeed, FlockingManager.instance.maxSpeed);
    }

    void Update()
    {
        ApplyRules();
        transform.Translate(0,0, speed * Time.deltaTime);
    }

    private void ApplyRules(){
        GameObject[] gos;
        gos = FlockingManager.instance.allFish;

        Vector3 vcentre = Vector3.zero;
        Vector3 vavoid = Vector3.zero;
        float gSpeed = 0.01f;
        float nDistance;
        int groupSize = 0;

        foreach (GameObject go in gos){
            nDistance = Vector3.Distance(go.transform.position, transform.position);
            if (nDistance <= FlockingManager.instance.neighbourDistance){
                vcentre += go.transform.position;
                groupSize++;

                if(nDistance < 1.0f){
                    vavoid = vavoid + (transform.position - go.transform.position);
                }

                Flock anotherFlock = go.GetComponent<Flock>();
                gSpeed = gSpeed + anotherFlock.speed;
            }
        }

        if(groupSize > 0){
            vcentre = vcentre/groupSize;
            speed = gSpeed / groupSize;

            Vector3 direction = (vcentre + vavoid) - transform.position;
            if(direction != Vector3.zero){
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                        Quaternion.LookRotation(direction),
                                                        FlockingManager.instance.rotationDistance * Time.deltaTime);
            }

        }
    }

}
