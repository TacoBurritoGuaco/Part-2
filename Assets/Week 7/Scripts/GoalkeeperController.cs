using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperController : MonoBehaviour
{
    public Rigidbody2D goalieRb;
    Vector2 direction;
    float mag;
    public float radius;
    // Start is called before the first frame update

    private void Update()
    {
        if (Controller.SelectedPlayer == null) return;
        direction = (Vector2)transform.position - (Vector2)Controller.SelectedPlayer.transform.position; //This gets the vector between the goal and the selected player
        mag = direction.magnitude;
        direction.Normalize(); //I'm normal :D
    }

    private void FixedUpdate()
    {
       if (mag/2 <= radius)
        {
            goalieRb.position = (Vector2)transform.position - direction * (mag / 2);
        }
       else
        {
            goalieRb.position = (Vector2)transform.position - direction * radius;
        }
    }
}
