using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MaskedMischiefNamespace
{
	public class PlayerMovementState : IState
	{
		protected PlayerMovementStateMachine stateMachine;
		protected Vector2 movementInput;
		protected static Vector2 staticMovement;
		public PlayerMovementState(PlayerMovementStateMachine playerMovementStateMachine)
		{
			this.stateMachine = playerMovementStateMachine;
		}

		public virtual void Enter()
		{
			//Debug.Log("Entering State: " + GetType().Name);
			AddCallbacks();
		}
		public virtual void Exit()
		{
			//Debug.Log("Exiting State: " + GetType().Name);
			RemoveCallbacks();
		}
		protected bool isGrounded()
		{
			foreach(Collider c in PlayerMovementStateMachine.triggers)
			{
				if (c == null)
					continue;
				if(c.CompareTag("Terrain") || c.CompareTag("Collidable"))
				{
					return true;
				}
			}
			return false;
			//return stateMachine.player.IsGrounded();
		}

		protected bool isGrounded(out Collider Ground)
		{
			foreach (Collider c in PlayerMovementStateMachine.triggers)
			{
				if (c == null)
					continue;
				if (c.CompareTag("Terrain") || c.CompareTag("Collidable"))
				{
					Ground = c;
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
			if(c.GetType() == typeof(CapsuleCollider) || c.GetType() == typeof(BoxCollider) || c.GetType() == typeof(SphereCollider))
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
			actions.Move.started += OnMoveStart;
			actions.Move.canceled += OnMoveCancel;
			actions.Win.started += OnWinStart;
			actions.PlaceTrap.started += OnPlaceTrapStart;
		}
		protected virtual void RemoveCallbacks() 
		{
			var actions = stateMachine.player.gameInput.player_input_actions.Player;

			actions.Jump.started -= OnJumpStart;
			actions.Sprint.started -= OnSprintStart;
			actions.Move.started -= OnMoveStart;
			actions.Move.canceled -= OnMoveCancel;
			actions.Win.started -= OnWinStart;
			actions.PlaceTrap.started -= OnPlaceTrapStart;
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
		public virtual void Update() { }
		public virtual void PhysicsUpdate()
		{
			Vector3 cameraDir = stateMachine.player.mainCamera.transform.rotation.eulerAngles;
			Vector3 moveDir = Quaternion.Euler(0, cameraDir.y, 0) * new Vector3(staticMovement.x, 0, staticMovement.y);
			//stateMachine.player.transform.Translate(moveDir * stateMachine.player.walkSpeed);
			if(!moveDir.Equals(new Vector3(0, 0, 0)))
			{
				Quaternion moveAngle = Quaternion.LookRotation(moveDir);
				stateMachine.player.transform.rotation = Quaternion.Slerp(stateMachine.player.transform.rotation, moveAngle, 0.2f);
				stateMachine.player.GetComponent<CharacterController>().Move(stateMachine.player.transform.forward * stateMachine.player.walkSpeed);
			}

		}
		#endregion

		//Each movement state will be using these callbacks in different ways
		//Override the functions in each movement state to modify their behavior
		#region Callbacks
		protected virtual void OnWinStart(InputAction.CallbackContext callbackContext)
		{
			stateMachine.player.Win();
		}
		protected virtual void OnJumpStart(InputAction.CallbackContext callbackContext)
		{

		}
		protected virtual void OnSprintStart(InputAction.CallbackContext callbackContext)
		{
			stateMachine.player.isSprinting = !stateMachine.player.isSprinting;
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
