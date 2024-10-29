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
			/*Collider other;
			stateMachine.player.IsGrounded(out other);
			Vector3 closestPoint = other.ClosestPointOnBounds(stateMachine.player.transform.position);
			Vector3 snappedPosition = new Vector3(stateMachine.player.transform.position.x, closestPoint.y + 0.1f, stateMachine.player.transform.position.z);
			stateMachine.player.transform.position = snappedPosition;*/
		}

		protected override void OnSprintStart(InputAction.CallbackContext callbackContext)
		{

		}

		public override void PhysicsUpdate()
		{
			PlayerRunner player = stateMachine.player;
			base.PhysicsUpdate();
			if(ground.distance > 0.1f)
			{
				player.transform.Translate(player.yVelocity * Vector3.up);
				player.yVelocity += player.gravity;
			}
			else
			{
				stateMachine.ChangeState(stateMachine.LandingState);
			}
		}
	}
}