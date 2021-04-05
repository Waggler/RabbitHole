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
    public float health;

    [Header("Player Ammo and Shooting")]
    public Animator ammoUI;
    [HideInInspector]public int maxAmmo = 9;
    public int ammo;
    public bool isReloading;

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
        ammoUI.SetInteger("ammoAmount", maxAmmo);
        health = maxHealth;
        ammo = maxAmmo;
        SceneID();
    }

    private void Update()
    {
        if (isReloading == true)
        {
            ammoUI.SetBool("isReloading", true);
        }
        else
            ammoUI.SetBool("isReloading", false);

        if (health <= 0)
        {
            targetScene = "Lose Screen";
            ChangeScene();
        }
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
