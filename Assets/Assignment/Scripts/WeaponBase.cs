using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    Vector2 movement; //The vector towards which the weapon moves
    float rotationValue; //the value of rotation the weapon does
    public float speed; //the speed of the weapon
    Rigidbody2D rb; //the weapon's rigid body

    float points; //the points that the weapon gives when killed by the knight

    // Start is called before the first frame update

    //Weapon's object constructor to be overriden by the other weapons
    WeaponBase()
    {
        //setting base values for the weapon class
        rotationValue = 0;
        movement = Vector2.zero;
        speed = 1;
        points = 10;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //Gets the weapon's rigidbody
    }

    //Empty fixedUpdate that will be overriden by classes inhereting from the weapon base
    //Used specifically to move the weapons in their own unique ways
    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 10); //Destroy the game object after 10 seconds by default
    }

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
    //This will have more functionality later
    public void weaponDeath()
    {
        GameObject.Find("Manager").SendMessage("scoreUpdate", points); //increase the score in main screen
        Destroy(gameObject); //destroy the object
    }
}
