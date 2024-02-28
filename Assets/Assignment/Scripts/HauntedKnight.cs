using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

//This is the knight script that will be used in the assignment project
//It is, for the most part, an edited version of the already existing knight script, with differences to accomodate for the differences between week 5 and this assignment
public class HauntedKnight : MonoBehaviour
{
    Vector2 destination; //the knight's destination
    Vector2 movement; //the knight's direction they move towards
    public float speed = 3; //The speed of the knight's animation
    Rigidbody2D rigidbody; //The knight's rigidbody
    Animator animator; //The knight's animator
    bool clickSelf = false; //A boolean to check if the knight has clicked on themselves
    bool isDead = false; //a boolean to check if the knight has died
    public bool isAttacking = false; //a boolean that checks if the knight is attacking
    //thi particular boolean is public so that it may be accessed by WeaponBase

    public float health; //the knight's current health
    public float maxHealth = 5; //the knight's max health

    float deathTimer = 0; //a timer used for when the knight dies 
    float attackTimer = 0; //a timer that determines whether or not the knight has stopped attacking

    // Start sets up important variables used by the haunted knight
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>(); //get the knight's rigidBody
        animator = GetComponent<Animator>(); //get the knight's animator
        health = maxHealth; //set health to max
        GameObject.Find("Manager").SendMessage("savedValue", maxHealth); //sets the healthbar to the max healthValue
        //This is done by finding the manager object and sending a message to it
    }

    //FixedUpdate is used to perform he knight's movement.
    private void FixedUpdate()
    {
        //When the knight dies
        if (isDead)
        {
            deathTimer += 1 * Time.deltaTime; //increases this timer by seconds
            //if 1 second has passed after death
            if (deathTimer >= 1f) {
                GameObject.Find("Manager").SendMessage("deathUpdate"); //sends a message to the manager to run deathUpdate, updating the score and the time playerPrefs
                SceneManager.LoadScene(2); //loads the second scene, being the game over screen
            }     
            return; //returs to prevent the knight from being able to move while dead
        }

        movement = destination - (Vector2)transform.position; //substracts the destination by the position of the knight to get an in-between vector
        //if either the y or x of destination (mouseposition) is greater than the player's own x and y coords
        if (destination.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0); //rotate the player on the y axis by 180
            //this will cause the knight to turn to the opposite side
        }
        //if either the y or x of destination (mouseposition) is less than the player's own x and y coords
        if (destination.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0); //rotate the player on the y axis by 180
            //this will cause the knight to turn to the opposite side
        }

        //If the maginute of the movement is less than 0.1 (a really small number)
        if (movement.magnitude < 0.1)
        {
            movement = Vector2.zero; //movement is set to zero, stopping the knight from moving
        }
        rigidbody.MovePosition(rigidbody.position + movement.normalized * speed * Time.deltaTime); //the knight is moved towards the position of the mouse by using their rigidbody position
        //times the normalized movement vector (the direction) times our speed * time.deltaTime
    }

    // Update is used to determine the knight's movement.
    void Update()
    {

        if (isDead) return; //stop this function if the knight is dead

        //If the player is currently attacking
        if (isAttacking)
        {
            attackTimer += 1 * Time.deltaTime; //increase attacktimer based on real time
            if (attackTimer >= 1f) //if the attacktimer is greater than or equal to 1
            {
                isAttacking = false; //set "isAttacking" to false
                attackTimer = 0;
            }
        }
        //when right-click is pressed on the mouse
        if (Input.GetMouseButtonDown(1))
        {
            //if attackTimer is currently reset
            if (attackTimer <= 0)
            {
                isAttacking = true; //set isAttack equal to true
            }
            Attack(); //call the "attack" function
        }
        if (Input.GetMouseButton(0) && !clickSelf && !EventSystem.current.IsPointerOverGameObject()) //returns true any time the mouse object is over a game object
                                                                                                     //This means that this only works when you do not click on UI elements
        {
            destination = Camera.main.ScreenToWorldPoint(Input.mousePosition); //sets the destination to the point on the screen where the mouse has clicked
        }
        animator.SetFloat("Move", movement.magnitude); //sets the float of the animator called "Move" to the movement's magnitude
        //This determines whether or not the knight should be using their walking animation
    }

    //Function called when the mouse is pressed down
    private void OnMouseDown()
    {
        clickSelf = true; //set "clickself" to true, preventing the knight from moving if they click on themselves
        //TakeDamage(1); //used for testing, damages the player
        //GameObject.Find("Manager").SendMessage("TakeDamage", 1); //finds the manager object, and tells it to take damage (effectively, lowering the healthBar)
    }
    //function called when the mouse is pressed up
    private void OnMouseUp()
    {
        clickSelf = false; //set clickself back to false, allowing the knight to move again should they click anywhere else on screen
    }

    //This function is called upon the knight making contact with an enemy (weapons) to determine when they should lose health or die
    public void TakeDamage(float damage)
    {
        health -= damage; //reduces health by a inputted value (damage)
        health = Mathf.Clamp(health, 0, maxHealth);//Forces a minimum and maximum value for the health value

        //If health has reached 0
        if (health == 0)
        {
            isDead = true; //set isDead to true (the knight has died)
            animator.SetTrigger("Death"); //trigger the "death" animation
        }
        else
        {
            isDead = false; //this is very much not efficient but works for now
            animator.SetTrigger("TakeDamage");
        }
    }
    //Function that attacks the player
    public void Attack()
    {
        if (health > 0) //if the health is greater than 0
        {
            animator.SetTrigger("Attack"); //perform the attack animation
        }
    }

    //This is called alongside the healthbar version of this function
    //Effectively, this allows me to check if health == 0, and if it is, the death animation plays
    public void savedValue()
    {
        if (health == 0) //if the health equals 0
        {
            //die? lol?
            isDead = true; //set the player to be dead
            animator.SetTrigger("Death"); //animate the player's death animation
        }
    }
}