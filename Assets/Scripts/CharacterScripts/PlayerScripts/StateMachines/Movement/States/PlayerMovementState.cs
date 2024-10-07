using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaskedMischiefNamespace
{
  public class PlayerMovementState : IState
  {
    public void Enter()
    {
      Debug.Log("Entering State: " + GetType().Name);
    }
    public void Exit()
    {
      Debug.Log("Exiting State: " + GetType().Name);
    }

    public void HandleInput() { }
    public void Update() { }
    public void PhysicsUpdate() { }
  }
}
