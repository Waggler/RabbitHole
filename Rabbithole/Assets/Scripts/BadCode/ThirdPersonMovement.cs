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
    public float speed = 6f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float dashDistance = 3f;
    public float dashCooldown = 2f;
    public bool isGrounded;
    public bool isGliding;
    public bool isDashing;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    public Vector3 Drag;

    private float waitTime = 0.5f;
    public float timer = 0.0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

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

        if (!isGrounded)
        {
            timer += Time.deltaTime;
        }

        // Handles Gliding Logic
        if (Input.GetButtonDown("Jump") && !isGrounded && timer > waitTime)
        {
            if (!isGliding)
            {
                gravity = -10;
                isGliding = true;
            }
            else if (isGliding == true)
            {
                gravity = -30;
                isGliding = false;
            }
        }

        // Handles Resetting Glide
        if (isGrounded == true)
        {
            gravity = -30;
            isGliding = false;
            timer = 0;
        }

        // Handles Dashing Logic
        if (Input.GetKeyDown(KeyCode.LeftShift) && isDashing == false && isGrounded)
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
}