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

    public bool isMovingRight;
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
            isMovingRight = true;
        }else if(moveInput.x < 0)
        {
            isMovingLeft = true;
        }
        else
        {
            isMovingLeft = false;
            isMovingRight = false;
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
        if (groundedCheck.isGrounded)
        {
            backJump = true;
        }
    }

    public void OnForwardJump(InputAction.CallbackContext context)
    {
        if (groundedCheck.isGrounded)
        {
            forwardJump = true;
        }
    }


    public void OnJump(InputAction.CallbackContext context)
    {
        if (groundedCheck.isGrounded)
        {
            jumpedUp = true;
        }
    }

    public bool IsInAir()
    {
        return jumpedUp || backJump || forwardJump;
    }
}
