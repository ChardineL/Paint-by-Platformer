using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    private float horizontal;
    private float vertical;
    public float speed = 6f;
    public float jumpingPower = 25f; //12f;
    public float fallMultiplier = 4f;
    public float lowJumpMultiplier = 4f;
    public float gravityscale = 3f;
    private AudioSource dashAudio;
    private bool isFacingRight = true;

    public bool canDash = false;
    private bool isDead = false;
    public bool canDoubleJump = true;
    private bool isDashing;
    private float dashingPower = 25f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    [SerializeField] public ProjectileBehavior ProjectilePrefab;
    [SerializeField] public Transform LaunchOffset;

    public int numDeaths;
    public float timeInLevel;
    public int[] colors = { 0, 1, 2 };
    //red, yellow, blue
    int currentColor = 0;
    int previousColor = 2;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask Ground;

    private float checkpointX;
    private float checkpointY;
    Vector2 checkpointpos;
    public float zoomOutSize = 10f;
    public float zoomInSize = 5f;
    public float zoomDuration = 1f;

    private Coroutine currentZoomCoroutine;

    //private bool isZooming = false;
    private PlayerInput playerInput;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }
    private void Start()
    {
        checkpointpos = this.transform.position;
        isDead = true;
        numDeaths = -1;
        timeInLevel = 0;
        canDash = true;//PlayerPrefs.GetInt("DashUnlocked", 0) == 1;
        dashAudio = GetComponent<AudioSource>();
    }

    public bool FacingRight()
    {
        return isFacingRight;
    }



    private void Update()
    {
        if (Menus.isPaused)
            return;

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
        if (this.transform.position.y < checkpointY - 100)
        {
            isDead = true;
        }

        if (Gamepad.current != null)
        {
            horizontal = Gamepad.current.leftStick.x.ReadValue();
            if (math.abs(horizontal) < 0.1f) horizontal = 0;
        }
        else
        {
            horizontal = Input.GetAxisRaw("Horizontal");
        }

        float vertical = rb.linearVelocity.y;

        // Animation If statemetns
        if (horizontal != 0 && IsGrounded())
        { //Plays walk animation if player is moving on the ground
            animator.SetFloat("Speed", 1);
            animator.SetBool("Grounded", true);
        }
        else if (horizontal == 0 && vertical == 0)
        { //Turns off walk animation if player is idle
            animator.SetFloat("Speed", 0);
            animator.SetBool("Grounded", true);
        }

        if (vertical > 0.01f && !IsGrounded())
        { //Plays jump animation if player is moving upwards
            animator.SetFloat("Vertical", 1);
            animator.SetBool("Grounded", false);
        }
        else if (vertical < -0.01f && !IsGrounded())
        { //Plays fall animation if player is moving downwards
            animator.SetFloat("Vertical", -1);
            animator.SetBool("Grounded", false);
        }
        else if (IsGrounded())
        { //Turns off jump animation if player isn't moving vertically
            animator.SetFloat("Vertical", 0);
            animator.SetBool("Grounded", true);
            canDoubleJump = true;
        }
        /*if (Input.anyKeyDown)
        {
            Debug.Log(Input.GetKeyDown(KeyCode.Space));
            Debug.Log("grounded: " + IsGrounded());
        }*/
        if (!Menus.isPaused &&(Input.GetKeyDown(KeyCode.Space) || (Gamepad.current != null && Gamepad.current.aButton.wasPressedThisFrame))
         && IsGrounded())
        {
            //Debug.Log(timeInLevel);
            Debug.Log("Menu: "+ Menus.isPaused);
            animator.SetFloat("Vertical", 1);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower * 0.6f);
        }
        else if (!Menus.isPaused &&((Input.GetKeyDown(KeyCode.Space) || Gamepad.current != null && Gamepad.current.aButton.wasPressedThisFrame)) && canDoubleJump == true)
        {
            animator.SetFloat("Vertical", 1);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower * 0.6f);
            canDoubleJump = false;
        }

        /*
        if ((Input.GetKeyDown(KeyCode.Space) || (Gamepad.current!=null && Gamepad.current.aButton.wasPressedThisFrame) ) && rb.linearVelocity.y > 0f)
        {
            //rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }
        */

        if (Gamepad.current.rightTrigger.wasPressedThisFrame && canDash)
        {
            StartCoroutine(Dash());
        }
        //gravity when falling
        if (rb.linearVelocityY < 0)
        {
            rb.gravityScale = fallMultiplier; // Increase gravity when falling
        }
        else if (rb.linearVelocityY > 0 && (Input.GetKey(KeyCode.Space) || (Gamepad.current != null && Gamepad.current.aButton.isPressed)))
        {
            //rb.linearVelocityY=0;
            rb.gravityScale = lowJumpMultiplier; // Reduce jump height if key is released
        }
        else
        {
            rb.gravityScale = gravityscale; //2f; // Default gravity when grounded
        }

        if (Gamepad.current.xButton.wasPressedThisFrame)
        {
            Instantiate(ProjectilePrefab, LaunchOffset.position, transform.rotation);
        }
        if (Gamepad.current.rightShoulder.wasPressedThisFrame)
        {
            previousColor = currentColor;
            if (currentColor == 0) currentColor = 1;
            else if (currentColor == 1) currentColor = 2;
            else if (currentColor == 2) currentColor = 0;
        }
        if (Gamepad.current.leftShoulder.wasPressedThisFrame)
        {
            previousColor = currentColor;
            if (currentColor == 0) currentColor = 2;
            else if (currentColor == 1) currentColor = 0;
            else if (currentColor == 2) currentColor = 1;
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
        //Debug.Log(collision.gameObject.CompareTag("Spikes"));
        if (collision.gameObject.CompareTag("Spikes") || collision.gameObject.CompareTag("Enemy"))
        {

            isDead = true;
        }

    }


    private void Flip()
    {

        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            //Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            //localScale.x *= -1f;
            //transform.localScale = localScale;
            transform.Rotate(0f, 180f, 0f);
        }


    }
    private IEnumerator Dash()
    {
        dashAudio.Play();
        canDash = false;
        isDashing = true;
        animator.SetBool("Dash", true);
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        float dashDirection;
        if (isFacingRight)
        {
            dashDirection = 1f;
        }
        else
        {
            dashDirection = -1f;
        }

        rb.linearVelocity = new Vector2(dashDirection * dashingPower, 0f);

        yield return new WaitForSeconds(dashingTime);

        rb.gravityScale = originalGravity;
        isDashing = false;
        animator.SetBool("Dash", false);
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ZoomOut"))
        {
            StartZoom(zoomOutSize); // Zoom out on enter
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ZoomOut"))
        {
            StartZoom(zoomInSize); // Zoom back in on exit
        }
    }

    private void StartZoom(float targetSize)
    {
        if (currentZoomCoroutine != null)
        {
            StopCoroutine(currentZoomCoroutine);
        }

        currentZoomCoroutine = StartCoroutine(ZoomCoroutine(targetSize));
    }

    private IEnumerator ZoomCoroutine(float targetSize)
    {
        float startSize = Camera.main.orthographicSize;
        float elapsed = 0f;

        while (elapsed < zoomDuration)
        {
            elapsed += Time.deltaTime;
            Camera.main.orthographicSize = Mathf.Lerp(startSize, targetSize, elapsed / zoomDuration);
            yield return null;
        }

        Camera.main.orthographicSize = targetSize;
        currentZoomCoroutine = null;
    }

}