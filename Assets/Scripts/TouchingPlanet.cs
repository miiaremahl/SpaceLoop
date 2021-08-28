using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script to see if player is touching planet. Used for jumping.
 * Game scoring. 
 * Miia Remahl 
 * mrema003@gold.ac.uk
 * 
 * 1.Alan Zucconi - Learn.gold -Collision example
 */

public class TouchingPlanet : MonoBehaviour
{
    //player object
    public GameObject Player;

    void Start()
    {
        //setting the palyer object (is the parent)
        Player = gameObject.transform.parent.gameObject;
    }


    //If collided with planet -> player is touching the planet
    //refs: 1.Alan Zucconi, collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Planet")
        {
            Player.GetComponent<Player>().TouchesPlanet = true;
        }
    }


    //if leaves collision -> player is not touching the planet
    //refs: 1.Alan Zucconi, collision
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Planet")
        {
            Player.GetComponent<Player>().TouchesPlanet = false;
        }
    }
}
