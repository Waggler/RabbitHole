using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Target : MonoBehaviour
{
    // Health of Target
    public float health = 50f;

    public void TakeDamage(float amount)
    {
        health -= amount;
        // If Health is Less than Zero kill the target
        if (health <= 0f)
        {
            Die();
        }
    }// END TakeDamage

    void Die()
    {
        gameObject.SetActive(false);
    }// END DIE

}
