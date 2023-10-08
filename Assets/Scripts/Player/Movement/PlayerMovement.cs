using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private AIBase pathfinding;
    [SerializeField] private PlayerInputHandling inputHandler;
    [SerializeField] private Transform movementRoot;
    [SerializeField] private float lookAheadMultiplier = 1;

    private Vector3 movementVector;

    private void Awake()
    {
        if (pathfinding == null) pathfinding = GetComponent<AIBase>();
        if (inputHandler == null) inputHandler = FindObjectOfType<PlayerInputHandling>();
        if (movementRoot == null) movementRoot = GetComponent<Transform>();
    }

    private void OnEnable()
    {
        inputHandler.MovementPerformedAction += ReadMovement;
        inputHandler.MovementStoppedAction += ReadStop;
        inputHandler.JumpPerformedAction += ReadJump;
    }

    private void OnDisable()
    {
        inputHandler.MovementPerformedAction -= ReadMovement;
        inputHandler.MovementStoppedAction -= ReadStop;
        inputHandler.JumpPerformedAction -= ReadJump;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    public void ReadJump(bool jump)
    {
        if (jump)
        {
            movementRoot.position += Vector3.up * 15;
        }
    }

    public void ReadMovement(Vector2 movement)
    {
        movementVector = new Vector3(movement.x, 0, movement.y);
    }

    public void ReadStop()
    {
        movementVector = Vector3.zero;
    }

    public void MovePlayer()
    {
        pathfinding.destination = movementRoot.position + movementVector * lookAheadMultiplier;
    }
}