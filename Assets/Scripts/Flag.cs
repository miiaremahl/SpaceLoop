using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Lap count flag. When triggered a lap is added.
 * Miia Remahl 
 * mrema003@gold.ac.uk
 */

public class Flag : MonoBehaviour
{
    //has flag been triggered this round
    public bool marked = false;

    void OnTriggerEnter2D(Collider2D collider)
    {
        //change flag to be trigggered next round again
        if (collider.gameObject.CompareTag("FlagChanger") && marked)
        {
            marked = false;
        }
    }

}
