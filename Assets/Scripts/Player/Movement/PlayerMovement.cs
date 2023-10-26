using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerInputHandling inputHandler;
    [SerializeField] private Transform movementRoot;
    [SerializeField] private Transform movementCamera;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float sprintMultiplier;

    public bool CanMove {  get; private set; }
    public bool Sprinting {  get; private set; }

    private Vector3 movementVector;
    private Vector3 movementForward;
    private Vector3 movementRight;

    private void Awake()
    {
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
        if (jump) { rb.AddRelativeForce(Vector3.up * jumpMultiplier, ForceMode.Impulse); }
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
        if(CanMove)
        {
            movementForward = Vector3.ProjectOnPlane(movementCamera.position - transform.position, transform.up).normalized;
            movementRight = Vector3.Cross(movementForward, transform.up).normalized;
            Vector3 velocity = rb.velocity;
            float slowdownMultiplier = 1 + Mathf.Clamp(speedMultiplier / 1000, 0, speedMultiplier);
            velocity.x /= slowdownMultiplier;
            velocity.y /= slowdownMultiplier;
            rb.velocity = velocity;
            rb.AddForce(speedMultiplier * Time.fixedDeltaTime * (-movementForward * movementVector.z + movementRight * movementVector.x).normalized, ForceMode.Impulse);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, -movementForward);
        Gizmos.DrawRay(transform.position, movementRight);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, movementVector);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, (-movementForward * movementVector.z + movementRight * movementVector.x));
    }
}