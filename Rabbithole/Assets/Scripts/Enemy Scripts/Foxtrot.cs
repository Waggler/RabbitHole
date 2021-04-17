using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Foxtrot : MonoBehaviour
{
    // Health of Target
    public float bossHealth = 50f;

    public void TakeDamage(float amount)
    {
        bossHealth -= amount;
        // If Health is Less than Zero kill the target
        if (bossHealth <= 0f)
        {
            BossDie();
        }
    }// END TakeDamage

    void BossDie()
    {
        gameObject.SetActive(false);
    }// END DIE

}
