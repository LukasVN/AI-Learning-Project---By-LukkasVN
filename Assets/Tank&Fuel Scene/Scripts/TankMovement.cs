using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public float speed = 10.0f;
    public GameObject fuel;
    public float rotationSpeed = 100.0f;

    void Update()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        // Move translation along the object's y-axis
        transform.Translate(0, translation, 0);

        // Rotate around our z-axis
        transform.Rotate(0, 0, -rotation);

        if(Input.GetKeyDown(KeyCode.Space)){
            CalculateDistance();
        }
    }

    private void CalculateDistance(){
        //Mathematical way:
        // float distance = Mathf.Sqrt(Mathf.Pow(fuel.transform.position.x - transform.position.x,2)+Mathf.Pow(fuel.transform.position.y - transform.position.y,2));

        //Unity based way:
        float distance = (fuel.transform.position-transform.position).magnitude;

        float uDistance = Vector3.Distance(fuel.transform.position, transform.position);

        Debug.Log("Distance: " + distance);
        Debug.Log("Distance: " + uDistance);
    }
}
