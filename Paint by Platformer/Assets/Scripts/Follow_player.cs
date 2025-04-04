using System;
using UnityEngine;

public class Follow_player : MonoBehaviour
{
    public Transform player;
    public Vector3 vec;
    //public GameObject end;


    // Update is called once per frame
    void Update () {
        
        transform.position = player.transform.position + vec;
    }

}
