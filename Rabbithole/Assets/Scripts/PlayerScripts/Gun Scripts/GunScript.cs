using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    [Header("Gun Stats")]
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 30f;
    public float clipSize;
    public float ammoCount;
    public float reloadTime;
    private float fireRate = 1f;
    private float nextTimeToFire = 1f;
    public bool reloading;
    public Camera fpsCam;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip gunShot;
    public AudioClip reloadSound;


    /*
    private void Start()
    {
        Init();
    }// END Start


    public void Init()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }// END Init
    */ // Lock Cursor Script Not Used

    void Update()
    {
        //This makes the Controller Trigger useable
        float controllerShooting = Input.GetAxis("RT");

        // changed to use controller shooting and fire1 mouse shooting
        if (controllerShooting >= 1 && Time.time >= nextTimeToFire && GameManager.Instance.ammo > 0 && GameManager.Instance.isReloading == false|| Input.GetButton("Fire1") && Time.time >= nextTimeToFire && GameManager.Instance.ammo > 0 && GameManager.Instance.isReloading == false)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            GameManager.Instance.ammo -= 1;
            GameManager.Instance.ammoUI.SetInteger("ammoAmount", GameManager.Instance.ammo);
            audioSource.PlayOneShot(gunShot, 0.7f);

        }

        // this used to be Input.GetKeyDown(KeyCode.R)  now it works with both controller and keyboard
        if (Input.GetButtonDown("Reload") && GameManager.Instance.isReloading == false && ammoCount < 9)
        {
            audioSource.PlayOneShot(reloadSound, 0.7f);
            StartCoroutine(routine: Reload());
        }
    }// END Update

    void Shoot()
    {
        Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * 100, Color.red, 2f);
        RaycastHit hit;

        //if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, range))
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            // This allows the gun to push back rigidbodies when hit
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
        }
    }// END Shoot

    IEnumerator Reload()
    {
        GameManager.Instance.isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        GameManager.Instance.ammo = GameManager.Instance.maxAmmo;
        GameManager.Instance.ammoUI.SetInteger("ammoAmount", GameManager.Instance.ammo);
        GameManager.Instance.isReloading = false;
    }// END Reload
}
