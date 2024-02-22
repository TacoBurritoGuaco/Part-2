using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class FootballPlayer : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Color oldColor;
    // Start is called before the first frame update
    void Start()
    {
        Selected(false);
    }

    private void OnMouseDown()
    {
        Selected(true);
    }
    public void Selected(bool isSelected)
    {
        if (isSelected)
        {
            spriteRenderer.color = Color.yellow;
        }
        else {
            oldColor = spriteRenderer.color;
        }
    }
}
