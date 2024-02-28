using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

//Class that inherets from weaponbase to create the hammer enemy
//The hammer will spin slowly, while aproaching the player
//Learned how to do inheretance in unity through https://learn.unity.com/tutorial/inheritance/?tab=overview#5c8924f2edbc2a0d28f48439
public class Hammer : WeaponBase
{
    //calls start, and edits values as necessary
    public void Start()
    {
        //Makes sure any values not directly edited are inhereted by the weapon
        //works similarly to a constructor in this sense
        base.Start();
        points = 15; //points gained from defeating a hammer
        
    }
    //fixedUpdate creates the spinning behavior of the hammer
    //The hammer was done previously in the week 5 assignment
    //I am reusing it here to save time, and it works the exact same, just using inheretance
    private void FixedUpdate()
    {
        rotationValue += 20; //increase rotation by 20
        movement = (Vector2)GameObject.Find("HauntedKnight").transform.position - (Vector2)transform.position; //displacement vector from knight position and the weapon's position
        rb.MovePosition(rb.position + movement.normalized * speed * Time.deltaTime); //Moves the weapon towards the knight using rigidbody
        transform.rotation = Quaternion.Euler(0, 0, rotationValue); //quaternion to make sure rotation is not messy (specifically transform rotation)
        rb.MoveRotation(rb.rotation + rotationValue + Time.deltaTime); //rigidbody rotation
    }
}
