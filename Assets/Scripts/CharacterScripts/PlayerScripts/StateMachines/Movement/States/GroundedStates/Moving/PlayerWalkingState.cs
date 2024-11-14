using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaskedMischiefNamespace
{
	public class PlayerWalkingState : PlayerGroundedState
	{
		public PlayerWalkingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
		{
		}

        public override void Enter()
        {
            base.Enter();
            stateMachine.player.animator.SetTrigger("isWalking");
        }

        public override void Exit()
        {
            base.Exit();
            stateMachine.player.animator.ResetTrigger("isWalking");
        }

        public override void HandleInput()
		{
			base.HandleInput();
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();

		}
	}
}
