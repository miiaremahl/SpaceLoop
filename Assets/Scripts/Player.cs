
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Player logic
 * Miia Remahl 
 * mrema003@gold.ac.uk
 * 
 * References:
 * 1.Code Monkey - Simple Jump in Unity 2D (Unity Tutorial for Beginners) : https://www.youtube.com/watch?v=ptvK4Fp5vRY
 * 2.Alan Zucconi - Learn.gold Nyan cat tutorial 
 * 3.Brackeys - How to make Doodle Jump in Unity (Livestream) https://www.youtube.com/watch?v=fHN-26GEVhA&feature=emb_title
 * 4.Alan Zucconi - Learn.gold Collison 2D
 * 5. Sauciminator - How to call on collision enter only once https://answers.unity.com/questions/1439048/how-to-call-on-collision-enter-only-once.html
 * 6. thereisnotrying - How to find index of colliding Game Object in OnCollisionEnter2D() https://answers.unity.com/questions/1172700/how-to-find-index-of-colliding-game-object-in-onco.html
 * 7. Kyle Suchar - 2D Player Movement and Jumping in Unity https://www.youtube.com/watch?v=L6Q6VHueWnU
 * 8. Distorted Pixel Studios - 2D Character Movement in Unity / 2020 https://www.youtube.com/watch?v=n4N9VEA2GFo
 * 9. Press start - Unity Movement [RigidBody vs Translate] https://www.youtube.com/watch?v=ixM2W2tPn6c
 */

public class Player : MonoBehaviour
{
    //how much jump effects (moves up)
    public float verticalMovementUp = 3f;

    //jump speed
    public float Jumpspeed = 10f;

    //movement
    public float horizontal;
    public float vertical;

    //Movement speed
    public float MovementSpeed = 10f;

    
    //Does the playr tocuh the planet (for jumping)
    public bool TouchesPlanet = false;

    //audio jumping
    public AudioSource jump;

    //audio collision
    public AudioSource collisionSound;
    public AudioSource bonusSound;

    //player state
    public enum State
    {
        Running,
        Jumping
    }
    //reference to haealthbar
    public HealthBar healthBar;

    //value of the bonus (added to health)
    public float bonusValue = 10f;

    //Max heatlh for playing
    public float MaxHealth = 100;

    //Max heatlh for playing
    public float CurrentHealth;


    //amount of bonuses collected by player
    public float collectedBonuses = 0f;

    //current state
    public State CurrentState;

    //Rigidbody
    public Rigidbody2D rb;

    //number of rounds player had survived
    public int survivedRounds = 0;


    //Layermask for detecting planet
    //[SerializeField] private LayerMask planetMask;

    //OLD
    // public BoxCollider2D boxCollider;

    //OLD
    //public float MovementSpeed = 10f;

    //old
    //public float jumpVelocity = 1f;




    void Start()
    {
        //set the first state
        CurrentState = State.Running;

        //set health
        CurrentHealth = MaxHealth;
        healthBar.SetMaxHealth((float)MaxHealth);

        //old
        //boxCollider = transform.GetComponent<BoxCollider2D>();

        //gets/sets rigidbody
        rb = transform.GetComponent<Rigidbody2D>();

    }

    //Used refs:2 Alan Zucconi (up keys) --> changed later to Physics reference
    void Update()
    {
        //take movement 
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        switch (CurrentState)
        {
            case State.Running:
                checkMovement();
                break;

            case State.Jumping:
                //checks if player is touching planet
                if (TouchesPlanet)
                {
                    CurrentState = State.Running;
                }
                //Handles movement
                moveCharacter();
                break;
        }
    }

    //Checks up movement
    private void checkMovement()
    {
        //Jumps if touching planet, keys: general upkeys
        if (TouchesPlanet && vertical > 0)
        {
            CurrentState = State.Jumping;
            Jump();
        }
        //character movement
        moveCharacter();
    }



    //basic Movement
    //refs: 9. Press start -> movement for rigidBodies
    void moveCharacter()
    {
        //movinf the character
        Vector2 horizontalMovement = new Vector2(horizontal, 0f);
        rb.AddForce(horizontalMovement * MovementSpeed);

        //rotation if needed
        if (!Mathf.Approximately(0, horizontal))
        {
           transform.rotation = horizontal > 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
        }
    }

    //Jumping
    //refs: 9. Press start -> movement for rigidBodies 
    void Jump()
    {
        jump.Play();
        Vector2 verticalMovement = new Vector2(0f, verticalMovementUp);
        rb.AddForce(verticalMovement * Jumpspeed);
    }


    //Entered collision 
    //refs:4.Alan Zucconi
    //6. thereisnotrying - how to get collided element
    void OnCollisionEnter2D(Collision2D collision)
    {
        //if collision object has tag 'Crater' make damage to player
        if (collision.gameObject.CompareTag("Craters"))
        {
            //gets the collided crater
            Crater cr = collision.gameObject.GetComponent<Crater>();
            float dmg = cr.damage;
            bool damaged = cr.hasDamaged;

            //checks if crater has done damage already
            //ref:  5. Sauciminator -> how to prefent from taking damage twice from same obj
            if (!damaged)
            {
                collisionSound.Play();
                //sets damaged to true (can't damage twice)
                cr.hasDamaged = true;

                //substracts the crater damage from health
                CurrentHealth = dmg > CurrentHealth ? 0 : CurrentHealth - cr.damage;
                healthBar.SetHealth(CurrentHealth);
            }
        }
        //if collision object has tag meteor make damage to player
        if (collision.gameObject.CompareTag("Meteor"))
        {
            collisionSound.Play();

            //gets the collided meteor
            Meteor mt = collision.gameObject.GetComponent<Meteor>();
            float dmg = mt.damage;

            //substracts the meteor damage from health
            CurrentHealth = dmg > CurrentHealth ? 0 : CurrentHealth - dmg;
            healthBar.SetHealth(CurrentHealth);
        }

        //If health is under 1 end the game
        if (CurrentHealth == 0)
        {
            EndGame();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        //Bonuspoint -> adds health and points
        if (collider.gameObject.CompareTag("BonusPoint"))
        {
            Destroy(collider.gameObject);
            bonusSound.Play();
            CurrentHealth = CurrentHealth + bonusValue > MaxHealth ? MaxHealth : CurrentHealth + bonusValue;
            //add health and points
            healthBar.SetHealth(CurrentHealth);
            collectedBonuses += 1;
        }


        //if collided with flag add rounds
        if (collider.gameObject.CompareTag("Flag"))
        {
            //gets the collided flag
            Flag flag = collider.gameObject.GetComponent<Flag>();
            bool marked = flag.marked;

            //ref:  5. Sauciminator -> how to prefent from colliding twice per round
            if (!marked)
            {
                //sets amrked to true (can't go twice per round)
                flag.marked = true;

                //add survived rounds
                survivedRounds += 1;
            }
        }

        //if player hits the bottom line -> game over
        if (collider.gameObject.CompareTag("BottomLine"))
        {
            CurrentHealth = 0;
            EndGame();
        }
    }

    //End the game (calls general logic)
    void EndGame()
    {
        FindObjectOfType<GeneralLogic>().EndGame();
        Destroy(gameObject);
    }

    //OLD CODE
    //used refs:1 Code Monkey, 3.Brackeys (freezed rotation fix),7. Kyle Suchar
    //private void Jump()
    //{
    //    rb.velocity = Vector2.up * jumpVelocity;
    //}

    //private bool TouchesPlanet() --> used this before for detecting if we hit planet but this didn't work properly
    //{
    //    RaycastHit2D rayCast = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 1f, planetMask);
    //    return rayCast.collider != null;
    //}


    //OLD CODE
    //ref: 8. Distorted Pixel Studios - took advice on how to rotate the character while moving
    //private void Movement()
    //{
    //    //detect movement keys being pressed
    //    var horizontal = Input.GetAxis("Horizontal");
    //    //movement
    //    transform.position += new Vector3(horizontal, 0, 0) * Time.deltaTime * MovementSpeed;

    //    //rotating character
    //    if (!Mathf.Approximately(0, horizontal))
    //    {
    //        transform.rotation = horizontal < 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
    //    }

    //}



}
