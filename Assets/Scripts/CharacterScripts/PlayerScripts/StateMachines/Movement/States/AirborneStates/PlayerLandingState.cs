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
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
			if (movementInput.Equals(new Vector2(0, 0)))
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

