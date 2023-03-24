using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public event EventHandler OnAction;

    private PlayerInputActions playerInputActions;
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Action.performed += Action;
        playerInputActions.Player.Pause.performed += Pause;   
    }
    private void Action(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (obj.phase == UnityEngine.InputSystem.InputActionPhase.Performed && OnAction != null)
        {
            OnAction(this, EventArgs.Empty);
        }
    }
    private void Pause(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        KitchenGameManager.Instance.SetPauseStatus(!KitchenGameManager.Instance.IsPaused());
    }
    public Vector2 MovementVector(bool normalized)
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        if (normalized)
        {
            return inputVector.normalized;
        }
        return inputVector;
    }
    public bool JumpingStatus()
    {
        return playerInputActions.Player.Jump.ReadValue<float>() == 1f;
    }
}
