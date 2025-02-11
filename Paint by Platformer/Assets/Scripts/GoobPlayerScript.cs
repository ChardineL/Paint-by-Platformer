using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

//script adapted from bendux
public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 6f;
    private float jumpingPower = 12f;
    private bool isFacingRight = true;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 200f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask Ground;


    private void Update()
    {
        if (isDashing)
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        if (Gamepad.current.aButton.wasPressedThisFrame && IsGrounded())
        {
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