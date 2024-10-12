using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaskedMischiefNamespace
{
  public class PlayerRunner : MonoBehaviour
  {
    private PlayerMovementStateMachine movementStateMachine;

    private void Awake()
    {
      movementStateMachine = new PlayerMovementStateMachine();
    }

    private void Start()
    {
      movementStateMachine.ChangeState(movementStateMachine.IdlingState);
    }

    private void Update()
    {
      movementStateMachine.HandleInput();
      movementStateMachine.Update();
    }

    private void FixedUpdate()
    {
      movementStateMachine.PhysicsUpdate();
    }
  }
}
