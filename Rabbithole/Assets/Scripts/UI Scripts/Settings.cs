using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class Settings : MonoBehaviour
{
    public AudioMixer audioMixer;

    public bool bClip;
    public GameObject bClipUI;
    public GameObject invincibleUI;
    public bool invincible;
    public bool gameisPaused = false;
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    // Bottomless Clip bool
    public void BClip()
    {
        if (bClip == false)
        {
            bClip = true;
            bClipUI.SetActive(true);
        }
        else if (bClip == true)
        {
            bClip = false;
            bClipUI.SetActive(false);
        }
    }

    // Invincibility Bool

    public void Invincibility()
    {
        if (invincible == false)
        {
            invincible = true;
            invincibleUI.SetActive(true);
        }
        else if (invincible == true)
        {
            invincible = false;
            invincibleUI.SetActive(false);
        }
    }

}
