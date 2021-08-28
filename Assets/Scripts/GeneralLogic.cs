using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * General gamemanager for the game.
 * Miia Remahl 
 * mrema003@gold.ac.uk
 * 
 * References:
 * 1.Brackeys - GAME OVER - How to make a Video Game in Unity (E08) https://www.youtube.com/watch?v=VbZ9_C4-Qbo
 * 2.Quick 'n Dirty - 3D Endless Runner Unity: #13 - Main Menu and Game Over Screen - Unity Tutorial / Guide, Indie Devlog https://www.youtube.com/watch?v=nsyc-q_3CGI
 * 3.leftshoe18 - How do I instantiate a prefab with scale? Update 1.2 https://answers.unity.com/questions/1578968/how-do-i-instantiate-a-prefab-with-scale.html
 */

public class GeneralLogic : MonoBehaviour
{
    //Reference to game over menu
    public GameObject gameOverMenu;

    //prefab of the meteor
    public Meteor meteorPF;

    //prefab of bonuspoint
    public BonusPoint bonusPoint;


    //crater spawn times for random function
    public float BonusSpawnMin = 2f;
    public float BonusSpawnMax = 5f;


    //crater spawn times for random function
    public float spawnMin = 0.9f;
    public float spawnMax = 3f;

    //tells if game is on
    public bool playing = true;

    //audio gameover
    public AudioSource gameover;


    //current state of the game
    public State CurrentState;

    //enum for the states
    public enum State
    {
        Playing,
        InScores
    }

    //corners of the canvas (extented play area)
    float YMax = 9f;
    float YMin = -7.00f;

    float StartX = -2.54f;
    float TargetX = -28.26f;

    float meteorMin = 0.05f;
    float mateorMax = 1.02f;


    public void Start()
    {
        //set the state to playing
        CurrentState = State.Playing;

        //starts the timer that spawns meteors
        StartCoroutine(MeteorTimer());

        //stars timer that spawns bonuspoints
        StartCoroutine(BonusTimer());

    }

    //End the game
    //refs: 1.Brackeys - took some tips for ending game
    // 2.for the end screen
    public void EndGame()
    {
        gameover.Play();
        if (CurrentState == State.Playing) {
            playing = false;
            CurrentState = State.InScores;

            //activate gameovermenu
            gameOverMenu.SetActive(true);
        }
    }


    //Restarts the game
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Quit the game (back to menu)
    public void Quit()
    {
        SceneManager.LoadScene(0);
    }

    //Spawns a meteor
    //refs: 3.leftshoe18 -> how to scale prefabs
    void SpawnMeteor()
    {
        //corners of the canvas (extented play area)
        YMax = 9f;
        YMin = -7.00f;

        StartX = -2.54f;
        TargetX = -28.26f;

        //get random scalenumber (for different meteor sizes)
        float ScaleValue = Random.Range(meteorMin, mateorMax);

        //random points where meteor is moving from and to
        Vector3 A = new Vector3(StartX, Random.Range(YMin, YMax), 0f);
        Vector3 B = new Vector3(TargetX, Random.Range(YMin, YMax), 0f);

        //how much scale is going to be changed
        Vector3 scaleChange = new Vector3(ScaleValue, ScaleValue, ScaleValue);

        //make a prefab and set point A and B (from-to-where)
        Meteor newMeteor = Instantiate(meteorPF, A, Quaternion.identity);
        newMeteor.A = A;
        newMeteor.B = B;

        //scale meteor
        newMeteor.transform.localScale += scaleChange;
    }


    //Spawns a bonusitem
    void spawnBonusItem()
    {
        //corners of the canvas (extented play area)
        YMax = 9f;
        YMin = -7.00f;

        StartX = -2.54f;
        TargetX = -28.26f;


        //random points where bonuspoint is moving from and to
        Vector3 A = new Vector3(StartX, Random.Range(YMin, YMax), 0f);
        Vector3 B = new Vector3(TargetX, Random.Range(YMin, YMax), 0f);

        //make a prefab and set point A and B (from-to-where)
        BonusPoint newBonusItem = Instantiate(bonusPoint, A, Quaternion.identity);
        newBonusItem.A = A;
        newBonusItem.B = B;

    }

    //Waits a random amount of time to spawn a meteor
    //ref:7.Alan Zucconi (coroutines,for secs to wait)
    IEnumerator MeteorTimer()
    {
        while (CurrentState == State.Playing)
        {
            SpawnMeteor();
            float secondsToWait = Random.Range(spawnMin, spawnMax);
            yield return new WaitForSeconds(secondsToWait);
        }
    }

    //Waits a random amount of time to spawn a bonuspoint
    //ref:7.Alan Zucconi (coroutines,for secs to wait)
    IEnumerator BonusTimer()
    {
        while (CurrentState == State.Playing)
        {
            float secondsToWait = Random.Range(BonusSpawnMin, BonusSpawnMax);
            yield return new WaitForSeconds(secondsToWait);
            spawnBonusItem();
        }
    }

}
