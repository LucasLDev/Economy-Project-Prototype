using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameManager gameManager;
    
    public Rigidbody2D rb;
    public Camera cam;

    private Animator animator;

    Vector2 movement;
    Vector2 mousePos;

    void Start()
    {
        
        animator = GetComponent<Animator>();
        animator.SetBool("move", false);
    }

    void Update()
    {
        //input
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            animator.SetBool("move", true);
        } else {
            animator.SetBool("move", false);
        }
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        //movement
        rb.MovePosition(rb.position + movement * gameManager.playerMoveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;


    }

    
}
