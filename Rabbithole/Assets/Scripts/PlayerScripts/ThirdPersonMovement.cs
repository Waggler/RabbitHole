using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [Header("Camera Settings")]
    public CharacterController controller;
    public Transform cam;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    [Header("Movement Settings")]
    private float speed = 6f;
    public float groundSpeed = 6f;
    public float glideSpeed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float bounceHeight = 2000000f;
    public bool isGrounded;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public Vector3 velocity;

    [Header("Dash Settings")]
    public bool isDashing;
    public float dashDistance = 3f;
    public float dashCooldown = 2f;
    public Vector3 Drag;

    [Header("Glide Settings")]
    public bool isGliding;
    public float waitTime = 0.35f;
    public float glideTimer = 0.0f;


    // Locks Cursor
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }// END Start


    void Update()
    {
        // Gets input from player movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Makes sures Player can move in multiple directions
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // Checks if player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Handles Movement Logic
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        // Handles Jumping Logic
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Resets Glide Timer
        if (!isGrounded)
        {
            glideTimer += Time.deltaTime;
        }

        // Handles Gliding Logic
        if (Input.GetButtonDown("Jump") && !isGrounded && glideTimer > waitTime)
        {
            if (!isGliding)
            {
                speed = glideSpeed;
                gravity = -5;
                isGliding = true;
            }
            else if (isGliding == true)
            {
                speed = groundSpeed;
                gravity = -30;
                isGliding = false;
            }
        }

        // Handles Resetting Glide
        if (isGrounded == true)
        {
            speed = groundSpeed;
            gravity = -30;
            isGliding = false;
            glideTimer = 0;
        }

        // Handles Dashing Logic
        if (Input.GetButtonDown("DashButton") && isDashing == false && isGrounded) // Jordan changed the input to "DashButton" now works with controller and Keyboard.
        {
            Debug.Log("Dash");
            StartCoroutine(Dash());
        }

        // Handles Gravity and Dash Physics
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        velocity.x /= 1 + Drag.x * Time.deltaTime;
        velocity.y /= 1 + Drag.y * Time.deltaTime;
        velocity.z /= 1 + Drag.z * Time.deltaTime;
    }// END Update

    private IEnumerator Dash()
    {
        velocity += Vector3.Scale(transform.forward, dashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * Drag.x + 1)) / -Time.deltaTime), 0, (Mathf.Log(1f / (Time.deltaTime * Drag.z + 1)) / -Time.deltaTime)));
        isDashing = true;
        yield return new WaitForSeconds(dashCooldown);
        isDashing = false;
    }// END IEnumerator Dash

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Mole"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            Debug.Log("Bounce");
        }

    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mole"))
        {
            Debug.Log("Bounce");
            velocity.y = Mathf.Sqrt(bounceHeight * -10f * gravity);
            //PlayerBall.AddForce(Vector3.up * BouncingForce);
        }
    }
}