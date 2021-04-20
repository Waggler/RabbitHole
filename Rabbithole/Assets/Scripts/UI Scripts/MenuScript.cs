using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuScript : MonoBehaviour
{

///Gives Controller Support to menus
    public GameObject mainmenuButton;
    public GameObject optionsButton;
    public GameObject HTPButton;
    public GameObject controllerButton;
    public GameObject keyboardButton;
    public GameObject controllerMenu;
    public GameObject keyboardMenu;
    public GameObject htpMenu;
    public GameObject optionsMenu;
    public AudioClip button;
    public void PlayGame()
    {
        SceneManager.LoadScene("Hub");
        Debug.Log("Play!");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
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
        EventSystem.current.SetSelectedGameObject(mainmenuButton);
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
        EventSystem.current.SetSelectedGameObject(mainmenuButton);
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
}
