using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler on_interact_action;
    public event EventHandler on_place_trap_action; // Add this line

    public PlayerInputActions player_input_actions;

    private void Awake()
    {
        player_input_actions = new PlayerInputActions();
        player_input_actions.Player.Enable();

        player_input_actions.Player.Interact.performed += Interact_performed;
        //player_input_actions.Player.PlaceTrap.performed += PlaceTrap_performed;
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

    public Vector2 getMovementInputVectorNormalized()
    {
        Vector2 movement_input = player_input_actions.Player.Move.ReadValue<Vector2>();
        return movement_input.normalized;
    }
}
