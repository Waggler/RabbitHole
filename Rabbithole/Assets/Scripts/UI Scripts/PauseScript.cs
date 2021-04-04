using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;




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

    // Update is called once per frame
    void Update()
    {
        // Press P to pause

        if (Input.GetKeyDown(KeyCode.P))
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
    }

    // Go to the main menu
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

}
