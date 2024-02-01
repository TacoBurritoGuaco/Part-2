using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public List<Vector2> points;
    public float newPointTreshold = 0.2f;
    Vector2 lastPosition;
    LineRenderer lineRenderer;
    Rigidbody2D rigidbody;
    Vector2 currentPos;
    public float speed = 1;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);

        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        currentPos = transform.position;
        if (points.Count > 0)
        {
            Vector2 direction = points[0] - currentPos;
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg; //Important math commands!
            rigidbody.rotation = -angle; //has to be negative!
        }
        rigidbody.MovePosition(rigidbody.position + (Vector2)transform.up*speed*Time.deltaTime);
    }
    private void Update()
    {
        lineRenderer.SetPosition(0, transform.position);
        if (points.Count > 0)
        {
           if(Vector2.Distance(currentPos, points[0]) < newPointTreshold) 
           { 
                points.RemoveAt(0);
                for(int i = 0; i < lineRenderer.positionCount - 2; i++)
                {
                    lineRenderer.SetPosition(i, lineRenderer.GetPosition(i+1));
                }
                lineRenderer.positionCount--;
           }
        }
    }

    private void OnMouseDown()
    {
        points = new List<Vector2>();
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);
    }

    private void OnMouseDrag()
    {
        Vector2 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Vector2.Distance(lastPosition, newPosition) > newPointTreshold)
        {
            points.Add(newPosition);
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, newPosition);
            lastPosition = newPosition;
        }
    }
}
