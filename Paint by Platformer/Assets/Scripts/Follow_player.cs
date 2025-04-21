using System;
using Unity.Collections;
using UnityEngine;

public class Follow_player : MonoBehaviour
{
    public Transform player;
    public Vector3 vec;
    
    // Update is called once per frame
    void Update () {
        
        transform.position = player.transform.position + vec;
    }

    

}


