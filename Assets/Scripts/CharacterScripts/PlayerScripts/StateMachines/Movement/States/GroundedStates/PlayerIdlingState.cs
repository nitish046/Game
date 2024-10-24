using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaskedMischiefNamespace
{
	public class PlayerIdlingState : PlayerGroundedState
	{
		public PlayerIdlingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
		{
		}

		public override void Enter()
		{
			base.Enter();
			staticMovement = new Vector2(0, 0);
		}
	}
}
