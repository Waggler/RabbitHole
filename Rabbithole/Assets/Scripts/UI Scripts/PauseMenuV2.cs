using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.EventSystems;




public class PauseMenuV2 : MonoBehaviour
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
    public GameObject HTPButton;
    public GameObject controllerButton;
    public GameObject keyboardButton;
    public GameObject controllerMenu;
    public GameObject keyboardMenu;
    public GameObject htpMenu;
    public GameObject optionsMenu;


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
        optionsMenu.SetActive(false);
        htpMenu.SetActive(false);
        keyboardMenu.SetActive(false);
        controllerMenu.SetActive(false);
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
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }
    public void OpenOptions()
    {
        optionsMenu.SetActive(true);
        //clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set new selected object
        EventSystem.current.SetSelectedGameObject(optionsButton);
    }
    public void CloseOptions()
    {
        //clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set new selected object
        EventSystem.current.SetSelectedGameObject(pauseButton);
        optionsMenu.SetActive(false);
    }
    public void OpenHTP()
    {
        htpMenu.SetActive(true);
        //clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set new selected object
        EventSystem.current.SetSelectedGameObject(HTPButton);
    }
    public void CloseHTP()
    {
        //clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set new selected object
        EventSystem.current.SetSelectedGameObject(pauseButton);
        htpMenu.SetActive(false);
    }
    public void OpenKeyboardScreen()
    {
        keyboardMenu.SetActive(true);
        //clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set new selected object
        EventSystem.current.SetSelectedGameObject(keyboardButton);
    }
    public void CloseKeyboardScreen()
    {
        //clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set new selected object
        EventSystem.current.SetSelectedGameObject(HTPButton);
        keyboardMenu.SetActive(false);
    }
    public void OpenControllerScreen()
    {
        controllerMenu.SetActive(true);
        //clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set new selected object
        EventSystem.current.SetSelectedGameObject(controllerButton);
    }
    public void CloseControllerScreen()
    {
        //clear selected object
        EventSystem.current.SetSelectedGameObject(null);
        //set new selected object
        EventSystem.current.SetSelectedGameObject(HTPButton);
        controllerMenu.SetActive(false);
    }

    // Go to the main menu
    public void MainMenu()
    {
        Time.timeScale = 1f;
        gamePaused = false;
        SceneManager.LoadScene("Main Menu");
    }
    public void Hub()
    {
        Time.timeScale = 1f;
        gamePaused = false;
        SceneManager.LoadScene("Hub");
    }
}