using System;
using System.Collections;
using UnityEngine;

public class BulletMove : MonoBehaviour{

    public float speed = 10f;

    public float destroyAfterTime = 3;

    private Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    void Update()
    {
        destroyAfterTime -= Time.deltaTime;

        if (destroyAfterTime < 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(this.gameObject);
    }
}
