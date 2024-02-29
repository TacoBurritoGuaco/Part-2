using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//This class serves the purpose of allowing me to create object iteration. 
public class WeaponBase : MonoBehaviour
{

    public Vector2 movement; //The vector towards which the weapon moves
    public float rotationValue; //the value of rotation the weapon does
    public float speed; //the speed of the weapon
    public float opacity; //the opacity of the sprite
    public Rigidbody2D rb; //the weapon's rigid body
    LineRenderer lineRenderer;

    public float points; //the points that the weapon gives when killed by the knight
    public float spawnTime; //how long the object has been alive for
    public AnimationCurve fading; //The curve at which the weapon fades away

    //Start function to be overrriden by inherited subclasses
    //To be fully honest, I am shocked at how unbeliavably effective using start instead of a specific constructor is in comparison
    //I had not thought of this on my own however, I would like to point out this advice was given to me by broargr_grabrograbr on discord
    //He is the coder for a game called "Brutal Orchestra". I figured asking for help from him to make sure my inheritance functioned without using the material from week 9
    public void Start()
    {
        //setting base values for the weapon class
        rotationValue = 0; //set rotationValue to 0
        movement = Vector2.zero; //set movement vector to 0
        speed = 1; //get the base speed
        points = 10; //get the base points

        rb = GetComponent<Rigidbody2D>(); //get the rigidBody2d

        transform.position = new Vector3(Random.Range(-7, 8), Random.Range(-4, 5), 0); //moves the object to a random position upon start
    }

    // Update is called once per frame
    //This update function can and likely will be overriden by some of the enemy subclasses
    //for now, however, it remains purely to serve the purpose of destroying the gameObject after a set period of time to prevent lag issues
    public void Update()
    {
        float interpolation = fading.Evaluate(spawnTime);
        transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, interpolation);

        Destroy(gameObject, 8); //Destroy the game object after 8 seconds by default
        spawnTime += 1 * Time.deltaTime; //increases the spawnTime by a second based on the in-game timer
    }

    //script that checks whenever a weapon has collided with another gameObject
    //if it collides with the player, it'll deal damage to them and destroy itself
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Only destroys on collision with the player (same for take damage)
        if (collision.gameObject == GameObject.Find("HauntedKnight") && spawnTime >= 2) //find out if you are colliding with the knight and a second has passed since you spawned
        {
            //If the knight is currently attacking
            //This is done by getting the script component from the haunted knight and calling the isAttacking boolean
            if(collision.gameObject.GetComponent<HauntedKnight>().isAttacking == true) 
            {
                weaponDeath();
            } else
            {
                collision.gameObject.SendMessage("TakeDamage", 1); //damage the knight
                GameObject.Find("Manager").SendMessage("TakeDamage", 1); //update the health bar to potray the damage taken
                Destroy(gameObject); //destroy the object
            }
        }
    }

    //Will be called by the knight whenever a weapon collides with the knight as it is doing its swing animation
    //This is called by the knight using SendMessage
    public void weaponDeath()
    {
        GameObject.Find("Manager").SendMessage("scoreUpdate", points); //increase the score in main screen
        Destroy(gameObject); //destroy the object
    }

    //Simple function that instantly destroys the weapon
    //This is called by the knight using sendMessage whenever the knight is killed using sendMessage.
    public void deathUpdate()
    {
        Destroy(gameObject);
    }
}
