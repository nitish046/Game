using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaskedMischiefNamespace
{
  public class PlayerMovementStateMachine : StateMachine
  {
    public PlayerIdlingState IdlingState { get; }
    public PlayerWalkingState WalkingState { get; }
    public PlayerRunningState RunningState { get; }

    public PlayerMovementStateMachine()
    {
      IdlingState = new PlayerIdlingState();
      WalkingState = new PlayerWalkingState();
      RunningState = new PlayerRunningState();
    }
  }
}
