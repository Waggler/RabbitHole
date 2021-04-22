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
        GameManager.Instance.health = GameManager.Instance.maxHealth;
        if (GameManager.Instance.allLevelsComplete == true)
        {
            GameManager.Instance.level1Complete = false;
            GameManager.Instance.level2Complete = false;
            GameManager.Instance.level3Complete = false;
            GameManager.Instance.allLevelsComplete = false;
        }
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }
    public void Retry()
    {
        GameManager.Instance.health = GameManager.Instance.maxHealth;
        SceneManager.LoadScene(GameManager.Instance.currentScene.ToString());
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game!");

        Application.Quit();
    }

}
