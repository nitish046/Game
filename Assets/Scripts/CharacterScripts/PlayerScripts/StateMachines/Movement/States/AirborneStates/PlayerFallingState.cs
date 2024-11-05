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
			base.PhysicsUpdate();
			var player = stateMachine.player;
			if (isGrounded())
			{
				stateMachine.ChangeState(stateMachine.LandingState);
			}
			else
			{
				player.GetComponent<CharacterController>().Move(new Vector3(0, player.yVelocity, 0));
				player.yVelocity -= player.gravity;
			}
		}

		public override void Update()
		{
			base.Update();
			if(isGrounded())
			{
				stateMachine.ChangeState(stateMachine.LandingState);
			}
		}

	}
}