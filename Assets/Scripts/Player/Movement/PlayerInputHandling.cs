using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputHandling : MonoBehaviour
{
    public event Action<Vector2> MovementPerformedAction;
    [SerializeField] private UnityEvent<Vector2> movementPerformedEvent;
    public event Action MovementStoppedAction;
    [SerializeField] private UnityEvent movementStoppedEvent;

    public event Action<bool> JumpPerformedAction;
    [SerializeField] private UnityEvent<bool> jumpPerformedEvent;

    public void OnMovement(InputAction.CallbackContext move)
    {
        if(move.performed)
        {
            Vector2 movement = move.ReadValue<Vector2>();
            MovementPerformedAction?.Invoke(movement);
            movementPerformedEvent.Invoke(movement);
        }
        else
        {
            MovementStoppedAction?.Invoke();
            movementStoppedEvent.Invoke();
        }
    }

    public void OnJump(InputAction.CallbackContext jump) 
    {
        JumpPerformedAction?.Invoke(jump.performed);
        jumpPerformedEvent.Invoke(jump.performed);
    }
}