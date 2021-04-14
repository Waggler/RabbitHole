using System;
using UnityEngine;


public class LookAtTarget : MonoBehaviour
{
    public Transform target;
    public Camera targetCamera;

    void Update()
    {
        transform.LookAt(transform.position + targetCamera.transform.rotation * Vector3.left,
        targetCamera.transform.rotation * Vector3.up);
    }
}
