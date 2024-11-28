using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace YS
{
    public class PlayerMovement : MonoBehaviour
    {
        public Rigidbody2D rb;
        public Animator anim;
        PlayerStates state;

        public bool wallJump=true;

        public  float horizontalMov;
        public float jumpHorizontalMov;

        [SerializeField]
        int speed = 5;
        [SerializeField]
        int jumpForce = 10;
        [SerializeField]
        public Transform wallCheck;
        [SerializeField]
        LayerMask wallLayer;
        [SerializeField]
        Transform groundCheck;
        [SerializeField]
        LayerMask groundLayer;


        [Header("Bools")]
        public bool isJump;
        public bool isGrounded=true;
        public bool canDoWallJump;
        public bool isWallSliding;
        private float wallSlidingSpeed = 2;


        [Header("Wall Jump")]
        public bool isWallJumping;
        public float wallJumpingDirection;
        public float wallJumpingTime=.1f;
        public float wallJumpingCounter;
        public float wallJumpingDuration = .4f;
        private Vector2 wallJumpPower = new Vector2(4f, 8f);
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponentInChildren<Animator>();
            state = GetComponent<PlayerStates>();
        }




        private void FixedUpdate()
        {
            

            if(wallJump)
            {
                canDoWallJump = isWalled();

            }

            isGrounded=isGround();

            if (!state.isDead)
            {
                anim.SetBool("isGrounded", isGrounded);
                anim.SetFloat("Speed", Mathf.Abs(horizontalMov));
                WallSlide();
                WallJump();
                Movement();
                Jump();
            }
            else
            {
                anim.SetBool("isGrounded", isGrounded);
                anim.SetFloat("Speed",0);

            }
            
        }

        #region Movement
        private void WallSlide()
        {
            jumpHorizontalMov= Input.GetAxis("Horizontal");
            if ((isWalled()&&!isGrounded&&jumpHorizontalMov!=0))
            {
                isWallSliding = true;
                rb.velocity=new Vector2(rb.velocity.x,Mathf.Clamp(rb.velocity.y,-wallSlidingSpeed,float.MaxValue));
            }
            else
            {
                isWallSliding=false;
            }
        }
        private void WallJump()
        {
            //if ((isWallSliding))
            //{
            //    isWallJumping = false;
            //    wallJumpingDirection=-transform.localScale.x;
            //    wallJumpingCounter = wallJumpingTime;

            //    CancelInvoke(nameof(StopWallJumping));
            //}
            //else
            //{
            //    wallJumpingCounter-=Time.deltaTime;
            //}

            //if(Input.GetKey(KeyCode.Space)&&wallJumpingCounter>0f)
            //{
            //    isWallJumping=true;
            //    rb.velocity = new Vector2(wallJumpingDuration * wallJumpPower.x, wallJumpPower.y);
            //    wallJumpingCounter=0;

            //    if(transform.localScale.x!=wallJumpingDirection)
            //    {
            //        gameObject.transform.localScale = new Vector3(-transform.localScale.x, 1, 1);

            //    }
            //    anim.Play("Jump");
            //    Invoke(nameof(StopWallJumping),wallJumpingDirection);
            //}

            if (isWallSliding)
            {
                // Determine wallJumpingDirection based on which wall you're on
                wallJumpingDirection = -transform.localScale.x; // This handles flipping the player
                wallJumpingCounter = wallJumpingTime;

                CancelInvoke(nameof(StopWallJumping));
            }
            else
            {
                wallJumpingCounter -= Time.deltaTime;
            }

            // Check for wall jump input
            if (Input.GetKey(KeyCode.Space) && wallJumpingCounter > 0f)
            {
                isWallJumping = true;

                // Set the jump direction based on the wall side
                float jumpDirection = (transform.localScale.x > 0) ? -1 : 1; // Right wall gives left jump

                rb.velocity = new Vector2(wallJumpingDuration * wallJumpPower.x * jumpDirection, wallJumpPower.y);
                wallJumpingCounter = 0;

                // Flip the player's sprite based on the wall jump direction
                if (transform.localScale.x != wallJumpingDirection)
                {
                    gameObject.transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
                }
                anim.Play("Jump");
                Invoke(nameof(StopWallJumping), wallJumpingDirection);
            }
        }
        private void StopWallJumping()
        {
            isWallJumping = false;
        }
        private void Movement()
        {
            horizontalMov = Input.GetAxis("Horizontal");

            if (!isGrounded)
            {
                //if (!isWallJumping)
                //{
                //    if (horizontalMov < -0.1f)
                //    {
                //        gameObject.transform.localScale = new Vector3(-1, 1, 1);
                //    }
                //    else
                //    {
                //        gameObject.transform.localScale = Vector3.one;
                //    }
                //}

                //rb.velocity = new Vector2( horizontalMov * inAirSpeed, rb.velocity.y);
            }
            else
            {
                if (!isWallJumping)
                {
                    if (horizontalMov < -0.1f)
                    {
                        gameObject.transform.localScale = new Vector3(-1, 1, 1);
                    }
                    else
                    {
                        gameObject.transform.localScale = Vector3.one;
                    }
                }

                rb.velocity = new Vector2(horizontalMov * speed, rb.velocity.y);
            }
            

            

        }
        private void Jump()
        {
            if (!isGrounded)
            {
                return;

            }

            
            if(Input.GetKey(KeyCode.Space))
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpForce);
                isGrounded = false;
                //anim
                anim.Play("Jump");


            }

        }
        #endregion

        #region Colliders
        private bool isWalled()
        {
            return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
        }
        private bool isGround()
        {
            return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag=="Enemy")
            {

            }
        }
        #endregion


    }

}
