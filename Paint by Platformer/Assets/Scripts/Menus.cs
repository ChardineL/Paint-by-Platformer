using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;

public class Menus : MonoBehaviour
{
    public GameObject panel;
    public GameObject pauseMenu;
    public GameObject firstPauseButton;
    public static bool isPaused;

    // public PlayerInput playerInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Gamepad.current.startButton.wasPressedThisFrame)
        {

            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }

        }
        /*if(panel.activeInHierarchy && isPaused){
            PressContinue();
        }*/
    }
    public void PressContinue()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        // playerInput.SwitchCurrentActionMap("Player");
    }

    public void PauseGame()
    {
        if (pauseMenu == null) Debug.LogError("PauseMenu is not assigned!");
        Debug.Log(pauseMenu.activeInHierarchy);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; //stops in game clock
        isPaused = true;

        // playerInput.SwitchCurrentActionMap("UI");
        // Debug.Log("Switched to UI action map");
        // playerInput.enabled = false;

        EventSystem.current.SetSelectedGameObject(null); // Clear previous selection
        EventSystem.current.SetSelectedGameObject(firstPauseButton);
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        // playerInput.enabled = true;
        // playerInput.SwitchCurrentActionMap("Player");
        isPaused = false;

        
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        isPaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
