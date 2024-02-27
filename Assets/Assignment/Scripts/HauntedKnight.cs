using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

    public float health; //the knight's current health
    public float maxHealth = 5; //the knight's max health
    bool isDead = false; //a boolean to check if the knight has died

    // Start sets up important variables used by the haunted knight
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>(); //get the knight's rigidBody
        animator = GetComponent<Animator>(); //get the knight's animator
        health = maxHealth; //set health to max
    }

    //FixedUpdate is used to perform he knight's movement.
    private void FixedUpdate()
    {
        if (isDead) return; //stop this function is the knight is dead

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

        //when right-click is pressed on the mouse
        if (Input.GetMouseButtonDown(1))
        {
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
            //SendMessage()
        }
        else
        {
            isDead = false; //this is very much not efficient but works for now
            animator.SetTrigger("TakeDamage");
        }
    }
    public void Attack()
    {
        if (health > 0)
        {
            animator.SetTrigger("Attack");
        }
    }

    //This is called alongside the healthbar version of this function
    //Effectively, this allows me to check if health == 0, and if it is, the death animation plays
    public void savedValue()
    {
        if (health == 0)
        {
            //die? lol?
            isDead = true;
            animator.SetTrigger("Death");
        }
    }
}