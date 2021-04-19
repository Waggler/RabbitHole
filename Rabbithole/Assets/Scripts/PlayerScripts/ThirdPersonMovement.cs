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
    public float bounceHeightOne = 2000000f;
    public float bounceHeightTwo = 2000000f;
    public float bounceHeightThird = 2000000f;
    public float bounceHeightFour = 2000000f;
    public float walkingfallRate = -20f;
    public bool isGrounded;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public Vector3 velocity;
    public float turnSpeed = 15;

    [Header("Dash Settings")]
    public bool isDashing;
    public float dashDistance = 3f;
    public float dashCooldown = 2f;
    public Vector3 Drag;

    [Header("Glide Settings")]
    public bool isGliding;
    public float waitTime = 0.35f;
    public float bounceWaitTime = 5f;
    public float glideTimer = 0.0f;
    public bool bounced;

    [Header("Health Settings")]
    public float hitPoints;
    public float damageTaken;

    [Header("Animator Settings")]
    Animator animator;
    public Animator moleAnimator;
    public Animator chadAnimator;

    Camera mainCamera;

    [Header("Karat UI")]

    public GameObject karatOneUI;
    public GameObject karatTwoUI;
    public GameObject karatThreeUI;

    public int karatPieces;
    

    [Header("Chad Settings")]
    public bool isChad;
    public GameObject nonChadGFX;
    public GameObject Chad;
    public float chadJumpHeight;

    [Header("Particle Effects")]
    public GameObject starsParticle;
    public GameObject timerParticle;

    // Locks Cursor
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        mainCamera = Camera.main;
    }// END Start


    void Update()
    {
        animator = GetComponent<Animator>();
        float yawCamera = mainCamera.transform.rotation.eulerAngles.y;
        // Gets input from player movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Makes sures Player can move in multiple directions
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // Checks if player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.fixedDeltaTime);
        // Handles Movement Logic
        if (direction.magnitude >= 0.1f && PauseMenuV2.gamePaused == false)
        {
            animator.SetBool("isMoving", true);
            chadAnimator.SetBool("isMoving", true);
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            //transform.rotation = Quaternion.Euler(0f, angle, 0f);
            

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isMoving", false);
            chadAnimator.SetBool("isMoving", false);
        }

        // Handles Jumping Logic
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetTrigger("Jump");
            chadAnimator.SetTrigger("Jumpin");
            animator.SetBool("isGrounded", false);
        }

        if (Input.GetButtonDown("Jump"))
        {
            animator.SetBool("isGrounded", false);
            chadAnimator.SetBool("isGrounded", false);
        }
        

        // Resets Glide Timer
        if (!isGrounded)
        {
            glideTimer += Time.deltaTime;
            animator.SetBool("isFalling", true);
            chadAnimator.SetBool("isFalling", true);
        }

        // Handles Gliding Logic
        if (Input.GetButtonDown("Jump") && !isGrounded && glideTimer > waitTime && bounced == false)
        {
            if (!isGliding && !isChad)
            {
                speed = glideSpeed;
                gravity = -5;
                isGliding = true;
                animator.SetBool("isGliding", true);
                animator.SetBool("isFalling", false);
            }
            else if (isGliding == true && !isChad)
            {
                speed = groundSpeed;
                gravity = -30;
                isGliding = false;
                animator.SetBool("isGliding", false);
                animator.SetBool("isFalling", true);
            }
        }

        // Handles Resetting Glide
        if (isGrounded == true)
        {
            speed = groundSpeed;
            gravity = -30;
            isGliding = false;
            glideTimer = 0;
            bounced = false;
            animator.SetBool("isGrounded", true);
            chadAnimator.SetBool("isGrounded", true);
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
            animator.SetBool("isGliding", false);
            chadAnimator.SetBool("isFalling", false);
        }

        if (Input.GetButtonDown("Jump") && !isGrounded && glideTimer > bounceWaitTime && bounced == true)
        {
            if (!isGliding)
            {
                speed = glideSpeed;
                gravity = -5;
                isGliding = true;
                animator.SetBool("isGliding", true);
                animator.SetBool("isFalling", false);
            }
            else if (isGliding == true)
            {
                speed = groundSpeed;
                gravity = -30;
                isGliding = false;
                animator.SetBool("isGliding", false);
                animator.SetBool("isFalling", true);
            }
        }

        // Handles Dashing Logic
        if (Input.GetButtonDown("DashButton") && isDashing == false && isGrounded) // Jordan changed the input to "DashButton" now works with controller and Keyboard.
        {
            Debug.Log("Dash");
            StartCoroutine(Dash());
        }

        // Handles Gravity and Dash Physics
        if (velocity.y > walkingfallRate)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        controller.Move(velocity * Time.deltaTime);
        velocity.x /= 1 + Drag.x * Time.deltaTime;
        velocity.y /= 1 + Drag.y * Time.deltaTime;
        velocity.z /= 1 + Drag.z * Time.deltaTime;

        if (hitPoints <= 0)
        {
            gameObject.SetActive(false);
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            
        }
        else
        {
            
        }

        if (isChad == true)
        {
            nonChadGFX.SetActive(false);
            Chad.SetActive(true);
            jumpHeight = chadJumpHeight;

        }

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
        if (other.CompareTag("Mole1"))
        {
            Debug.Log("Bounce");
            moleAnimator.SetBool("IsJumpedOn", true);
            bounced = true;
            speed = groundSpeed;
            gravity = -30;
            isGliding = false;
            velocity.y = Mathf.Sqrt(bounceHeightOne * -10f * gravity);
            StartCoroutine("particleStars");
            StartCoroutine(MoleReset());
            //PlayerBall.AddForce(Vector3.up * BouncingForce);
            GameObject gObject = Instantiate(starsParticle, transform.position, Quaternion.identity) as GameObject;
            Destroy(gObject, 3);
        }

        if (other.CompareTag("Mole2"))
        {
            Debug.Log("Bounce2");
            moleAnimator.SetBool("IsJumpedOn", true);
            bounced = true;
            speed = groundSpeed;
            gravity = -30;
            isGliding = false;
            StartCoroutine("particleStars");
            StartCoroutine(MoleReset());
            velocity.y = Mathf.Sqrt(bounceHeightTwo * -10f * gravity);

            //PlayerBall.AddForce(Vector3.up * BouncingForce);
        }

        if (other.CompareTag("Mole3"))
        {
            Debug.Log("Bounce3");
            moleAnimator.SetBool("IsJumpedOn", true);
            bounced = true;
            speed = groundSpeed;
            gravity = -30;
            isGliding = false;
            StartCoroutine("particleStars");
            StartCoroutine(MoleReset());
            velocity.y = Mathf.Sqrt(bounceHeightThird * -10f * gravity);

            //PlayerBall.AddForce(Vector3.up * BouncingForce);
        }
        if (other.CompareTag("Mole4"))
        {
            Debug.Log("Bounce4");
            moleAnimator.SetBool("IsJumpedOn", true);
            bounced = true;
            speed = groundSpeed;
            gravity = -30;
            isGliding = false;
            StartCoroutine("particleStars");
            StartCoroutine(MoleReset());
            velocity.y = Mathf.Sqrt(bounceHeightFour * -10f * gravity);

            //PlayerBall.AddForce(Vector3.up * BouncingForce);
        }

        if (other.CompareTag("Bullet") && !isDashing)
        {
            hitPoints -= damageTaken;
        }

        IEnumerator MoleReset()
        {
            yield return new WaitForSeconds(3);
            moleAnimator.SetBool("IsJumpedOn", false);
        }
        if (other.CompareTag("GoldKarat"))
        {
            Destroy(other.gameObject);
            karatPieces += 1;

            if (karatPieces >= 3)
            {
                karatThreeUI.gameObject.SetActive(true);
                isChad = true;
            }
            else if (karatPieces == 2)
            {
                karatTwoUI.gameObject.SetActive(true);
            }
            else if (karatPieces == 1)
            {
                karatOneUI.gameObject.SetActive(true);
            }
        }

        // Health Pickup
        if (other.gameObject.CompareTag("HCarrot"))
        {
            if (GameManager.Instance.health < 8)
            {
                GameManager.Instance.health += 1;
                Destroy(other.gameObject);
            }
            else

            return;
        }
        
        //Timer Pickup
        if (other.gameObject.CompareTag("TimePickup"))
        {
            GameManager.Instance.timeReduced += 5f;
            StartCoroutine("particleTimer");
            //GameObject gObject = Instantiate(timerParticle, transform.position, Quaternion.identity);
            //Destroy(gObject, 3);
            Destroy(other.gameObject);
        }
            else

            return;
    }

    IEnumerator particleTimer()
    {
        timerParticle.SetActive(true);
        yield return new WaitForSeconds(2);
        timerParticle.SetActive(false);
    }

    IEnumerator particleStars()
    {
        starsParticle.SetActive(true);
        yield return new WaitForSeconds(2);
        starsParticle.SetActive(false);
    }

}