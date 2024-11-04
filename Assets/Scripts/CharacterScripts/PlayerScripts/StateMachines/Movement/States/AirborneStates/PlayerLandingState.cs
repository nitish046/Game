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
			Collider ground;
			if(isGrounded(out ground))
			{
				snapToGround(ground);
			}
			stateMachine.player.yVelocity = 0;
			if(staticMovement == new Vector2(0, 0))
				stateMachine.ChangeState(stateMachine.IdlingState);
			else
				stateMachine.ChangeState(stateMachine.WalkingState);
			
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
			
		}
	}
}

