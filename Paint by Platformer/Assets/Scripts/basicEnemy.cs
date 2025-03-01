using UnityEngine;

public class basicEnemy : MonoBehaviour
{
    public float speed = 2f;             // Movement speed of the enemy
    public float detectionDistance = 1f; // Distance for detecting walls or edges
    public LayerMask wallLayer;          // The layer mask to detect walls/obstacles
    public Transform groundCheck;        // The point to check if the enemy is about to fall

    private bool isMovingRight = false;
    private Rigidbody2D rb;

    void Start()
    {
        // Get the Rigidbody2D component for physics-based movement
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        if(DetectObstacle())
        {
            TurnAround();
        }
        // Apply movement based on direction
        if (isMovingRight)
        {
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y); // Move right, keep the y velocity for jumping or falling
        }
        else
        {
            rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y); // Move left, keep the y velocity for jumping or falling
        }
    }

    bool DetectObstacle()
    {
        // Cast a ray ahead of the enemy to detect obstacles
        RaycastHit2D hit = Physics2D.Raycast(transform.position, isMovingRight ? Vector2.right : Vector2.left, detectionDistance, wallLayer);

        // Debug the raycast hit
        if (hit.collider != null)
        {
            Debug.Log("Obstacle detected: " + hit.collider.name);
        }

        return hit.collider != null; // If we hit something, return true
    }

    bool DetectEdge()
    {
        // Check if the enemy is about to fall off the cliff by casting a ray downward
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, detectionDistance, wallLayer);

        // Debug the raycast hit
        if (hit.collider == null)
        {
            Debug.Log("No ground detected, enemy might fall off.");
        }

        return hit.collider == null; // If no ground is detected, return true
    }

    void TurnAround()
    {
        isMovingRight = !isMovingRight; // Reverse the direction
        Debug.Log("Turning around, new direction: " + (isMovingRight ? "Right" : "Left"));
    }
}

