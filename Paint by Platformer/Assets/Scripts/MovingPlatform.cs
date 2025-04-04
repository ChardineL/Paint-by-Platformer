using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed;
    public int startingPoint;
    public Transform[] points;
    private int i;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position=points[startingPoint].position;   //starts at this position
    }

    // Update is called once per frame
    void Update()
    {
        //checking distance between platform and point
        if(Vector2.Distance(transform.position, points[i].position)<0.02f){
            i++; //goes to next position
            if(i==points.Length){
                i=0;//resets
            }
        }
        transform.position=Vector2.MoveTowards(transform.position,points[i].position, 
        speed*Time.deltaTime); //moves platform at a set speed
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
