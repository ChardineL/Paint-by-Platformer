using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

//script adapted from bendux
public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    private float horizontal;
    private float vertical;
    private float speed = 6f;
    private float jumpingPower = 12f;
    private bool isFacingRight = true;

    private bool canDash = true;
    private bool isDead = false;
    private bool isDashing;
    private float dashingPower = 200f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask Ground;

    private float checkpointX;
    private float checkpointY;

    private void Start()
    {
        checkpointX = this.transform.position.x;
        checkpointY = this.transform.position.y;
        isDead = true;
    }


    private void Update()
    {
        if (isDead)
        {
            transform.position = new Vector3 (checkpointX, checkpointY, 0);
            isDead = false;
        }
        if (isDashing)
        {
            return;
        }

        //checking if he's fallen too far from checkpoint (dead)
        if(this.transform.position.y < checkpointY - 100)
        {
            isDead = true;
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = rb.linearVelocity.y;

        // Animation If statemetns
        if (horizontal != 0 && IsGrounded()){ //Plays walk animation if player is moving on the ground
            animator.SetFloat("Speed", 1);
            animator.SetBool("Grounded", true);
        }
        else if(horizontal == 0 && vertical == 0){ //Turns off walk animation if player is idle
            animator.SetFloat("Speed", 0);
            animator.SetBool("Grounded", true);
        }

        if (vertical > 0.01f && !IsGrounded()){ //Plays jump animation if player is moving upwards
            animator.SetFloat("Vertical", 1);
            animator.SetBool("Grounded", false);
        }
        else if(vertical < -0.01f && !IsGrounded()) { //Plays fall animation if player is moving downwards
            animator.SetFloat("Vertical", -1);
            animator.SetBool("Grounded", false);
        }
        else if(IsGrounded()){ //Turns off jump animation if player isn't moving vertically
            animator.SetFloat("Vertical", 0);
            animator.SetBool("Grounded", true);
        }

        if (Gamepad.current.aButton.wasPressedThisFrame && IsGrounded())
        {
            animator.SetFloat("Vertical", 1);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
        }

        if (Gamepad.current.aButton.wasPressedThisFrame && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }

        if (Gamepad.current.rightTrigger.wasPressedThisFrame && canDash)
        {
            StartCoroutine(Dash());
        }

        Flip();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
    
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, Ground);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(transform.localScale.x * dashingPower, 0f);
  
        yield return new WaitForSeconds(dashingTime);

        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}