using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsOfAI{
    public class UpdateMove : MonoBehaviour
    {
        float speed = 0.5f;
        private void Awake() {
        }
        void Update()
        {
            transform.Translate(0,0, Time.deltaTime * speed);
        }
    }
}
