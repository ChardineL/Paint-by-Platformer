using UnityEngine;

public class basicEnemy : MonoBehaviour
{
    public float minDist=50.0f;
    public float fMaxX=250.0f;
    public float movingSpeed=20.0f;
    int direction=-1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
                       GetComponent <Rigidbody2D>().velocity = new Vector2(-movingSpeed,GetComponent<Rigidbody2D>().velocity.y);
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
                        GetComponent <Rigidbody2D>().velocity = new Vector2(movingSpeed,GetComponent<Rigidbody2D>().velocity.y);
                    }
                else
                    {
                        direction = -1;
                    }
            break;
        }
}
