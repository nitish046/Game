using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaskedMischiefNamespace
{
	public class PlayerMovementState : IState
	{
		protected PlayerMovementStateMachine stateMachine;
		public PlayerMovementState(PlayerMovementStateMachine playerMovementStateMachine)
		{
			this.stateMachine = playerMovementStateMachine;
		}

		public virtual void Enter()
		{
			Debug.Log("Entering State: " + GetType().Name);
		}
		public virtual void Exit()
		{
			Debug.Log("Exiting State: " + GetType().Name);
		}

		public virtual void HandleInput() { }
		public virtual void Update() { }
		public virtual void PhysicsUpdate() { }
	}
}
