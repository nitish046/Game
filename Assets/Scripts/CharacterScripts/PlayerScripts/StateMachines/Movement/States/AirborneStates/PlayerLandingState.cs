using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaskedMischiefNamespace
{
	public class PlayerLandingState : PlayerMovementState
	{
		public PlayerLandingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
		{
		}

		public override void Enter()
		{
			base.Enter();
			stateMachine.player.yVelocity = 0;
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
			Transform playerTransform = stateMachine.player.transform;
			Debug.Log(ground.point);
			playerTransform.position = new Vector3(playerTransform.position.x, ground.point.y + 0.1f, playerTransform.position.z);
			if (staticMovement == Vector2.zero)
			{
				stateMachine.ChangeState(stateMachine.IdlingState);
			}
			else
			{
				stateMachine.ChangeState(stateMachine.WalkingState);
			}
		}
	}
}

