using System;
using UnityEngine;

public class IsHit : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void action()
    {
        if(this.GetComponent<basicEnemy>() != null)
        {
            basicEnemy enemy = this.GetComponent<basicEnemy>();
            //enemy.loseHealth();
        }
    }
}
