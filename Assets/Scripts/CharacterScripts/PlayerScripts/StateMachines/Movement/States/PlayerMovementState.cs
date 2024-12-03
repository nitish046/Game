using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MaskedMischiefNamespace
{
	public class PlayerMovementState : IState
	{
		protected PlayerMovementStateMachine stateMachine;
		protected Vector2 movementInput;
		protected static Vector2 staticMovement;

        public float current_sprint_time;
        public float cooldown_timer;      
        public float sprint_time = 5f;
        public float sprint_cooldown = 3f;

        public PlayerMovementState(PlayerMovementStateMachine playerMovementStateMachine)
		{
			this.stateMachine = playerMovementStateMachine;
		}

		public virtual void Enter()
		{
			Debug.Log("Entering State: " + GetType().Name);
			AddCallbacks();
		}
		public virtual void Exit()
		{
			//Debug.Log("Exiting State: " + GetType().Name);
			RemoveCallbacks();
		}
		protected bool isGrounded()
		{
			stateMachine.player.Collisions(Vector3.down, stateMachine.player.yVelocity - 0.1f, new[] { "Terrain", "Collidable", "Hidable" });
			foreach (RaycastHit r in stateMachine.player.hits)
			{
				//Debug.Log(r);
				if (Vector3.Angle(r.normal, Vector3.up) < 15)
				{
					return true;
				}
			}
			return false;
			//return stateMachine.player.IsGrounded();
		}

		protected bool isGrounded(out Collider Ground)
		{
			stateMachine.player.Collisions(Vector3.down, stateMachine.player.yVelocity - 0.1f, new[] { "Terrain", "Collidable" });
			foreach (RaycastHit r in stateMachine.player.hits)
			{
				Debug.Log(r);
				if (Vector3.Angle(r.normal, Vector3.up) < 15)
				{
					Ground = r.collider;
					return true;
				}
			}
			Ground = null;
			return false;
			//return stateMachine.player.IsGrounded();
		}

		protected void snapToGround(Collider c)
		{
			Vector3 p = stateMachine.player.transform.position;
			if (c.GetType() == typeof(CapsuleCollider) || c.GetType() == typeof(BoxCollider) || c.GetType() == typeof(SphereCollider))
				p.y = c.ClosestPoint(p).y;
			else
				p.y = c.ClosestPointOnBounds(p).y;
			stateMachine.player.transform.position = p;
		}

		protected virtual void AddCallbacks()
		{
			var actions = stateMachine.player.gameInput.player_input_actions.Player;

			actions.Jump.started += OnJumpStart;
			actions.Sprint.started += OnSprintStart;
			actions.Sprint.canceled += OnSprintCancel;
			actions.Move.started += OnMoveStart;
			actions.Move.canceled += OnMoveCancel;
			actions.Win.started += OnWinStart;
			actions.PlaceTrap.started += OnPlaceTrapStart;
			actions.Pause.started += OnPauseStart;
		}
		protected virtual void RemoveCallbacks()
		{
			var actions = stateMachine.player.gameInput.player_input_actions.Player;

			actions.Jump.started -= OnJumpStart;
			actions.Sprint.started -= OnSprintStart;
			actions.Sprint.canceled -= OnSprintCancel;
			actions.Move.started -= OnMoveStart;
			actions.Move.canceled -= OnMoveCancel;
			actions.Win.started -= OnWinStart;
			actions.PlaceTrap.started -= OnPlaceTrapStart;
			actions.Pause.started -= OnPauseStart;
		}

		private void OnSprintCancel(InputAction.CallbackContext context)
		{
			stateMachine.player.isSprinting = false;
		}

		protected virtual void OnPlaceTrapStart(InputAction.CallbackContext callbackContext)
		{

		}


		//These methods can be overridden if any state needs its own logic.
		#region Universal
		public virtual void HandleInput()
		{
			movementInput = stateMachine.player.gameInput.getMovementInputVectorNormalized();
			/*
				   * The player's movement direction is actually decided by staticMovement
				   * If staticMovement stops being updated, the player is unable to turn
				   */
			staticMovement = movementInput;
		}
		public virtual void Update()
		{
			//if(Physics.CapsuleCast)
		}
		public virtual void PhysicsUpdate()
		{
			Vector3 cameraDir = stateMachine.player.mainCamera.transform.rotation.eulerAngles;
			Vector3 moveDir = Quaternion.Euler(0, cameraDir.y, 0) * new Vector3(staticMovement.x, 0, staticMovement.y);
			//stateMachine.player.transform.Translate(moveDir * stateMachine.player.walkSpeed);
			float speed = (stateMachine.player.isSprinting) ? stateMachine.player.runSpeed : stateMachine.player.walkSpeed;
            if (stateMachine.player.is_cooldown)
			{
                if (cooldown_timer >= sprint_cooldown)
                {
                    stateMachine.player.is_cooldown = false;
                    cooldown_timer = 0f;
                }
                cooldown_timer += Time.deltaTime;
            }
			if (!moveDir.Equals(new Vector3(0, 0, 0)))
			{
				Quaternion moveAngle = Quaternion.LookRotation(moveDir);
				stateMachine.player.transform.rotation = Quaternion.Slerp(stateMachine.player.transform.rotation, moveAngle, 0.2f);
				if (!stateMachine.player.WillCollide(stateMachine.player.transform.forward, speed, new[] { "Collidable" }))
					stateMachine.player.GetComponent<CharacterController>().Move(stateMachine.player.transform.forward * speed);

			}
			stateMachine.player.GetComponent<CharacterController>().Move(Vector3.down * -stateMachine.player.yVelocity);

		}
		#endregion

		//Each movement state will be using these callbacks in different ways
		//Override the functions in each movement state to modify their behavior
		#region Callbacks
		protected virtual void OnPauseStart(InputAction.CallbackContext callbackContext)
		{
			stateMachine.player.Pause();
		}
		protected virtual void OnWinStart(InputAction.CallbackContext callbackContext)
		{
			stateMachine.player.Win();
		}
		protected virtual void OnJumpStart(InputAction.CallbackContext callbackContext)
		{

		}
		protected virtual void OnSprintStart(InputAction.CallbackContext callbackContext)
		{
			stateMachine.player.isSprinting = true;
		}
		protected virtual void OnMoveStart(InputAction.CallbackContext callbackContext)
		{

		}
		protected virtual void OnMoveCancel(InputAction.CallbackContext callbackContext)
		{

		}



		#endregion
	}
}
