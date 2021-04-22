using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUIScript : MonoBehaviour
{
    public Image ammoUI;
    public Image bottomlessClip;
    public Animator ammoAnimator;

    // Update is called once per frame
    private void Start()
    {
        ammoAnimator.SetInteger("ammoAmount", GameManager.Instance.maxAmmo);
    }
    void Update()
    {
        if (GameManager.Instance.bottomlessClip == false)
        {
            ammoUI.enabled = true;
            bottomlessClip.enabled = false;
            if (GameManager.Instance.isReloading == true)
            {
                ammoAnimator.SetBool("isReloading", true);
            }
            else
            {
                ammoAnimator.SetBool("isReloading", false);
            }
            ammoAnimator.SetInteger("ammoAmount", GameManager.Instance.ammo);
        }

        if(GameManager.Instance.bottomlessClip == true)
        {
            ammoUI.enabled = false;
            bottomlessClip.enabled = true;
        }
    }
}
