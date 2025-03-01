using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackArea : MonoBehaviour
{
    private int damage = 1;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("HERE");
        if(collider.GetComponent<IsHit>() != null)
        {
            Debug.Log("DETECTED");
            IsHit hit = collider.GetComponent<IsHit>();
            hit.action();
        }
    }
}
