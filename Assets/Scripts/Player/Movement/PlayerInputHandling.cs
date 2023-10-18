using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputHandling : MonoBehaviour
{
    public event Action<Vector2> MovementPerformedAction;
    [Header("Player Movement")]
    [SerializeField] private UnityEvent<Vector2> movementPerformedEvent;
    public event Action MovementStoppedAction;
    [SerializeField] private UnityEvent movementStoppedEvent;

    public event Action<Vector2> CameraMovementPerformedAction;
    [Header("Camera Movement")]
    [SerializeField] private UnityEvent<Vector2> CameraMovementPerformedEvent;
    public event Action CameraMovementStoppedAction;
    [SerializeField] private UnityEvent CameraMovementStoppedEvent;

    public event Action<bool> JumpPerformedAction;
    [Header("Jump")]
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

    public void OnCameraMovement(InputAction.CallbackContext cameraMove)
    {
        if (cameraMove.performed)
        {
            Vector2 movement = cameraMove.ReadValue<Vector2>();
            CameraMovementPerformedAction?.Invoke(movement);
            CameraMovementPerformedEvent.Invoke(movement);
        }
        else
        {
            CameraMovementStoppedAction?.Invoke();
            CameraMovementStoppedEvent.Invoke();
        }
    }

    public void OnJump(InputAction.CallbackContext jump) 
    {
        JumpPerformedAction?.Invoke(jump.performed);
        jumpPerformedEvent.Invoke(jump.performed);
    }
}