using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



/*
 * Game scoring. 
 * Miia Remahl 
 * mrema003@gold.ac.uk
 * 
 * References:
 * 1.Brackeys - SCORE & UI - How to make a Video Game in Unity (E07) https://www.youtube.com/watch?v=TAGZxRMloyU ---> used for most of the scoring code
 * 2.Alan Zucconi - Learn.gold Course material (for coroutines)
 */
public class Score : MonoBehaviour
{
    //Referecense to player
    public Player player;

    //Reference to general logic
    public GeneralLogic gl;

    //Score text
    [SerializeField] TextMeshProUGUI scoreText;

    //Lap text
    [SerializeField] TextMeshProUGUI LapText;

    //Score text (endscreen)
    [SerializeField] TextMeshProUGUI EndScreenScoreText;

    //lap text (endscreen)
    [SerializeField] TextMeshProUGUI EndScreenLapText;

    //How much is the bonuspoint worth
    public int BonusValue=10;

    //what time time gives x point in game
    public float pointTime=1f;

    //how many points in x time worth
    public float pointValue=1f;

    //counts the score (every x (pointtime) second give a point)
    public float finalScore = 0;



    void Start()
    {
        //starts the timer that counts points
        StartCoroutine(ScoreTimer());
    }


    void Update()
    {
        SetScores();
    }

    //sets the UI text for score
    //1.Brackeys scoring setting
    void SetScores()
    {
        //setting endscreen scores
        if (!gl.playing)
        {
            EndScreenScoreText.text = CountScore().ToString();
            EndScreenLapText.text = player.survivedRounds.ToString();
        }

        //setting screen scores
        scoreText.text = CountScore().ToString();
        LapText.text= player.survivedRounds.ToString();
    }

    //adds to score every x time
    //refs: 2.Alan Zucconi - coroutines example
    IEnumerator ScoreTimer()
    {
        GeneralLogic flag = gl.gameObject.GetComponent<GeneralLogic>();
        while (gl.playing)
        {
            yield return new WaitForSeconds(pointTime);
            AddToScore((float)pointValue);
        }
    }

    //counts the score (bonuses + timed score)
    float CountScore()
    {
        return finalScore+(player.collectedBonuses* BonusValue);
    }

    //adds to final score given amount
    void AddToScore(float amount)
    {
        finalScore += amount;
    }
}
