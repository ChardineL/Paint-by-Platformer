using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIColorScript : MonoBehaviour
{
    [SerializeField] GameObject myPlayer;

    private int currentColor = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //redundant from goob script, not sure how to reference it rn
        if (Gamepad.current.rightShoulder.wasPressedThisFrame)
        {
            if (currentColor == 2)
            {
                currentColor = 0;
            }
            else
            {
                currentColor++;
            }
        }
        if (Gamepad.current.leftShoulder.wasPressedThisFrame)
        {
            if (currentColor == 0)
            {
                currentColor = 2;
            }
            else
            {
                currentColor--;
            }
        }
        if(currentColor == 0) this.GetComponent<Image>().color = Color.red;
        if(currentColor == 1) this.GetComponent<Image>().color = Color.yellow;
        if(currentColor == 2) this.GetComponent<Image>().color = Color.blue;
    }
}
