using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{

    //Task 1 variables
    Vector2 destination;
    Vector2 movement; //direction we move in
    public float speed = 3; //Tune depending on animation
    Rigidbody2D rigidbody;
    Animator animator;
    bool clickSelf = false;

    //Task 2 variables
    public float health;
    public float maxHealth = 5;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = maxHealth;
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
        if (Input.GetMouseButtonDown(1))
        {
            Attack();
        }
        if (Input.GetMouseButton(0) && !clickSelf)
        {
            destination = Camera.main.ScreenToWorldPoint(Input.mousePosition); //magic trick, remember it for later!
        }
        animator.SetFloat("Move", movement.magnitude);
    }

    private void OnMouseDown()
    {
        clickSelf = true;
        SendMessage("TakeDamage", 1); //If you have a function called this in ANY object related to this, call it!
    }
    private void OnMouseUp()
    {
        clickSelf = false;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);//saves us from doing if statements
        //Keep something within these bounds

        if (health == 0)
        {
            //die? lol?
            animator.SetTrigger("Death");
        } else
        {
            animator.SetTrigger("TakeDamage");
        } 
    }
    public void Attack()
    {
        if (health > 0)
        {
            animator.SetTrigger("Attack");
        }
    }
}
