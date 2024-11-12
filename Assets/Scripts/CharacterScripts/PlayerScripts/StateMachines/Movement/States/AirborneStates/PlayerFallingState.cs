using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MaskedMischiefNamespace
{
	public class PlayerFallingState : PlayerMovementState
	{
		public PlayerFallingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
		{
		}

		public override void Enter()
		{
			base.Enter();
		}

		public override void Exit()
		{
			base.Exit();
			stateMachine.player.yVelocity = 0;
		}

		protected override void OnSprintStart(InputAction.CallbackContext callbackContext)
		{

		}

		public override void PhysicsUpdate()
		{
			if (isGrounded())
			{
				stateMachine.ChangeState(stateMachine.LandingState);
			}
			else
			{
				stateMachine.player.yVelocity -= stateMachine.player.gravity;
			}
			base.PhysicsUpdate();
		}

		public override void Update()
		{
			base.Update();
		}

	}
}