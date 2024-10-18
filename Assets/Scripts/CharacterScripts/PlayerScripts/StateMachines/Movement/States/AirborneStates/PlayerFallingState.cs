using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
			var player = stateMachine.player;
			if(player.IsGrounded())
			{
				stateMachine.ChangeState(stateMachine.IdlingState);
			}
			else
			{
				player.transform.Translate(0, player.yVelocity, 0);
				player.yVelocity -= player.gravity;
			}
		}
	}
}