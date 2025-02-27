using Unity.VisualScripting;
using UnityEngine;

public class RedSquareScript : MonoBehaviour
{
    private int currentColor;
    private PlayerMovement playerMovement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject playerObject = GameObject.Find("Goob_0");
        playerMovement = playerObject.GetComponent<PlayerMovement>();
       
    }

    // Update is called once per frame
    void Update()
    {
        currentColor = playerMovement.currentColor;
        if(currentColor != 0)
        {
            this.GetComponent<BoxCollider2D>().enabled = false;
            this.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 0.5f);
        }
        else
        {
            this.GetComponent <BoxCollider2D>().enabled = true;
            this.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);

        }
        
        
    }
}
