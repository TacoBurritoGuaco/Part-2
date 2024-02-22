using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class Ball : MonoBehaviour
{
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Controller.score += 1;
        print(Controller.score);
        rb.position = GameObject.Find("Kick Off Spot").transform.position;
        rb.velocity = Vector2.zero;
    }

    //Added this just for funsies
    //The position of the ball now resets if it goes off to the edge of the screen!
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameObject.Find("Pitch") == collision.gameObject)
        {
            rb.position = GameObject.Find("Kick Off Spot").transform.position;
            rb.velocity = Vector2.zero;
        }
    }
}
