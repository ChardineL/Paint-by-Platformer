using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    private float horizontal;
    private float vertical;
    private float speed = 6f;
    public float jumpingPower = 15f; //12f;
    public float fallMultiplier = 2.5f; 
    public float lowJumpMultiplier = 2f;
    private bool isFacingRight = true;

    public bool canDash = false;
    private bool isDead = false;
    private bool isDashing;
    private float dashingPower = 200f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    public int numDeaths;
    public float timeInLevel;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask Ground;

    private float checkpointX;
    private float checkpointY;
    Vector2 checkpointpos;
    private void Start()
    {
        checkpointpos = this.transform.position;
        isDead = true;
        numDeaths = -1;
        timeInLevel = 0;
    }



    private void Update()
    {
        DeathManager.AddTime(Time.deltaTime);
        if (isDead)
        {
            transform.position = checkpointpos;
            numDeaths++;
            DeathManager.AddDeath();
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
        if(Input.anyKeyDown){
            Debug.Log(Input.GetKeyDown(KeyCode.Space));
            Debug.Log("grounded: "+IsGrounded());
        }
        if ((Input.GetKeyDown(KeyCode.Space) || (Gamepad.current!=null && Gamepad.current.aButton.wasPressedThisFrame))
         && IsGrounded())
        {
            //Debug.Log(timeInLevel);
            Debug.Log(DeathManager.getDeaths());
            animator.SetFloat("Vertical", 1);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
        }

        if ((Input.GetKeyDown(KeyCode.Space) || (Gamepad.current!=null && Gamepad.current.aButton.wasPressedThisFrame) ) && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }

        if (Gamepad.current.rightTrigger.wasPressedThisFrame && canDash)
        {
            StartCoroutine(Dash());
        }
        //gravity when falling
        if (rb.linearVelocityY < 0) 
        {
            rb.gravityScale = fallMultiplier; // Increase gravity when falling
        } 
        else if (rb.linearVelocityY > 0 && !(Input.GetKey(KeyCode.Space)|| (Gamepad.current != null && Gamepad.current.aButton.isPressed))) 
        {
            rb.gravityScale = lowJumpMultiplier; // Reduce jump height if key is released
        }
        else 
        {
            rb.gravityScale = 1f; // Default gravity when grounded
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
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.CompareTag("Spikes"));
        if(collision.gameObject.CompareTag("Spikes")){
            
            isDead=true;
        }
        
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