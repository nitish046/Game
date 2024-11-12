using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MaskedMischiefNamespace
{
	public class PlayerGroundedState : PlayerMovementState
	{
		public PlayerGroundedState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
		{
		}

		public override void Enter()
		{
			base.Enter();
		}

		//Only grounded states need to check for if the player is grounded, so PhysicsUpdate() is overriden to add that logic here only
		public override void PhysicsUpdate()
		{
			if (!isGrounded())
				stateMachine.ChangeState(stateMachine.FallingState);
			base.PhysicsUpdate();
		}

		public override void Update()
		{
			base.Update();
		}

		protected override void AddCallbacks()
		{
			base.AddCallbacks();
		}

		protected override void RemoveCallbacks()
		{
			base.RemoveCallbacks();
			
		}

		protected override void OnJumpStart(InputAction.CallbackContext callbackContext)
		{
			base.OnJumpStart(callbackContext);
			stateMachine.ChangeState(stateMachine.JumpingState);
		}
		protected override void OnMoveStart(InputAction.CallbackContext callbackContext)
		{
			base.OnMoveStart(callbackContext);
			if (stateMachine.player.isSprinting)
				stateMachine.ChangeState(stateMachine.RunningState);
			else
				stateMachine.ChangeState(stateMachine.WalkingState);
		}
		protected override void OnMoveCancel(InputAction.CallbackContext callbackContext)
		{
			base.OnMoveCancel(callbackContext);
			stateMachine.ChangeState(stateMachine.IdlingState);
			//Will be implemented later
			/*
			if (stateMachine.player.isSprinting)
				stateMachine.ChangeState(stateMachine.HardStoppingState);
			else
				stateMachine.ChangeState(stateMachine.SoftStoppingState);
			*/

		}
	}
}
