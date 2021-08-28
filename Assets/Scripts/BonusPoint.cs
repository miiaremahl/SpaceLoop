using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Code for bonuspoint.
 * Miia Remahl 
 * mrema003@gold.ac.uk
 * 
 * References:
 * 1.Alan Zucconi - Learn.gold Missile example (Lerp)
 * 2.Nomical Games - Creating Basic Particle Effects in Unity https://www.youtube.com/watch?v=7hRWhnL1IVk
 * 3. Press start - Unity Movement [RigidBody vs Translate] https://www.youtube.com/watch?v=ixM2W2tPn6c
 */


public class BonusPoint : MonoBehaviour
{
    //The points to move from-to (A->B)
    public Vector3 A;
    public Vector3 B;

    //The time prefab was created (used foir lerping)
    float creationTime;

    //Effect that appears when collided with player
    public ParticleSystem effect;

    //The curve in which the lerping happens
    public AnimationCurve Curve;

    //rotating speed
    public float RotatingSpeed;

    //how long Lerping takes
    public float Duration;


    //Rigidbody
    public Rigidbody2D rb;

    void Start()
    {
        //set the creation time for prefab
        creationTime = Time.time;
    }

    void FixedUpdate()
    {
        DoLerping();
    }

    //refs: 1.Alan Zucconi --> advice for lerping, 
    // 3. Press start -> moving ridigbodies
    void DoLerping()
    {
        //sustract the creation time from the time game has been on / it with duration
        float t = (Time.time - creationTime) / Duration;
        float t2 = Curve.Evaluate(t);

        //change the position
        rb.MovePosition(Vector3.Lerp(A, B, t2));

        //Old rotation
        //transform.Rotate(0, 0, RotatingSpeed * Time.deltaTime);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        //destroyes the bonusitem if it hits player -> and makes an effect
        //refs: 2. Nomical Games  how to make effect when colliding with something
        if (collision.gameObject.CompareTag("Player"))
        {
            //instantiate the effects
            Instantiate(effect, transform.position, Quaternion.identity);

            //destroy the object
            Destroy(gameObject);
        }

    }

    //destroy the star
    void DestroyTotally()
    {
        Destroy(gameObject);
    }
}
