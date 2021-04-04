using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarScript : MonoBehaviour
{
    // Moves around our slider
    public Slider slider;

    // Sets Gunny's Max Health so we don't need to do it via inspector
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }


    // Gets assigned by Gunny's Health
    public void Health(int health)
    {
        slider.value = health;
    }


}
 