using UnityEngine;

public class MovePillar : MonoBehaviour
{
    public float speed;  
    public float moveDistance = 3f;      
    private Vector2 startPos;
    private Vector2 targetPos;
    private bool playerOnPlatform = false;

    void Start()
    {
        startPos = transform.position;
        targetPos = new Vector2(startPos.x, startPos.y + moveDistance);
    }

    void Update()
    {
        if (playerOnPlatform)
        {
            transform.position = Vector2.MoveTowards(transform.position, 
            targetPos, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, 
            startPos, speed * Time.deltaTime);
        }
    }

    public void SetPlayerOnPlatform(bool state)
    {
        playerOnPlatform = state;
    }

    //makes player move with platform
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.position.y>transform.position.y){
            collision.transform.SetParent(transform); 
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }

}
