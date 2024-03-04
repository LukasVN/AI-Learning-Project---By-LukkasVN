using System;
using UnityEngine;

public class Move : MonoBehaviour {

    public GameObject goal;
    Vector3 direction;
    public float speed = 2f;

    void Start() {
        // direction = goal.transform.position - transform.position;
    }

    private void LateUpdate() {
        Vector3 direction = goal.transform.position - transform.position;
        if (direction.magnitude > 2)
        {
            Vector3 velocity = direction.normalized * speed * Time.deltaTime;
            transform.Translate(velocity, Space.World);
            transform.LookAt(goal.transform.position);
        }

    }

}

