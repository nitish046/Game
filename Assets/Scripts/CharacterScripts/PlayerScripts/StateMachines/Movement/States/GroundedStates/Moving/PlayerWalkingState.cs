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

		public override void HandleInput()
		{
			base.HandleInput();
		}

		public override void PhysicsUpdate()
		{
			Transform t = stateMachine.player.transform;
			base.PhysicsUpdate();
			RaycastHit hits;
			Vector3 movement = t.forward * stateMachine.player.walkSpeed;
			Physics.Raycast(t.position, staticMovement, out hits, stateMachine.player.walkSpeed);
			stateMachine.player.transform.Translate(movement, Space.World);
		}
	}
}
