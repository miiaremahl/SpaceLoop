using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Meteor code.
 * Miia Remahl 
 * mrema003@gold.ac.uk
 * 
 * References:
 * 1.Alan Zucconi - Learn.gold Missile example (Lerp)
 * 2. Press start - Unity Movement [RigidBody vs Translate] https://www.youtube.com/watch?v=ixM2W2tPn6c
 * 
 */
public class Meteor : MonoBehaviour
{
    //Where to lerp from-to (A-> B)
    public Vector3 A;
    public Vector3 B;


    //rotating speed
    public float RotatingSpeed;

    //damage made by meteor
    public float damage;

    //damage per kg
    public float damagePerKg=100;

    //effect for explosion when collided with player
    public ParticleSystem explosion;

    //Rigidbody
    public Rigidbody2D rb;

    //public float t;
    public float Duration; 

    //animation curve for lerping
    public AnimationCurve Curve;

    //creation time of the prefab
    float creationTime;




    void Start()
    {
        //set damage to 0
        damage = 0;

        //Set rotating speed for planet
        RotatingSpeed = 100f;

        //get the creation time
        creationTime = Time.time;
    }

    void Update()
    {
        //sets damage
        if(damage == 0)
        {
            damage = Mathf.Round(damagePerKg * rb.mass);
        }
    }

    void FixedUpdate()
    {
        DoLerping();
    }

    //Lerps object to it's place
    //1.Alan Zucconi -advice for lerping
    //2.Press start - moving ridigbodies
    void DoLerping()
    {
        //Current time - creation time / duration of the lerp
        float t = (Time.time - creationTime) / Duration;

        float t2 = Curve.Evaluate(t);

        //position
        rb.MovePosition(Vector3.Lerp(A, B, t2));


        //rotating the meteor
        //transform.Rotate(0, 0, RotatingSpeed * Time.deltaTime);
    }

    //refs: 1.Alan Zucconi -lectures: how to do triggers
    void OnTriggerEnter2D(Collider2D collider)
    {
        //destroyes the meteor if right trigger is found
        if (collider.gameObject.CompareTag("MeteorDestroyer"))
        {
            Destroy(gameObject);
        }
    }

    //refs: 1.Alan Zucconi -lectures: how to do collision
    void OnCollisionEnter2D(Collision2D collision)
    {
        //destroyes the meteor if it hits player (+effect)
        if (collision.gameObject.CompareTag("Player"))
        {
            //triggers the effext --> makes a prefab
            Instantiate(explosion,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }

    }


}
