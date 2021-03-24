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

    public AudioSource audioSource;
    public AudioClip gunShot;
    public AudioClip reloadSound;


    public Camera fpsCam;

    [SerializeField]
    //private Transform firePoint;

    private void Start()
    {
        Init();
    }


    public void Init()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float controllerShooting = Input.GetAxis("RT"); // Jordan Added this. This makes the Controller Trigger useable
        if (controllerShooting >= 1 && Time.time >= nextTimeToFire && ammoCount <= clipSize && reloading == false|| Input.GetButton("Fire1") && Time.time >= nextTimeToFire && ammoCount <= clipSize && reloading == false) // Jordan changed this to use controller shooting and fire1 mouse shooting
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            ammoCount += 1;

            audioSource.PlayOneShot(gunShot, 0.7f);

        }

        if (Input.GetButtonDown("Reload") && reloading == false) // this used to be Input.GetKeyDown(KeyCode.R)  now it works with both controller and keyboard
        {
            audioSource.PlayOneShot(reloadSound, 0.7f); // Jordan Added this
            StartCoroutine(routine: Reload());
        }

    }
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

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
        }
    }

    IEnumerator Reload()
    {
        reloading = true;
        yield return new WaitForSeconds(reloadTime);
        ammoCount = 0;
        reloading = false;
    }
}
