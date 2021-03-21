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
    private float fireRate = 15f;
    private float nextTimeToFire = 1f;

    public bool reloading;

    public AudioSource audioSource;
    public AudioClip gunShot;


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
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && ammoCount <= clipSize && reloading == false)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            ammoCount += 1;

            audioSource.PlayOneShot(gunShot, 0.7f);

        }

        if (Input.GetKeyDown(KeyCode.R) && reloading == false)
        {
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
