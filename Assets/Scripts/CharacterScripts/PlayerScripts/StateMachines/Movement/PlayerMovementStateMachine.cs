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

		public PlayerMovementStateMachine(PlayerRunner playerRunner)
		{
			player = playerRunner;
			IdlingState = new PlayerIdlingState(this);
			WalkingState = new PlayerWalkingState(this);
			RunningState = new PlayerRunningState(this);
			FallingState = new PlayerFallingState(this);
		}

	}
}
