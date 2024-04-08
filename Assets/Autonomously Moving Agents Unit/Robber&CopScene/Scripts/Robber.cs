using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Robber : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject target;
    private Move moveScript;
    public enum MovementType{
        Seek,
        Flee,
        Pursue,
        Evade,
        Wander,
        Hide,
    }
    public MovementType movementType;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        moveScript = target.GetComponent<Move>();
    }

    private void Seek(Vector3 location){
        agent.SetDestination(location);
    }

    private void Flee(Vector3 location){
        Vector3 fleeVector = location - transform.position;
        agent.SetDestination(transform.position - fleeVector);
    }

    private void Pursue(){
        Vector3 targetDir = target.transform.position - transform.position;

        float relativeHeading = Vector3.Angle(transform.forward, transform.TransformVector(target.transform.forward));
        float toTarget = Vector3.Angle(transform.forward, transform.TransformVector(targetDir));

        if((toTarget > 90 && relativeHeading < 20) || moveScript.currentSpeed < 0.01f){
            Seek(target.transform.position);
            return;
        }

        float lookAHead = targetDir.magnitude/(agent.speed + target.GetComponent<Move>().currentSpeed);
        //Debug.DrawLine(target.transform.position, target.transform.position + target.transform.forward * lookAHead, Color.red);
        Seek(target.transform.position + target.transform.forward*lookAHead); //Multiply an extra number to predict the distance further
    }

    private void Evade(){
        Vector3 targetDir = transform.position - target.transform.position;

        float relativeHeading = Vector3.Angle(transform.TransformVector(target.transform.forward), transform.forward);
        float toTarget = Vector3.Angle(transform.TransformVector(targetDir), transform.forward);

        if((toTarget > 90 && relativeHeading < 20) || moveScript.currentSpeed < 0.01f){
            Flee(target.transform.position);
            return;
        }

        float lookAHead = targetDir.magnitude/(agent.speed + moveScript.currentSpeed);
        //Debug.DrawLine(target.transform.position, target.transform.position + target.transform.forward * lookAHead, Color.red);
        Flee(target.transform.position + target.transform.forward*lookAHead);

        
        // Vector3 targetDir = target.transform.position - transform.position;
        // float lookAHead = targetDir.magnitude/(agent.speed + moveScript.currentSpeed);
        // Debug.DrawLine(target.transform.position, target.transform.position + target.transform.forward * lookAHead, Color.red);
        // Flee(target.transform.position + target.transform.forward*lookAHead); 
    }

    private void Wander(){
        Vector3 wanderTarget = Vector3.zero;
        float wanderRadius = 10;
        float wanderDistance = 20;
        float wanderJitter = 10;

        wanderTarget += new Vector3(Random.Range(-1f,1) * wanderJitter,
                                    0,
                                    Random.Range(-1f,1) * wanderJitter);
        
        wanderTarget.Normalize();
        wanderTarget *= wanderRadius;

        Vector3 targetLocal = wanderTarget + new Vector3(0,0,wanderDistance);
        Vector3 targetWorld = gameObject.transform.InverseTransformVector(targetLocal);
        Seek(targetWorld);
    }

    private void Hide(){
        float distance = Mathf.Infinity;
        Vector3 chosenSpot = Vector3.zero;
        float hideDistance = 20;

        for (int i = 0; i < World.Instance.GetHidingSpots().Length; i++){
            Vector3 hideDirection = World.Instance.GetHidingSpots()[i].transform.position - target.transform.position;
            Vector3 hidePosition = World.Instance.GetHidingSpots()[i].transform.position + hideDirection.normalized * hideDistance;

            if(Vector3.Distance(transform.position, hidePosition) < distance){
                chosenSpot = hidePosition;
                distance = Vector3.Distance(transform.position, hidePosition);
            }
        }

        Seek(chosenSpot);
        //agent.SetDestination(chosenSpot);
    }
    
    void Update()
    {
        switch (movementType)
        {
            case MovementType.Seek: 
                Seek(target.transform.position);           
            break;

            case MovementType.Flee:
                Flee(target.transform.position);
            break;

            case MovementType.Pursue:
                Pursue();
            break;
            case MovementType.Evade:
                Evade();
            break;
            case MovementType.Wander:
                Wander();
            break;
            case MovementType.Hide:
                Hide();
            break;

        }
        
    }
    
    
}
