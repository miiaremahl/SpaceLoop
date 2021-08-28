using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * For destroying spawned effects.
 * Miia Remahl 
 * mrema003@gold.ac.uk
 * 
 * 1.Alan Zucconi :Learn.gold Coroutines example
 */
public class DestroyEffects : MonoBehaviour
{

    void Start()
    {
        //start a timer
        StartCoroutine(DestroyEffect());
    }


    //after 2 seconds destroy the effect prefab
    //refs:1.Alan Zucconi -> how to use coroutines
    IEnumerator DestroyEffect()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

}

