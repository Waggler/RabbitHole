using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarScript : MonoBehaviour
{
    public Image healthFill;

    /*
    public void Start()
    {
        healthFill.fillAmount = GameManager.Instance.maxHealth;
    }
    */

    public void Update()
    {
        healthFill.fillAmount = GameManager.Instance.maxHealth;
        healthFill.fillAmount = GameManager.Instance.health / GameManager.Instance.maxHealth;
    }
}
 