using System;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
  public event EventHandler on_interact_action;
  public event EventHandler on_place_trap_action; // Add this line

  public event EventHandler on_hotbar_toggle_left;
  public event EventHandler on_hotbar_toggle_right;

  public event EventHandler on_use_item;

  public PlayerInputActions player_input_actions;

  private void Awake()
  {
    player_input_actions = new PlayerInputActions();
    player_input_actions.Player.Enable();

    player_input_actions.Player.Interact.performed += Interact_performed;
    //player_input_actions.Player.PlaceTrap.performed += PlaceTrap_performed;

    player_input_actions.Player.ToggleHotbarLeft.performed += ToggleHotbarLeft_performed;
    player_input_actions.Player.ToggleHotbarRight.performed += ToggleHotbarRight_performed;

    player_input_actions.Player.UseItem.performed += UseItem_performed;
  }

  private void Interact_performed(InputAction.CallbackContext obj)
  {
    on_interact_action?.Invoke(this, EventArgs.Empty);
  }

  private void PlaceTrap_performed(InputAction.CallbackContext obj) // Add this method
  {
    on_place_trap_action?.Invoke(this, EventArgs.Empty); // Trigger event
    Debug.Log("PlaceTrap action triggered"); // For testing if it works
  }

  public bool getJumpInput()
  {
    return player_input_actions.Player.Jump.ReadValue<float>() == 1;
  }

  public bool getSprintInput()
  {
    return player_input_actions.Player.Sprint.ReadValue<float>() == 1;
  }

  public bool getUseItemInput()
  {
    Debug.Log("getUseItemInput: " + player_input_actions.Player.UseItem.ReadValue<float>());
    return player_input_actions.Player.UseItem.ReadValue<float>() == 1;
  }

  private void UseItem_performed(InputAction.CallbackContext obj)
  {
    on_use_item?.Invoke(this, EventArgs.Empty);
  }

  public Vector2 getMovementInputVectorNormalized()
  {
    Vector2 movement_input = player_input_actions.Player.Move.ReadValue<Vector2>();
    return movement_input.normalized;
  }

  // public int getHotbarInput()
  // {
  //   Axis toggleAxis = player_input_actions.Player.ToggleHotbar.ReadValue<Axis>();
  //   return (toggleAxis == 0) ? 0 : ((toggleAxis < 0) ? -1 : 1);
  // }

  public bool getHotbarInputLeft()
  {
    return player_input_actions.Player.ToggleHotbarLeft.ReadValue<float>() == 1;
  }

  public bool getHotbarInputRight()
  {
    return player_input_actions.Player.ToggleHotbarRight.ReadValue<float>() == 1;
  }

  private void ToggleHotbarLeft_performed(InputAction.CallbackContext obj)
  {
    on_hotbar_toggle_left?.Invoke(this, EventArgs.Empty);
  }
  private void ToggleHotbarRight_performed(InputAction.CallbackContext obj)
  {
    on_hotbar_toggle_right?.Invoke(this, EventArgs.Empty);
  }
}
