using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//The axe works similarly to the knife, choosing a random point on the screen and moving towards it
//However, unlike the knife, the axe starts at a monumentally slow speed when it spawn, which increases the long it is alive for
public class Axe : WeaponBase
{
    float rotAcc; //the acceleration of rotation
    float speedAcc; //the acceleration of speed

    //calls start, and edits values as necessary
    public void Start()
    {
        //Makes sure any values not directly edited are inhereted by the weapon
        //works similarly to a constructor in this sense
        base.Start();
        points = 40; //points gained from defeating a hammer
        speed = 0; //sets speed to 0 at the beginning
        movement = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2)); //a random position that the axe will move towards
    }
    //The axe spins, not unlike the hammer. However, unlike the hammer, both its speed and rotation as not constant, incrementally increasing in speed.
    private void FixedUpdate()
    {
        //Increases acceleration over time, therefore, increasing the rate at which the axe spins and moves
        speedAcc += 0.01f * Time.deltaTime;
        rotAcc += 1f * Time.deltaTime;

        //Increase the rotation value and the speed value exponentially
        rotationValue += rotAcc;
        speed += speedAcc;

        rb.MovePosition(rb.position + movement.normalized * speed * Time.deltaTime); //Moves the weapon towards the knight using rigidbody
        transform.rotation = Quaternion.Euler(0, 0, rotationValue); //quaternion to make sure rotation is not messy (specifically transform rotation)
        rb.MoveRotation(rb.rotation + rotationValue + Time.deltaTime); //rigidbody rotation
    }
}
