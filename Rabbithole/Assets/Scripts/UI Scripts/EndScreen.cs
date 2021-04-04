using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{

    public void MainMenu()
    {
        Debug.Log("Back to the Menu");
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game!");

        Application.Quit();
    }

}
