using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoalDrivenBehaviour{
public class Spawner : MonoBehaviour
{
        public GameObject patientprefab;
        public int numPatients;
        void Start(){
            for (int i = 0; i < numPatients; i++){
                Instantiate(patientprefab, transform.position, Quaternion.identity);
            }

                Invoke("SpawnPatient",5f);

        }

        private void SpawnPatient(){
            Instantiate(patientprefab, transform.position, Quaternion.identity);
            Invoke("SpawnPatient",Random.Range(2f,10f));
        }
        void Update()
        {
            
        }
    }
}
