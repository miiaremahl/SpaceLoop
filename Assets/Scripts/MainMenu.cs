using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * MainMenu interaction.
 * Miia Remahl 
 * mrema003@gold.ac.uk
 * 
 * References:
 * 1.Brackeys - START MENU in Unity https://www.youtube.com/watch?v=zc8ac_qUXQY
 * 2. Unity documentation - Importing Fonts https://docs.unity3d.com/560/Documentation/Manual/class-Font.html
 */

public class MainMenu : MonoBehaviour
{
    //reference to howtoplay screen
    public GameObject HowToPlay;


    //Start the game.. loads the next scene
    //1.Brackeys - main menu setting
    public void PlayGame()
   {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
   }

    //enabled how to play screen
    //1.Brackeys - main menu setting
    public void EnableHowToPlay()
    {
        HowToPlay.SetActive(true);
    }

    //disables how to play screen
    //1.Brackeys - main menu setting
    public void DisableHowToPlay()
    {
        HowToPlay.SetActive(false);
    }

    //goes back to main menu
    //1.Brackeys - main menu setting
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -2);
    }
}
