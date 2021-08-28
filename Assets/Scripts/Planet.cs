using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Controls planet movement.
 * Miia Remahl 
 * mrema003@gold.ac.uk
 * 
 * References:
 * 1.Alan Zucconi - Learn.gold Course material (for rotation)
 * 2.Unity documentation - Object.Instantiate https://docs.unity3d.com/ScriptReference/Object.Instantiate.html
 * 3. robertbu - Spawn prefabs in a circle? https://answers.unity.com/questions/714835/best-way-to-spawn-prefabs-in-a-circle.html
 * 4. https://forum.unity.com/threads/converting-a-double-to-a-float.102712/
 * 5. Heke - [SOLVED]How to change Quaternion by 180 degrees? https://answers.unity.com/questions/476128/how-to-change-quaternion-by-180-degrees.html
 * 6. robertbu - How to make an Instantiated prefab a Child of a gameobject that already exists in the hierarchy https://answers.unity.com/questions/586985/how-to-make-an-instantiated-prefab-a-child-of-a-ga.html
 * 7.Alan Zucconi - Learn.gold Course material (for coroutines)
 */
public class Planet : MonoBehaviour
{

    //rotating speed
    public float RotatingSpeed;

    //prefab for crater
    public Crater craterPreFab;

    //radius of the planet
    public float radius = 4f;

    //crater spawn times for random function
    public float spawnMin = 0.9f;
    public float spawnMax = 1.9f;


    //crater spawn times for random function
    public float bonusSpawnMin = 3f;
    public float bonusSpawnMax = 6f;

    //reference to general logic
    public GeneralLogic gl;




    void Start()
    {
        //Set rotating speed for planet
        RotatingSpeed = 100f;

        //Spawns the first crater
        SpawnCrater();

        //starts the timer that spawns craters
        StartCoroutine(CraterTimer());

    }

    void Update()
    {
        //if playing rotate planet
        if (gl.playing)
        {
            RotatePlanet();
        }
    }

    //Rotates planet with given speed
    //refs:1.Alan Zucconi
    void RotatePlanet()
    {
        transform.Rotate(0, 0, RotatingSpeed * Time.deltaTime);
    }

    //spawns crater to the bottom of the planet
    //refs: 2.Unity documentation (Initantiate)
    void SpawnCrater()
    {
        //Counts position for the prefab
        //ref: 3. robertbu ---> used the circle calculation but changed code for context
        Vector3 position;
        int angle = 180;
        Vector3 center = transform.position;
        position.x = center.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        position.y = center.y + radius * Mathf.Cos(angle * Mathf.Deg2Rad)-0.02f;
        position.z = center.z;

        //Rotating the prefab (some inspo from ref.5 Heke)
        Vector3 rotation = new Vector3(0, 0, 180);


        //Making prefab the child of planet (inspo ref. 6. robertbu)
        Crater newCrater = Instantiate(craterPreFab, position, Quaternion.Euler(rotation));
        newCrater.transform.parent = GameObject.Find("PlanetLines").transform; ;
    }

    //Waits a random amount of time 
    //ref:7.Alan Zucconi (coroutines,for secs to wait)
    IEnumerator CraterTimer(){
        while (gl.playing)
        {
            float secondsToWait = Random.Range(spawnMin, spawnMax);
            yield return new WaitForSeconds(secondsToWait);
            //spawn a crater
            SpawnCrater();
        }
    }

}
