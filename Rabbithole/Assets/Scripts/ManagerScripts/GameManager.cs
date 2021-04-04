using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Player Settings")]
    public GameObject player;
    private int maxHealth = 8;
    public int health;

    [Header("Level Completion")]
    public bool tutComplete = false;
    public bool level1Complete = false;
    public bool level2Complete = false;
    public bool level3Complete = false;

    [Header("Scene Transitions")]
    public string currentScene;
    public string targetScene;

    [Header("Pause Menu")]
    public bool isPaused;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        health = maxHealth;
        SceneID();
    }

    public void ChangeScene()
    {
        SceneManager.LoadSceneAsync(targetScene);
        SceneID();
    }

    public void SceneID()
    {
        currentScene = SceneManager.GetActiveScene().ToString();
    }
}
