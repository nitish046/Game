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
		public Rigidbody rigidbody;

		private PlayerMovementStateMachine movementStateMachine;
		private void Awake()
		{
			gameInput = GetComponent<GameInput>();
			movementStateMachine = new PlayerMovementStateMachine(this);
			isSprinting = false;
			mainCamera = Camera.main;
			rigidbody = GetComponentInChildren<Rigidbody>();
		}

		private void OnTriggerEnter(Collider other)
		{
			Debug.Log(other.name);
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
		public bool IsGrounded(out Collider other)
		{
			var capsule = GetComponentInChildren<CapsuleCollider>();
			Vector3 center = transform.TransformPoint(capsule.center);
			float adjHeight = (capsule.height - capsule.radius) / 2;

			Vector3 a = new Vector3(center.x, center.y + adjHeight, center.z);
			Vector3 b = new Vector3(center.x, center.y - adjHeight, center.z);

			//return Physics.Raycast(transform.position, -Vector3.up, 0.1f);
			RaycastHit h;
			bool c = Physics.CapsuleCast(a, b, capsule.radius, Vector3.down, out h, Mathf.Abs(yVelocity - 0.1f));
			if(c)
			{
				//Debug.Log(h.collider.transform.position);
			}
			other = h.collider;
			return c;
		}
		public void FindGround(out RaycastHit other)
		{
			RaycastHit h;
			bool c = Physics.Raycast(transform.position, Vector3.down, out h);
			if (c)
			{
				//Debug.Log(h.collider.transform.position);
			}
			//Debug.Log(h.distance);
			other = h;
		}
		public void CheckCollision(out RaycastHit other)
		{
			RaycastHit h;
			bool c = Physics.Raycast(transform.position, transform.forward, out h);
			//Debug.Log(h.collider.name);
				//Debug.Log(h.collider.transform.position);
			
			//Debug.Log(h.distance);
			other = h;
		}
	}
}
