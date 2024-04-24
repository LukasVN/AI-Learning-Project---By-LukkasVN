using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsOfAI{
    public class FixedUpdateMove : MonoBehaviour
    {
        float speed = 0.5f;
        void FixedUpdate()
        {
            transform.Translate(0,0, Time.fixedDeltaTime * speed);
        }
    }
}
