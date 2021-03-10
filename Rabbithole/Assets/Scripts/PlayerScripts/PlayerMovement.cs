using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
	private CharacterController characterController;
	private Animator animator;

	[SerializeField]
	private float moveSpeed;
	[SerializeField]
	private float turnSpeed = 5f;

	private void Awake()
	{
		characterController = GetComponent<CharacterController>();
		//1animator = GetComponentInChildren<Animator>();
	}


    private void Start()
    {
		Init();
    }


	public void Init()
    {
		Cursor.lockState = CursorLockMode.Locked;
	}


    private void Update()
	{
		var horizontal = Input.GetAxis("Horizontal");
		var vertical = Input.GetAxis("Vertical");

		var movement = new Vector3(horizontal, 0, vertical);

		//characterController.SimpleMove(movement * Time.deltaTime * moveSpeed);
		characterController.Move(movement * Time.deltaTime * moveSpeed);
		//animator.SetFloat("Speed", movement.magnitude);

		
		
		if (movement.magnitude > 0)
		{
			//Quaternion newDirection = Quaternion.LookRotation(movement);

			Quaternion newDirection = transform.localRotation;

			transform.rotation = Quaternion.Slerp(transform.rotation, newDirection, Time.deltaTime * turnSpeed);
		}
		
	}
}

