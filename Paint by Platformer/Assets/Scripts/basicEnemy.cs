using UnityEngine;

public class basicEnemy : MonoBehaviour
{
    public float initialpos=0;
    public float minDist=50.0f;
    public float maxDist=250.0f;
    public float movingSpeed=20.0f;
    int direction=-1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialpos=transform.position.x;
        minDist+=initialpos;
        maxDist+=initialpos;
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
                    }
                else
                    {
                       direction = 1;
                    }
                break;
             case 1:
                  //Moving Right
                if(transform.position.x < maxDist)
                    {
                        GetComponent <Rigidbody2D>().linearVelocity = new Vector2(movingSpeed,GetComponent<Rigidbody2D>().linearVelocityY);
                    }
                else
                    {
                        direction = -1;
                    }
            break;
        }
    }
}
