using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
    bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteKey("healthLast"); Testing to make sure there is a default value
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = PlayerPrefs.GetFloat("healthLast", 5); //sets health to healthLast at the start of the scene
        SendMessage("savedValue", PlayerPrefs.GetFloat("healthLast")); //sets the healthbar to the healthvalue saved
    }

    private void FixedUpdate()
    {
        if (isDead) return;

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
        

        if (isDead) return;

        if (Input.GetMouseButtonDown(1))
        {
            Attack();
        }
        if (Input.GetMouseButton(0) && !clickSelf && !EventSystem.current.IsPointerOverGameObject()) //returns true any time the mouse object is over a game object
            //This means that this only works when you do not click on UI elements
        {
            destination = Camera.main.ScreenToWorldPoint(Input.mousePosition); //magic trick, remember it for later!
        }
        animator.SetFloat("Move", movement.magnitude);
    }

    private void OnMouseDown()
    {
        if (isDead) return;
        clickSelf = true;
        SendMessage("TakeDamage", 1); //If you have a function called this in ANY object related to this, call it!
    }
    private void OnMouseUp()
    {
        //if (isDead) return; DO NOT DO THIS (reminder for later)
        clickSelf = false;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);//saves us from doing if statements
        //Keep something within these bounds
        PlayerPrefs.SetFloat("healthLast", health); //saves the current health of the knight to our system
        //this should also update this value whenever your health value is affected

        if (health == 0)
        {
            //die? lol?
            isDead = true;
            animator.SetTrigger("Death");
        } else
        {
            isDead = false; //this is very much not efficient but works for now
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

    //This is called alongside the healthbar version of this function
    //Effectively, this allows me to check if health == 0, and if it is, the death animation plays
    public void savedValue()
    {
        if (health == 0)
        {
            //die? lol?
            isDead = true;
            animator.SetTrigger("Death");
        }
    }
}
