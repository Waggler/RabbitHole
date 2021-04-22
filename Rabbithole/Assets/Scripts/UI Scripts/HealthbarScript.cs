using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarScript: MonoBehaviour
{
    public Image healthFill;
    public Image healthBase;
    public Image invulnerable;
    public void Update()
    {
        if (GameManager.Instance.invuln == false)
        {
            invulnerable.enabled = false;
            healthFill.enabled = true;
            healthBase.enabled = true;
            healthFill.fillAmount = GameManager.Instance.maxHealth;
            healthFill.fillAmount = GameManager.Instance.health / GameManager.Instance.maxHealth;
        }

        if (GameManager.Instance.invuln == true)
        {
            invulnerable.enabled = true;
            healthFill.enabled = false;
            healthBase.enabled = false;
        }
    }
}
 