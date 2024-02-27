using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class serves the purpose of allowing me to create object iteration. 
public class WeaponBase : MonoBehaviour
{
    Vector2 movement; //The vector towards which the weapon moves
    float rotationValue; //the value of rotation the weapon does
    public float speed; //the speed of the weapon
    Rigidbody2D rb; //the weapon's rigid body

    float points; //the points that the weapon gives when killed by the knight

    //Weapon's object constructor to be overriden by the other weapons
    //I will keep this around, however in the case that I can do this more efficiently by simply using a mix of prefabs and public values, it will be removed
    //It all largely depends on how object inheritence is handled in unity.
    WeaponBase()
    {
        //setting base values for the weapon class
        rotationValue = 0;
        movement = Vector2.zero;
        speed = 1;
        points = 10;
    }

    // Update is called once per frame
    //This update function can and likely will be overriden by some of the enemy subclasses
    //for now, however, it remains purely to serve the purpose of destroying the gameObject after a set period of time to prevent lag issues
    void Update()
    {
        Destroy(gameObject, 10); //Destroy the game object after 10 seconds by default
    }

    //script that checks whenever a weapon has collided with another gameObject
    //if it collides with the player, it'll deal damage to them and destroy itself
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Only destroys on collision with the player (same for take damage)
        if (collision.gameObject == GameObject.Find("Haunted Knight")) //find out if you are colliding with the knight
        {
            collision.gameObject.SendMessage("TakeDamage", 1); //damage the knight
            Destroy(gameObject); //destroy the object
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
