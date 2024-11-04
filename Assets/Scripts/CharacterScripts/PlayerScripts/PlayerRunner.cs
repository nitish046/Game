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
		[SerializeField] protected GameObject winLoseController;

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

		public void Win()
		{
			winLoseController.GetComponent<WinLoseControl>().WinGame();
		}

		private PlayerMovementStateMachine movementStateMachine;
		private void Awake()
		{
			gameInput = GetComponent<GameInput>();
			movementStateMachine = new PlayerMovementStateMachine(this);
			isSprinting = false;
			mainCamera = Camera.main;
		}

		private void Start()
		{
			Debug.Log("Start");
			if (!TryGetComponent<CharacterController>(out CharacterController))
			{
				CharacterController = gameObject.AddComponent<CharacterController>();

				CharacterController.center = new Vector3(0, 0.8222382f, 0);
				CharacterController.radius = 0.33f;
				CharacterController.height = 1.75962f;
			}
			Debug.Log(CharacterController);
			movementStateMachine.ChangeState(movementStateMachine.IdlingState);
		}
		private void OnDestroy()
		{
			movementStateMachine.ExitState();
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
