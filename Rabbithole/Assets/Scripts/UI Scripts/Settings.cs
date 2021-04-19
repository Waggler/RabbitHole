using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class Settings : MonoBehaviour
{
    public AudioMixer audioMixer;
   
    public bool bClip;
    public bool invincible;
    public bool gameisPaused = false;
    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume",volume);
    }

    // Bottomless Clip bool
    public void BClip()
    {
        if (bClip == false)
        {
            bClip = true;
        } else if(bClip == true)
        {
            bClip = false;
        }
    }

    // Invincibility Bool

    public void Invincibility()
    {
        if (invincible == false)
        {
            invincible = true;
        }
        else if (invincible == true)
        {
            invincible = false;
        }
    }

}
