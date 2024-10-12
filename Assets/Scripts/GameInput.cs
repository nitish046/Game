using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{

  public event EventHandler on_interact_action;

  private PlayerInputActions player_input_actions;

  private void Awake()
  {
    player_input_actions = new PlayerInputActions();
    player_input_actions.Player.Enable();

    player_input_actions.Player.Interact.performed += Interact_performed;
  }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        on_interact_action?.Invoke(this, EventArgs.Empty);
    }

    public bool getJumpInput()
  {
    if (player_input_actions.Player.Jump.ReadValue<float>() == 1)
        return true;

    return false;
  }

  public bool getSprintInput()
  {
    if (player_input_actions.Player.Sprint.ReadValue<float>() == 1)
        return true;

    return false;
  }


  public Vector2 getMovementInputVectorNormalized()
  {
    Vector2 movement_input = player_input_actions.Player.Move.ReadValue<Vector2>();
    movement_input = movement_input.normalized;

    return movement_input;
  }
}
