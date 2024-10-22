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

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
			Vector3 cameraDir = stateMachine.player.mainCamera.transform.rotation.eulerAngles;
			Vector3 moveDir = Quaternion.Euler(0, cameraDir.y, 0) * new Vector3(movementInput.x, 0, movementInput.y);
			//stateMachine.player.transform.Translate(moveDir * stateMachine.player.walkSpeed);
			stateMachine.player.GetComponent<CharacterController>().Move(moveDir * stateMachine.player.walkSpeed);
		}
	}
}
