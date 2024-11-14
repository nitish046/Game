using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaskedMischiefNamespace
{
	public class PlayerRunningState : PlayerGroundedState
	{
		public PlayerRunningState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
		{
		}

        public override void Enter()
        {
            base.Enter();
            stateMachine.player.animator.SetTrigger("isSprinting");
        }

        public override void Exit()
        {
            base.Exit();
            stateMachine.player.animator.ResetTrigger("isSprinting");
        }

    }
}
