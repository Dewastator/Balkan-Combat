using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(GroundedCheck))]
public class InputHandler : MonoBehaviour
{
    private GroundedCheck groundedCheck;

    public bool isAttacking;
    public bool leftLegKick;
    public bool rightLegKick;
    public bool rightArmPunch;
    public bool leftArmPunch;
    public bool jumpedUp;
    public bool forwardJump;
    public bool backJump;
    public bool isCrouching;
    public bool isBlocking;

    public bool isMovingForward;
    public bool isMovingLeft;

    public Vector2 moveInput;

    private void Start()
    {
        groundedCheck = GetComponent<GroundedCheck>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if (moveInput.x > 0)
        {
            isMovingForward = true;
        }
        else
        {
            isMovingLeft = false;
            isMovingForward = false;
        }
    }
    public void OnLeftPunch(InputAction.CallbackContext context)
    {
        if (groundedCheck.isGrounded)
        {
            isAttacking = true;
            leftArmPunch = true;
        }
    }

    public void OnRightPunch(InputAction.CallbackContext context)
    {
        if (groundedCheck.isGrounded)
        {
            isAttacking = true;
            rightArmPunch = true;
        }
    }

    public void OnLeftKick(InputAction.CallbackContext context)
    {
        if (groundedCheck.isGrounded)
        {
            isAttacking = true;
            leftLegKick = true;
        }
    }

    public void OnRightKick(InputAction.CallbackContext context)
    {
        if (groundedCheck.isGrounded)
        {
            isAttacking = true;
            rightLegKick = true;
        }
    }

    public void OnBackJump(InputAction.CallbackContext context)
    {
        if (groundedCheck.isGrounded && !IsAttacking() && !isCrouching)
        {
            backJump = true;
        }
    }

    public void OnForwardJump(InputAction.CallbackContext context)
    {
        if (groundedCheck.isGrounded && !IsAttacking() && !isCrouching)
        {
            forwardJump = true;
        }
    }


    public void OnJump(InputAction.CallbackContext context)
    {
        if (groundedCheck.isGrounded && !IsAttacking() && !isCrouching)
        {
            jumpedUp = true;
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() != 0)
        {
            isCrouching = true;
        }
        else
        {
            isCrouching = false;
        }
    }

    public void OnBlock(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() != 0)
        {
            isBlocking = true;
        }
        else
        {
            isBlocking = false;
        }
    }
    public bool IsInAir()
    {
        return jumpedUp || backJump || forwardJump;
    }
    
    public bool IsAttacking()
    {
        return rightArmPunch || rightLegKick || leftArmPunch || leftLegKick;
    }
}
