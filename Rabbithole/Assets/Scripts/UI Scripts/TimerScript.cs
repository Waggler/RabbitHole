using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{

    float currentTime = 0f;
    float startingTime = 80f;

    [SerializeField] TextMeshProUGUI timerText;

    // Sets the starting time
    private void Start()
    {
        currentTime = startingTime;
    }

    // Counts down the time
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        timerText.text = currentTime.ToString("0");

        if (currentTime <= 0)
        {
            currentTime = 0;
        }
    }

    /* My original script had seperate timers to proc when you won or lost
      I'd do something similar here */

}
