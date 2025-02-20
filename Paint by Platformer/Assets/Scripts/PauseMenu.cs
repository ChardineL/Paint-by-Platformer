<<<<<<< Updated upstream
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
=======
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;
>>>>>>> Stashed changes

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< Updated upstream
        if(Input.GetKeyDown(KeyCode.Escape) || Gamepad.current.startButton.wasPressedThisFrame){
=======
        Debug.Log("Start button is being pressed!");
        if(Input.GetKeyDown(KeyCode.Escape) || Gamepad.current.startButton.wasPressedThisFrame){
             
>>>>>>> Stashed changes
            if (isPaused){
                ResumeGame();
            }
            else{
                PauseGame();
            }
<<<<<<< Updated upstream
        }
    }
    public void PauseGame(){
=======
            
        }
    }
    public void PauseGame(){
          Debug.Log("Start button is being pressed!");
>>>>>>> Stashed changes
        pauseMenu.SetActive(true);
        Time.timeScale=0f; //stops in game clock
        isPaused=true;
    }
    public void ResumeGame(){
<<<<<<< Updated upstream
=======
         
>>>>>>> Stashed changes
        pauseMenu.SetActive(false);
        Time.timeScale=1f;
        isPaused=false;
    }
<<<<<<< Updated upstream
=======
    
>>>>>>> Stashed changes
    public void GoToMainMenu(){
        Time.timeScale=1f;
        SceneManager.LoadScene("MainMenu");
        isPaused=false;
    }

    public void QuitGame(){
        Application.Quit();
    }
}
