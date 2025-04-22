
using UnityEngine;

public class basicEnemy : MonoBehaviour
{
    public float initialposx;
    public float minDist;
    public float maxDist;
    public float movingSpeed;
    int direction=-1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialposx=transform.position.x;
        minDist=initialposx-minDist;
        maxDist+=initialposx;
    }

    // Update is called once per frame
    void Update()
    {
         switch (direction)
        {
             case -1:
                // Moving Left
                if( transform.position.x > minDist)
                    {
                       GetComponent <Rigidbody2D>().linearVelocity = new Vector2(-movingSpeed,GetComponent<Rigidbody2D>().linearVelocityY);
                       //Debug.Log("moving left " +direction);
                       
                    }
                else
                    {
                        Vector3 localScale = transform.localScale;
                       direction = 1;
                       localScale.x *=-1f;
                       transform.localScale=localScale;
                    }
                break;
             case 1:
                  //Moving Right
                if(transform.position.x < maxDist)
                    {
                        GetComponent <Rigidbody2D>().linearVelocity = new Vector2(movingSpeed,GetComponent<Rigidbody2D>().linearVelocityY);
                        //Debug.Log("moving right");
                    }
                else
                    {
                        Vector3 localScale = transform.localScale;
                        direction = -1;
                        localScale.x *=-1f;
                       transform.localScale=localScale;
                    }
                break;
        }
    }
}
