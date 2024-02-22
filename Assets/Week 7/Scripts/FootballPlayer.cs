using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;

public class FootballPlayer : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Color oldColor;
    Rigidbody2D rb;
    public float speed = 500;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        oldColor = spriteRenderer.color;
        Selected(false);
    }

    private void OnMouseDown()
    {
        Controller.SetSelectedPlayer(this);
    }
    public void Selected(bool isSelected)
    {
        if (isSelected)
        {
            spriteRenderer.color = Color.yellow;
        }
        else {
            spriteRenderer.color = oldColor;
        }
    }

    public void Move(Vector2 direction)
    {
        rb.AddForce(direction * speed);
    }
}
