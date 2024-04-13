using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    private float speed;
    private bool turning = false;
    void Start()
    {
        speed = Random.Range(FlockingManager.instance.minSpeed, FlockingManager.instance.maxSpeed);
    }

    void Update()
    {
        Bounds b = new Bounds(FlockingManager.instance.transform.position, FlockingManager.instance.swimLimits * 2);

        if(!b.Contains(transform.position)){
            turning = true;
        } else{
            turning = false;
        }

        if(turning){
            Vector3 direction = FlockingManager.instance.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 
                                                    FlockingManager.instance.rotationSpeed * Time.deltaTime);
        } else{
            if(Random.Range(0,100) < 10){
                speed = Random.Range(FlockingManager.instance.minSpeed, FlockingManager.instance.maxSpeed);
            }
            if(Random.Range(0,100) < 10){
                ApplyRules();
            }
        }
        
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
            vcentre = vcentre/groupSize + FlockingManager.instance.goalPosition - transform.position;
            speed = gSpeed / groupSize;
            if(speed > FlockingManager.instance.maxSpeed){
                speed = FlockingManager.instance.maxSpeed;
            }

            Vector3 direction = (vcentre + vavoid) - transform.position;
            if(direction != Vector3.zero){
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                        Quaternion.LookRotation(direction),
                                                        FlockingManager.instance.rotationSpeed * Time.deltaTime);
            }

        }
    }

}
