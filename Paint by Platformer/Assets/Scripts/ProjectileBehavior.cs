using System;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{

    public float Speed = 100.0f;
    //public GameObject commander = Commander.Find("Controller");
    
    bool right;

    private void Start()
    {
        GetComponent<Renderer>().material.color = Color.red; 
        //right = commander.FacingRight();
    }

    void Update()
    {   
            transform.position += 5 * transform.right * Time.deltaTime * Speed;  
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        
    }
}
