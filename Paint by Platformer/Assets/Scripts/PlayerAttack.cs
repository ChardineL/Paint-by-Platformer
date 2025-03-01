using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{


    private GameObject attackArea;
    private bool attacking = false;

    private float timeToAttack = 0.25f;
    private float timer = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attackArea = transform.GetChild(2).gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(attackArea.name);
        if (Gamepad.current.xButton.wasPressedThisFrame)
        {
            
            Attack();
        }

        if(attacking)
        {
            timer += Time.deltaTime;

            if(timer >= timeToAttack)
            {
                timer = 0;
                attacking = false;
                //attackArea.SetActive(attacking);
            }
        }
    }

    private void Attack()
    {
        attacking = true;
        attackArea.SetActive(attacking);
    }
}
