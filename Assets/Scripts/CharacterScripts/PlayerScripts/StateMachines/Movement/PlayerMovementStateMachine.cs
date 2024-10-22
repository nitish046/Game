using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaskedMischiefNamespace
{
	public class PlayerMovementStateMachine : StateMachine
	{
		public PlayerRunner player { get; }
		public PlayerIdlingState IdlingState { get; }
		public PlayerWalkingState WalkingState { get; }
		public PlayerRunningState RunningState { get; }
		public PlayerFallingState FallingState { get; }
		public PlayerJumpingState JumpingState { get; }
		public PlayerHardStoppingState HardStoppingState { get; }
		public PlayerSoftStoppingState SoftStoppingState { get; }

		public PlayerMovementStateMachine(PlayerRunner playerRunner)
		{
			player = playerRunner;
			IdlingState = new PlayerIdlingState(this);
			WalkingState = new PlayerWalkingState(this);
			RunningState = new PlayerRunningState(this);
			FallingState = new PlayerFallingState(this);
			JumpingState = new PlayerJumpingState(this);
			SoftStoppingState = new PlayerSoftStoppingState(this);
			HardStoppingState = new PlayerHardStoppingState(this);
		}

	}
}
