using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public CharacterController controller;

    [Header("Player Stats")]
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float glideBuffer = 0.5f;
    public float glideCount;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;

    bool isGrounded;

    //[Header("Player Audio")]

    //public AudioSource voiceSource;

    //public AudioClip jump;
    //public AudioClip jumpVerb;




    // Update is called once per frame
    void Update()
    {
        // Creates a sphere on groundcheck to handle gravity & velocity

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Gets Player key input

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Handles player moving around
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Glide
        if (Input.GetButtonDown("Jump") && !isGrounded)
        {
            gravity = -10;
        }
        else
        {
            gravity = -15f;
        }

        // Handles Gravity

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
    IEnumerator GlideTimer ()
    {
        yield return new WaitForSeconds(2);
    }
}
