using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    Vector2 movement;
    public float speed = 1;
    Rigidbody2D rb;
    float rotationValue;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rotationValue += 20;
        movement = (Vector2)GameObject.Find("Knight").transform.position - (Vector2)transform.position;
        rb.MovePosition(rb.position + movement.normalized * speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, rotationValue);
        rb.MoveRotation(rb.rotation + rotationValue + Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 10); //Destroy the game object after 10 seconds
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Only destroys on collision with the player (same for take damage)
        if(collision.gameObject == GameObject.Find("Knight"))
        {
            collision.gameObject.SendMessage("TakeDamage", 1);
            Destroy(gameObject);
        }
    }
}
