using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaskedMischiefNamespace
{
	public class PlayerRunner : MonoBehaviour
	{
		public float yVelocity = 0;
		public float jumpStrength;
		public float gravity;
		public GameInput gameInput;
		public bool isSprinting;
		public float walkSpeed;
		public float runSpeed;
		public Camera mainCamera;
		private CharacterController CharacterController;

		private void OnTriggerEnter(Collider other)
		{
			if (!other.CompareTag("Player"))
				PlayerMovementStateMachine.triggers.Add(other);
			Debug.Log(other.tag);
		}

		private void OnTriggerExit(Collider other)
		{
			Debug.Log("Uncollide");
			if (!other.CompareTag("Player"))
				PlayerMovementStateMachine.triggers.Remove(other);
		}

		private void OnTriggerStay(Collider other)
		{
			
		}

		private void OnCollisionEnter(Collision collision)
		{
			
		}

		private PlayerMovementStateMachine movementStateMachine;
		private void Awake()
		{
			gameInput = GetComponent<GameInput>();
			movementStateMachine = new PlayerMovementStateMachine(this);
			isSprinting = false;
			mainCamera = Camera.main;
			CharacterController = GetComponent<CharacterController>();
		}

		private void Start()
		{
			movementStateMachine.ChangeState(movementStateMachine.IdlingState);
		}

		private void Update()
		{
			movementStateMachine.HandleInput();
			movementStateMachine.Update();
		}

		private void FixedUpdate()
		{
			movementStateMachine.PhysicsUpdate();

		}
		public bool IsGrounded()
		{
			//return Physics.Raycast(GetComponentInChildren<Rigidbody>().transform.position, -Vector3.up, 0.1f);
			return CharacterController.isGrounded;
		}
	}
}
