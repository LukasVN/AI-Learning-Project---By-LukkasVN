using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsOfAI{
    public class LateUpdateMove : MonoBehaviour
    {
        float speed = 0.5f;
        void LateUpdate()
        {
            transform.Translate(0,0, Time.deltaTime * speed);
        }
    }
}
