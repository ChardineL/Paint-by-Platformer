using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
public class getShoot : MonoBehaviour
{
    public GameObject popupPanel;
    public GameObject selectedButton;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameObject player = collision.gameObject;
            PlayerMovement playerScript = player.GetComponent<PlayerMovement>();
            if (playerScript)
            {
                playerScript.canDash = true;
                PlayerPrefs.SetInt("ShootUnlocked", 1);
                Destroy(gameObject);
            }

        }
    }
    private void OnDestroy()
    {
        if (popupPanel != null)
        {
            popupPanel.SetActive(true); // Show the popup
            Time.timeScale = 0f; // Pause the game
            Menus.isPaused = true; // Keep everything consistent

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                PlayerInput playerInput = player.GetComponent<PlayerInput>();
                if (playerInput != null)
                {
                    playerInput.SwitchCurrentActionMap("UI");
                }

                EventSystem.current.SetSelectedGameObject(null); // Clear previous selection
                EventSystem.current.SetSelectedGameObject(selectedButton);
            }
        }


    }
}
