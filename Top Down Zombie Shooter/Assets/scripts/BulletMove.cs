using System;
using System.Collections;
using UnityEngine;

public class BulletMove : MonoBehaviour{

    static public bool pistol;
    static public bool assultrifle;
    static public bool sniper;
    static public bool LMG;

    [SerializeField] float pistolSpeed = 15;
    [SerializeField] float assultrifleSpeed = 20;
    [SerializeField] float sniperSpeed = 50;
    [SerializeField] float LMGSpeed = 17;

    private float speed = 0;

    [SerializeField] float destroyAfterTime = 3;

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
