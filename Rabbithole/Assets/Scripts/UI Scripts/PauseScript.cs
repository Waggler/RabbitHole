using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.EventSystems;




public class PauseScript : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource aSource;
    public AudioClip button;
    public AudioMixer audioMixer;


    [Header("Bool")]
    public static bool gamePaused = false;

    [Header("GameObjects")]
    public GameObject pauseMenu;

    ///Gives Controller Support to menus
    public GameObject pauseButton;
    public GameObject optionsButton;
    public GameObject closedButton;

    // Update is called once per frame
    void Update()
    {
        // Press P to pause

        if (Input.GetButtonDown("Pause"))
        {
            if (gamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // Play da game
    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }



    // Pause da game
    void Pause()
    {
        Cursor.lockState = CursorLockMode.Confined;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;

        //clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set new selected object
        EventSystem.current.SetSelectedGameObject(pauseButton);
    }

    // Go to the main menu
    public void MainMenu()
    {
        Time.timeScale = 1f;
        gamePaused = false;
        SceneManager.LoadScene("Main Menu");
    }

}
