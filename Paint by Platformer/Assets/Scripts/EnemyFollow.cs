using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private GameObject player;
    public float speed;
    public bool chase;
    public float range;
    public Transform startPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player=GameObject.FindGameObjectWithTag("Player");
    }


    public void Update()
    {
        if(player==null)
            return;
        if(chase==true)
            Chase(); 
        else
            ReturnStartPoint();
        if (Vector3.Distance(player.transform.position, transform.position)>range) {
            Debug.Log(Vector3.Distance(player.transform.position, transform.position));
            chase=false;
        } 
    

    }

    private void ReturnStartPoint(){
        transform.position=Vector2.MoveTowards(transform.position, 
        startPoint.position, speed*Time.deltaTime);
    }
    
    private void Chase(){
        transform.position=Vector2.MoveTowards(transform.position, 
        player.transform.position, speed*Time.deltaTime);
    }
}
