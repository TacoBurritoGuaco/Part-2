using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
