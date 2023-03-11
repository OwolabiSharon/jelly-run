using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public GameObject player;
    public float maxSpeed = 10f;
    public float acceleration = 0.1f;

    private Rigidbody rb;
    private float currentSpeed = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        currentSpeed = Mathf.Clamp(currentSpeed + acceleration * Time.fixedDeltaTime, 0f, maxSpeed);
        rb.velocity = direction * currentSpeed;
    }
}