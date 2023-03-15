using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyFollow : MonoBehaviour
{
    public GameObject player;
    public float maxSpeed = 20f;
    public float acceleration = 0.5f;

   // private Rigidbody2D rb;
    public float currentSpeed = 10f;

    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
{
    Vector3 direction = (player.transform.position - transform.position).normalized;
    currentSpeed = Mathf.Clamp(currentSpeed + acceleration * Time.fixedDeltaTime, 0f, maxSpeed);
    transform.position += direction * currentSpeed * Time.fixedDeltaTime;
}
}
