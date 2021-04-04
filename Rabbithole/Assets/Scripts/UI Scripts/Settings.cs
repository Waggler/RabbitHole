using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class Settings : MonoBehaviour
{
    public AudioMixer audioMixer;
    public GameObject pauseMenuUi;
    public GameObject otherCanvas;
    public bool gameisPaused = false;
    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume",volume);
    }
    void Update()
    {
         // Pause Functionality
        if (Input.GetButtonDown("Pause"))
        {
            if (gameisPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    void Pause()
    {
        pauseMenuUi.SetActive(true);
        otherCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        gameisPaused = true;
    }
    public void Resume()
    {
        pauseMenuUi.SetActive(false);
        otherCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        gameisPaused = false;
    }
}
