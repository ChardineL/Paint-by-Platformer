using System;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{

    public float Speed = 100.0f;
    //public GameObject commander = Commander.Find("Controller");
    
    public ColorTrack color = GameObject.Find("Color Tracker").GetComponent<ColorTrack>();
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
        Destroy(gameObject);
    }
}
