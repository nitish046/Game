using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
namespace MaskedMischiefNamespace
{
	public interface IState
	{
		public void Enter();
		public void Exit();

		public void HandleInput();
		public void Update();
		public void PhysicsUpdate();
	}
}
