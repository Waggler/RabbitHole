﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSetter : MonoBehaviour
{
    public string targetScene;
    public bool isGoal1;
    public bool isGoal3;

    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (isGoal1 == true)
            {
                    GameManager.Instance.level1Complete = true;
            }

            if (isGoal3 == true)
            {
                    GameManager.Instance.level3Complete = true;
            }

            if (GameManager.Instance.allLevelsComplete == true)
            {
                GameManager.Instance.targetScene = "Win Screen";
            }
            else
            {
                GameManager.Instance.targetScene = targetScene;
            }
            GameManager.Instance.ChangeScene();
        }
    }
}