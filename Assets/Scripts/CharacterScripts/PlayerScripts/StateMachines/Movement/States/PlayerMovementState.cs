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
		}
		public virtual void Update() { }
		public virtual void PhysicsUpdate() { }
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
