using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
			var player = stateMachine.player;

			if(!player.IsGrounded())
			{
				stateMachine.ChangeState(stateMachine.FallingState);
			}
		}
	}
}
