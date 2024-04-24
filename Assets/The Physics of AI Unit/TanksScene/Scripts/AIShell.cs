using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsOfAI{
    public class AIShell : MonoBehaviour
    {
        public GameObject explosion;
        private Rigidbody rb;
        private void Start() {
            rb = GetComponent<Rigidbody>();
        }

        void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.tag == "tank")
            {
                GameObject exp = Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(exp, 0.5f);
                Destroy(gameObject);
            }
        }
        private void Update() {
            transform.forward = rb.velocity;
        }
    }
}
