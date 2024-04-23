using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviourTrees
{
    public class Rotate : MonoBehaviour
    {
        // Update is called once per frame
        void FixedUpdate()
        {
            transform.Rotate(0, 2, 0);
        }
    }
}
