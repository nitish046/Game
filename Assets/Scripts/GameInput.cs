using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{

  public event EventHandler OnInteractAction;

  private PlayerInputActions playerInputActions;

  private void Awake()
  {
    playerInputActions = new PlayerInputActions();
    playerInputActions.Player.Enable();

    playerInputActions.Player.Interact.performed += Interact_performed;
  }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public bool getJumpInput()
  {
    if (playerInputActions.Player.Jump.ReadValue<float>() == 1)
    {
      return true;
    }
    return false;
  }

  public bool getSprintInput()
  {
    if (playerInputActions.Player.Sprint.ReadValue<float>() == 1)
      return true;
    return false;
  }


  public Vector2 getMovementInputVectorNormalized()
  {
    Vector2 movementInput = playerInputActions.Player.Move.ReadValue<Vector2>();
    /*
     if(movementInput.y == 0)
     {
         movementInput.x = 0;
     }*/

    /*if (movementInput.y < 0)
    {
        movementInput.x *= -1;
    }*/

    movementInput = movementInput.normalized;

    return movementInput;
  }
}
