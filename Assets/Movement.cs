using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private Animator anim;
    Vector2 movement;

    // variables that handle animation
    public bool isMoving;
    public bool facingRight = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
       //animation control while moving
       isMoving = rb.velocity.x != 0 || rb.velocity.y != 0 ;
       anim.SetBool("IsMoving", isMoving);

       //finding input for movement
       movement.x = Input.GetAxisRaw("Horizontal");
       movement.y = Input.GetAxisRaw("Vertical"); 

        // ------------- was testing flip function with key press
       //if(Input.GetKeyDown(KeyCode.F))
        //Flip();

        // get mouse position
       Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // flips the character based on mouse position
       if (mousePos.x > transform.position.x && !facingRight)
       {
        Flip();
       } 
       else if (mousePos.x < transform.position.x && facingRight)
       {
        Flip();
       }
    }

    private void FixedUpdate() 
    {
        //code that handles movement of character
        movement.Normalize();
        rb.velocity = new Vector2(movement.x * speed * Time.fixedDeltaTime, movement.y * speed * Time.fixedDeltaTime);
    }

    private void Flip() //flip character function
    {
        facingRight = !facingRight; //works as a switcher
        transform.Rotate(0, 180, 0);
    }
}
