using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class Settings : MonoBehaviour
{
    public AudioMixer audioMixer;

    public bool gameisPaused = false;

    public Toggle bottomlessCheck;
    public Toggle invulnerableCheck;
    public Slider volumeSlider;

    public void Awake()
    {
        bottomlessCheck.isOn = GameManager.Instance.bottomlessClip;
        invulnerableCheck.isOn = GameManager.Instance.invuln;
        audioMixer.SetFloat("volume", GameManager.Instance.volume);
        volumeSlider.value = GameManager.Instance.volume;
    }

    public void Update()
    {
        audioMixer.SetFloat("volume", GameManager.Instance.volume);
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        GameManager.Instance.volume = volume;
    }

    // Bottomless Clip bool
    public void BClip(bool bottomlessClip)
    {
        GameManager.Instance.bottomlessClip = bottomlessClip;
    }

    // Invincibility Bool
    public void Invincibility(bool invincible)
    {
        GameManager.Instance.invuln = invincible;
    }

}
