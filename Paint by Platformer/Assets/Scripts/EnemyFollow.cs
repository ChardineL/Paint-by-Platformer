
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private GameObject player;
    private PlayerMovement playerScript;
    public float speed;
    public bool chase;
    public float range;
    public Transform startPoint;
    private bool hasReset = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerScript = player.GetComponent<PlayerMovement>();
        }
    }


    public void Update()
    {
        if (player == null)
            return;
        //playerScript = player.GetComponent<PlayerMovement>();
        Debug.Log("died? " + playerScript.isDead + "printing... " + chase + " reset? " + hasReset);
        if (playerScript.isDead)
        {
            // if (!hasReset)
            // {
                chase = false;
                transform.position = startPoint.position;
                //hasReset = true;
                return;
            // }
            
        }
        // else
        // {
        //     hasReset = false;
        // }

        if (chase)
            Chase();
        else
            ReturnStartPoint();
        if (Vector3.Distance(player.transform.position, transform.position) > range)
        {
            // Debug.Log(Vector3.Distance(player.transform.position, transform.position));
            chase = false;
        }


    }

    private void ReturnStartPoint()
    {
        FlipSprite(player.transform.position);
        transform.position = Vector2.MoveTowards(transform.position,
        startPoint.position, speed * Time.deltaTime);
    }

    private void Chase()
    {
        FlipSprite(player.transform.position);
        transform.position = Vector2.MoveTowards(transform.position,
        player.transform.position, speed * Time.deltaTime);
    }
    private void FlipSprite(Vector2 targetPosition)
{
    Vector2 direction = targetPosition - (Vector2)transform.position;
    
    if (direction.x > 0 && transform.localScale.x < 0)
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    else if (direction.x < 0 && transform.localScale.x > 0)
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
}
