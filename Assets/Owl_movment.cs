using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YS;

public class OwlMovement : MonoBehaviour
{
    public Transform flyingPos; 
    public Transform targetPos;
    public PlayerMovement player; 
    private Rigidbody2D rb; 

    public float birdFlyingSpeed = 5f; 
    public bool followTarget = true; 

    private Vector2 idlePosition;
    Vector3 s;

    void Start()
    {
        // Find player and get owl's Rigidbody2D
        player = FindObjectOfType<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();

        // Define the idle position (optional)
        idlePosition = flyingPos != null ? flyingPos.position : transform.position;
    }

    void FixedUpdate()
    {
        HandleOwlDirection();

        if (followTarget)
        {
            FollowPlayer();
        }
        else
        {
            rb.velocity = Vector2.zero;

        }



    }

    private void HandleOwlDirection()
    {
        
        if (transform.position.x < player.transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void FollowPlayer()
    {
        Rigidbody2D playerRB = player.GetComponent<Rigidbody2D>();

        Vector2 moveDirection = new Vector2(player.transform.localScale.x * birdFlyingSpeed, 0);
        //rb.position = Vector2.MoveTowards(rb.position, targetPos.position, birdFlyingSpeed * Time.deltaTime);
        rb.position = Vector3.SmoothDamp(rb.position, targetPos.position, ref s, Time.deltaTime*birdFlyingSpeed);
        //rb.velocity = moveDirection;

        //if (playerRB.velocity == Vector2.zero)
        //{
        //    rb.velocity = Vector2.zero; 
        //    rb.position = Vector2.MoveTowards(rb.position, targetPos.position, birdFlyingSpeed * Time.deltaTime);
        //}
        //else
        //{
            
        //}
    }



    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Owl_Dome"))
        {
            followTarget = false;
        }
    }
        
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Owl_Dome"))
        {
            followTarget = true;
        }
    }
        
}
