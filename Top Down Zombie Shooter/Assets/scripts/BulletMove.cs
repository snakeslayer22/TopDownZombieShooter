using System;
using System.Collections;
using UnityEngine;

public class BulletMove : MonoBehaviour{

    static public bool pistol;
    static public bool assultrifle;
    static public bool sniper;
    static public bool LMG;

    public float pistolSpeed = 15;
    public float assultrifleSpeed = 20;
    public float sniperSpeed = 50;
    public float LMGSpeed = 17;

    private float speed = 0;

    public float destroyAfterTime = 3;

    private Rigidbody2D rb;

    public string zombie;

    void Start()
    {
        setSpeed();

        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    private void setSpeed()
    {
        if (pistol == true)
        {
            speed = pistolSpeed;
        }
        else if (assultrifle == true)
        {
            speed = assultrifleSpeed;
        }
        else if (sniper == true)
        {
            speed = sniperSpeed;
        }
        else if (LMG == true)
        {
            speed = LMGSpeed;
        }
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
        if (col.collider.tag == zombie)
        {

        }

        Destroy(this.gameObject);
    }
}
