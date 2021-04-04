using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSetter : MonoBehaviour
{

    public string targetScene;
    public bool stageGoal = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.targetScene = targetScene;
            if(stageGoal == true)
            {
                GameManager.Instance.level1Complete = true;
            }
            GameManager.Instance.ChangeScene();
        }
        else
            return;
    }
}
