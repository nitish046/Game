using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
		public Animator animator;
		public Camera mainCamera;
		private CharacterController CharacterController;
		private Rigidbody rigidbody;
		[SerializeField] public GameObject trapPrefab; //This is the trap
		[SerializeField] protected GameObject winLoseController;
		public List<RaycastHit> hits;
        public bool is_cooldown = false;
        //hiding
        public Material[] materials; // Assign your two materials here

		private bool isTransparent = false;
		bool click;
		private void OnTriggerEnter(Collider other)
		{
			if (!other.CompareTag("Player"))
				PlayerMovementStateMachine.triggers.Add(other);
			//Debug.Log(other.tag);
		}

		private void ToggleTransparency()
		{
			isTransparent = !isTransparent;

			foreach (Material mat in materials)
			{
				Color color = mat.color;
				color.a = isTransparent ? 0f : 1f; // Toggle between fully transparent and opaque
				mat.color = color;
			}
		}
		private void OnTriggerExit(Collider other)
		{
			//Debug.Log("Uncollide");
			if (!other.CompareTag("Player"))
				PlayerMovementStateMachine.triggers.Remove(other);
			if (other.gameObject.tag == "hide")
			{
				if (click)
				{
					ToggleTransparency();
					click = false;
				}
			}
		}

		private void OnTriggerStay(Collider other)
		{
			if (other.gameObject.tag == "hide")
			{
				if (Input.GetKeyDown(KeyCode.F))
				{
					ToggleTransparency();
					click = true;
				}
			}
		}

		private void OnCollisionEnter(Collision collision)
		{

		}

		public bool WillCollide(Vector3 dir, float len)
		{
			Vector3 center = CharacterController.center;
			float radius = CharacterController.radius;
			float height = CharacterController.height;

			float offset = (height / 2) - radius;

			Vector3 a = center;
			a.y += offset;
			Vector3 b = center;
			b.y -= offset;

			LayerMask mask = Convert.ToInt32("10000000", 2);
			RaycastHit[] hits = rigidbody.SweepTestAll(dir, Mathf.Abs(len));

			foreach (RaycastHit hit in hits)
			{
				if (!(hit.collider.CompareTag("Henry") || hit.collider.CompareTag("Terrain")))
					return true;
			}
			return false;
		}
		public bool WillCollide(Vector3 dir, float len, string[] tags)
		{
			Vector3 center = CharacterController.center;
			float radius = CharacterController.radius;
			float height = CharacterController.height;

			float offset = (height / 2) - radius;

			Vector3 a = center;
			a.y += offset;
			Vector3 b = center;
			b.y -= offset;

			LayerMask mask = Convert.ToInt32("10000000", 2);
			RaycastHit[] hits = rigidbody.SweepTestAll(dir, Mathf.Abs(len));

			foreach (RaycastHit hit in hits)
			{
				foreach (string s in tags)
				{
					if (hit.collider.CompareTag(s))
						return true;
				}
			}
			return false;
		}

		public void Collisions(Vector3 dir, float len, string[] tags)
		{
			hits = new List<RaycastHit>();
			Vector3 center = CharacterController.center;
			float radius = CharacterController.radius;
			float height = CharacterController.height;

			float offset = (height / 2) - radius;

			Vector3 a = center;
			a.y += offset;
			Vector3 b = center;
			b.y -= offset;

			LayerMask mask = Convert.ToInt32("10000000", 2);
			List<RaycastHit> tempList = rigidbody.SweepTestAll(dir, Mathf.Abs(len)).ToList();

			foreach (RaycastHit hit in tempList)
			{
				foreach (string s in tags)
				{
					if (hit.collider.CompareTag(s))
						hits.Add(hit);
				}
			}
		}
		public void Collisions(Vector3 dir, float len)
		{
			hits = new List<RaycastHit>();
			Vector3 center = CharacterController.center;
			float radius = CharacterController.radius;
			float height = CharacterController.height;

			float offset = (height / 2) - radius;

			Vector3 a = center;
			a.y += offset;
			Vector3 b = center;
			b.y -= offset;

			LayerMask mask = Convert.ToInt32("10000000", 2);
			List<RaycastHit> tempList = rigidbody.SweepTestAll(dir, len).ToList();

			foreach (RaycastHit hit in tempList)
			{
				if (!(hit.collider.CompareTag("Henry")))
					hits.Add(hit);
			}
		}
		public void Win()
		{
			Debug.Log("Winning from Player");
			winLoseController.GetComponent<WinLoseControl>().WinGame();
		}
		public void Pause()
		{
			winLoseController.GetComponent<WinLoseControl>().PauseGame();
		}

		private PlayerMovementStateMachine movementStateMachine;
		private void Awake()
		{
			gameInput = GetComponent<GameInput>();
			movementStateMachine = new PlayerMovementStateMachine(this);
			isSprinting = false;
			mainCamera = Camera.main;
			rigidbody = GetComponent<Rigidbody>();
            animator = transform.GetChild(0).GetComponent<Animator>();
        }

		private void Start()
		{
			//Debug.Log("Start");
			if (!TryGetComponent<CharacterController>(out CharacterController))
			{
				CharacterController = gameObject.AddComponent<CharacterController>();

				CharacterController.center = new Vector3(0, 0.8222382f, 0);
				CharacterController.radius = 0.33f;
				CharacterController.height = 1.75962f;
			}
			//Debug.Log(CharacterController);
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

