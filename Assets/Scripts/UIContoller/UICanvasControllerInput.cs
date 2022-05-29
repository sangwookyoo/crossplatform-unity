using UnityEngine;

public class UICanvasControllerInput : MonoBehaviour
{
    [Header("Output")]
    public PlayerInputAction playerInputAction;

    public void VirtualMoveInput(Vector2 virtualMoveDirection)
    {
        playerInputAction.MoveInput(virtualMoveDirection);
    }

    public void VirtualLookInput(Vector2 virtualLookDirection)
    {
        playerInputAction.LookInput(virtualLookDirection);
    }

    public void VirtualJumpInput(bool virtualJumpState)
    {
        playerInputAction.JumpInput(virtualJumpState);
    }

    public void VirtualSprintInput(bool virtualSprintState)
    {
        playerInputAction.SprintInput(virtualSprintState);
    }
}