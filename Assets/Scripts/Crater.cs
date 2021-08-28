using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Script for prefab craters. Used for example destroying the prefab.
 * Miia Remahl 
 * mrema003@gold.ac.uk
 * 
 * References:
 * 1.Alan Zucconi - Learn.gold Collision2D/Trigger tutorial
 * 
 */

public class Crater : MonoBehaviour
{

    //the damage crater makes
    public float damage;

    //has the crater been damaged already
    public bool hasDamaged;

    //effect for axplosion
    public ParticleSystem explosion;

    //Rigidbody
    public Rigidbody2D rb;

    void Start()
    {
        //set damaged to not damaged
        hasDamaged = false;

        //get rigidbody
        rb = transform.GetComponent<Rigidbody2D>();
    }

    //1.Alan Zucconi trigger example
    void OnTriggerEnter2D(Collider2D collider)
    {

        //if CraterDestroyer is triggered then crater is destroyed
        if (collider.gameObject.CompareTag("CraterDestroyer"))
        {
            Destroy(gameObject);
        }
    }

    //If crater hits player then explosion effect is triggered
    //1.Alan Zucconi collision example
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasDamaged)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
        }

    }
}
