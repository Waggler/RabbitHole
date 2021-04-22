using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Player Settings")]
    public GameObject player;
    [HideInInspector]public float maxHealth = 8;
    public float health = 8;

    [Header("Player Ammo and Shooting")]
    [HideInInspector]public int maxAmmo = 9;
    public int ammo;
    public bool isReloading;

    [Header("Tutorial Checks")]
    public bool tut1Check;
    public bool tut2Check;
    public bool tut3Check;

    [Header("Level Completion")]
    public bool tutComplete = false;
    public bool level1Complete = false;
    public bool level2Complete = false;
    public bool level3Complete = false;
    public bool allLevelsComplete = false;

    [Header("Scene Transitions")]
    public string currentScene;
    public string targetScene;

    [Header("Bandaids")]
    public int enemiesKilled = 0;
    public float timeReduced;

    [Header("Settings and Cheats")]
    public float volume;
    public bool invuln;
    public bool bottomlessClip;

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
        ammo = maxAmmo;
    }

    private void Update()
    {
        if (health <= 0)
        {
            SceneManager.LoadScene("Lose Screen");
            health = maxHealth;
        }
        
        if(tut1Check == true && tut2Check == true && tut3Check == true)
        {
            tutComplete = true;
        }

        if (level1Complete && level2Complete && level3Complete)
        {
            allLevelsComplete = true;
            SceneManager.LoadScene("Win Screen");
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(targetScene);
        ammo = maxAmmo;
        health = maxHealth;
        currentScene = targetScene;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(targetScene);
    }
}
