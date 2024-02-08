using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    Vector2 destination;
    Vector2 movement; //direction we move in
    public float speed = 3; //Tune depending on animation
    Rigidbody2D rigidbody;
    Animator animator;
    bool clickSelf = false;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        movement = destination - (Vector2)transform.position;

        if (movement.magnitude < 0.1)
        {
            movement = Vector2.zero;
        }
        rigidbody.MovePosition(rigidbody.position + movement.normalized * speed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && !clickSelf)
        {
            destination = Camera.main.ScreenToWorldPoint(Input.mousePosition); //magic trick, remember it for later!
        }
        animator.SetFloat("Move", movement.magnitude);
    }

    private void OnMouseDown()
    {
        clickSelf = true;
        animator.SetTrigger("TakeDamage");
    }
    private void OnMouseUp()
    {
        clickSelf = false;
    }
}
