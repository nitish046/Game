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
		protected static bool isGrounded = false;
		public static RaycastHit ground;
		protected static bool staticIsGrounded = false;
		public static RaycastHit collision;
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
			Debug.Log("Exiting State: " + GetType().Name);
			RemoveCallbacks();
		}

		protected virtual void AddCallbacks() 
		{
			var actions = stateMachine.player.gameInput.player_input_actions.Player;

			actions.Jump.started += OnJumpStart;
			actions.Sprint.started += OnSprintStart;
			actions.Move.started += OnMoveStart;
			actions.Move.canceled += OnMoveCancel;
		}
		protected virtual void RemoveCallbacks() 
		{
			var actions = stateMachine.player.gameInput.player_input_actions.Player;

			actions.Jump.started -= OnJumpStart;
			actions.Sprint.started -= OnSprintStart;
			actions.Move.started -= OnMoveStart;
			actions.Move.canceled -= OnMoveCancel;
		}

		//These methods can be overridden if any state needs its own logic.
		#region Universal
		public virtual void HandleInput() 
		{
			movementInput = stateMachine.player.gameInput.getMovementInputVectorNormalized();
			/*
			 * The player's movement direction is actually decided by staticMovement
			 * If staticMovement stops being updated, the player is unable to turn
			 * This is what happens when the player is airborne
			 */
			staticMovement = movementInput;
		}
		public virtual void Update() 
		{
			stateMachine.player.FindGround(out ground);
		}
		public virtual void PhysicsUpdate()
		{
			if(stateMachine.player.yVelocity < stateMachine.player.gravity * 15)
			{
				stateMachine.player.yVelocity = stateMachine.player.gravity * 15;
			}
			Vector3 cameraDir = stateMachine.player.mainCamera.transform.rotation.eulerAngles;
			Vector3 moveDir = Quaternion.Euler(0, cameraDir.y, 0) * new Vector3(staticMovement.x, 0, staticMovement.y);
			//stateMachine.player.transform.Translate(moveDir * stateMachine.player.walkSpeed);
			if(!moveDir.Equals(new Vector3(0, 0, 0)))
			{
				Quaternion moveAngle = Quaternion.LookRotation(moveDir);
				stateMachine.player.transform.rotation = Quaternion.Slerp(stateMachine.player.transform.rotation, moveAngle, 0.2f);
			}

		}
		#endregion

		//Each movement state will be using these callbacks in different ways
		//Override the functions in each movement state to modify their behavior
		#region Callbacks
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
