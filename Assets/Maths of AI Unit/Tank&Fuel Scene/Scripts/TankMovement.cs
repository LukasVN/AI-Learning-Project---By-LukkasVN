using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathsofAI{
    public class TankMovement : MonoBehaviour
    {
        public float speed = 10.0f;
        public GameObject fuel;
        public float rotationSpeed = 100.0f;
        private bool autopilot;
        public float autoRotatingSpeed = 3f;

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

            //Calculates the distance and angle from the fuel and starts to rotate towards it
            if(Input.GetKeyDown(KeyCode.Space)){
                CalculateDistance();
                InstantlyRotateTowards();
            }
            if(Input.GetKeyDown(KeyCode.T)){
                autopilot = !autopilot;
            }

            if(CalculateDistance() < 2){
                autopilot = false;
            }
            if(autopilot){
                autoPilot();
                RotateAndCalculateAngle();
            }

        }

        private void autoPilot(){
            transform.position += transform.up * speed * Time.deltaTime;
        }

        private float CalculateDistance(){
            //Mathematical way:
            // float distance = Mathf.Sqrt(Mathf.Pow(fuel.transform.position.x - transform.position.x,2)+Mathf.Pow(fuel.transform.position.y - transform.position.y,2));

            //Unity mathematical way:
            // float distance = (fuel.transform.position-transform.position).magnitude;

            //Unity methods way
            return Vector3.Distance(fuel.transform.position, transform.position);

        }

        private void RotateAndCalculateAngle(){
            Vector3 tankforward = transform.up; //Up because its a fake 2D in a 3D project
            Vector3 fueldirection = fuel.transform.position - transform.position;

            Debug.DrawRay(transform.position,tankforward*10, Color.green, 2f); //Check where the tankforward is facing
            Debug.DrawRay(transform.position,fueldirection, Color.red, 2f); //Check where the fueldirection is facing

            //Mathematical way to calculate the angle with the dot product
            float dot = tankforward.x * fueldirection.x + tankforward.y * fueldirection.y;
            float mathAngle = Mathf.Acos(dot / (tankforward.magnitude * fueldirection.magnitude));

            //Unity way to calculate the angle
            float uAngle = Vector3.Angle(tankforward,fueldirection);
            
            Debug.Log("Math Angle: "+ mathAngle * Mathf.Rad2Deg);
            Debug.Log("Unity Angle: " + uAngle);

            //Rotate over time in the mathematical way with the UnityCrossProduct plus my personal touch to avoid rotating stuck on small values
            if(uAngle * Mathf.Rad2Deg < 3 && uAngle * Mathf.Rad2Deg != 0){
                InstantlyRotateTowards();
                return;
            }
            Vector3 crossProduct = UnityCrossProduct(tankforward, fueldirection);
            transform.Rotate(0,0,crossProduct.z * autoRotatingSpeed * Time.deltaTime);

        }

        private void InstantlyRotateTowards(){
            //Rotate instantly using quaternions
            Vector3 fueldirection = fuel.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, fueldirection);
            transform.rotation = targetRotation;
        }
        
        
        private Vector3 UnityCrossProduct(Vector3 v, Vector3 w){
            Vector3 crossProduct = Vector3.Cross(v, w); 
            return crossProduct;
        }
    }
}

