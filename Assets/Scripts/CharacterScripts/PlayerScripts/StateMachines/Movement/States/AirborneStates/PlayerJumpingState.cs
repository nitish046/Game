using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaskedMischiefNamespace
{
	public class PlayerJumpingState : PlayerMovementState
	{
		public PlayerJumpingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
		{
		}

		public override void Enter()
		{
			base.Enter();
			stateMachine.player.yVelocity += stateMachine.player.jumpStrength;
		}

		public override void Exit()
		{
			base.Exit();
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
			PlayerRunner player = stateMachine.player;
			player.transform.Translate(player.yVelocity * Vector3.up);
			if (ground.distance > 0.1f)
			{
				stateMachine.ChangeState(stateMachine.FallingState);
			}
		}
	}
}