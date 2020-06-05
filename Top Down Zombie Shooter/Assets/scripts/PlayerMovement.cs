using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour{

    private Rigidbody2D rb;

    public float speed;

    private float horizontal;
    private float vertical;

    void Start(){
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate(){
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(horizontal * speed, vertical * speed);
    }
}
