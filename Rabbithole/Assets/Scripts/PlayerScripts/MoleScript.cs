using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleScript : MonoBehaviour
{
    public float BouncingForce = 2f;
    public GameObject Player;
    //public Rigidbody PlayerBall;

    private void Start()
    {
        //PlayerBall = gameObject.GetComponent<Rigidbody>();
        //Gunny = Player.GetComponent<ThirdPersonMovement>();
        //Vector3 jumpVelocity = ThirdPersonMovement
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Bounce");
            //PlayerBall.AddForce(Vector3.up * BouncingForce);
        }
    }
}
