using UnityEngine;
using UnityEngine.InputSystem;

public class ProjectileSpeechBubble : MonoBehaviour
{
     public GameObject[] speechBubbles;
    private int currentIndex = 0;
    private bool playerInside = false;

    void Start()
    {
        // Hide all bubbles at the start
        foreach (GameObject bubble in speechBubbles)
        {
            bubble.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInside && Gamepad.current != null && Gamepad.current.bButton.wasPressedThisFrame)
        {
            ShowNextBubble();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            currentIndex = 0;
            if (speechBubbles.Length > 0)
            {
                speechBubbles[currentIndex].SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            foreach (GameObject bubble in speechBubbles)
            {
                bubble.SetActive(false);
            }
        }
    }

    void ShowNextBubble()
    {
        if (speechBubbles.Length == 0) return;

        // Hide current
        speechBubbles[currentIndex].SetActive(false);

        // Advance index
        currentIndex = (currentIndex + 1) % speechBubbles.Length;

        // Show next
        speechBubbles[currentIndex].SetActive(true);
    }
}
