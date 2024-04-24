using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhysicsOfAI{
    public class Shell : MonoBehaviour
    {
        public GameObject explosion;
        private float speed = 0f;
        private float mass = 1f;
        private float force = 0.15f;
        private float drag = 0.5f;
        private float acceleration;

        void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.tag == "tank")
            {
                GameObject exp = Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(exp, 0.5f);
                Destroy(gameObject);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            acceleration = force / mass;
            speed += acceleration * 1;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            speed *= 1 - Time.deltaTime * drag;
            transform.Translate(0,0, speed);
        }
    }
}
