using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
 * HealthBar code.
 * Miia Remahl 
 * mrema003@gold.ac.uk
 * 
 * References:
 * 1.brackeys - How to make a HEALTH BAR in Unity! https://www.youtube.com/watch?v=BLfNP4Sc_iA
 */


public class HealthBar : MonoBehaviour
{
    //gradient to change the color of bar
    public Gradient gradient;

    //inside fill of the bar
    public Image fill;

    //slider for changing the bar
    public Slider slider;


    //set HealthBar to the maximum 
    //refs:1. brackeys : used the tutorial to learn how to do the healthbar
    public void SetMaxHealth(float health)
    {
        //setting bar values
        slider.maxValue = health;
        slider.value = health;

        //setting the gradient of the bar at the beginning
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float health)
    {
        //setting the health
        slider.value = health;

        //setting the bar color
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
