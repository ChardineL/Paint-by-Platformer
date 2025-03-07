using UnityEngine;

public class NPCSpeechBubble : MonoBehaviour
{
    public GameObject speechBubble; // Assign in Inspector

    void Start()
    {
        speechBubble.SetActive(false); // Hide speech bubble initially
    }

    void OnTriggerEnter2D(Collider2D other) // Use OnTriggerEnter for 3D
    {
        if (other.CompareTag("Player")) // Make sure your player has the tag "Player"
        {
            speechBubble.SetActive(true); // Show speech bubble
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            speechBubble.SetActive(false); // Hide speech bubble when leaving
        }
    }
}
