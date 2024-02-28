using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A weapon type that inherets from weapon base
//This specific weapon type is simple. When spawned, it rotates a certain amount before moving forwards at a moderate speed
public class Knife : WeaponBase
{
    private void Start()
    {
        base.Start(); //using the rest of the base function's base
        movement = new Vector2 (Random.Range(-1, 2), Random.Range(-1, 2)); //a random position that the knife will move towards
    }
    //Fixed update moves the knife towards a specified point off-screen
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * speed * Time.deltaTime); //Moves towards a specified point in an specified direction
        transform.rotation = Quaternion.Euler(0, 0, Vector2.Angle(transform.position.normalized, movement.normalized)); //quaternion to make sure rotation is not messy (specifically transform rotation)
        rb.MoveRotation(Vector2.Angle(transform.position.normalized, movement.normalized)); //rigidbody rotation done using vector2.angle
        //This function returns the angle between two different vectors, effectively giving us how much the knife should rotate
    }
}
