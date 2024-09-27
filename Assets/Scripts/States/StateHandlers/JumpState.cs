using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : State
{
    private float jumpTime = 0;
    private float startingYPos;
    private float startingXPos;
    public JumpState(Player player, PlayerStateMachine playerStateMachine, Animator animator, Rigidbody rb) : base(player, playerStateMachine, animator, rb)
    {
    }
    bool up, forward, back;

    public override void OnEnter()
    {
        base.OnEnter();

        if (player.isRotated)
        {
            playerStateMachine.ChangeState(playerStateMachine.rotationState);
        }
        startTime = Time.time;
        jumpTime = 0;
        startingXPos = player.transform.position.x;
        startingYPos = player.transform.position.y;
        SelectJumpType();
    }

    private void SelectJumpType()
    {
        if (player.inputHandler.jumpedUp)
        {
            up = true;
            player.ChangeAnimation(PlayerAnimation.JumpUp, 0.1f);
        }
        if (player.inputHandler.forwardJump)
        {
            up = false;
            forward = true;

            player.inputHandler.jumpedUp = false;

            if (!Helper.FacingRight(player.transform))
            {
                player.ChangeAnimation(PlayerAnimation.Backflip, 0.1f);
            }
            else
            {
                player.ChangeAnimation(PlayerAnimation.Frontflip, 0.1f);
            }
        }
        if (player.inputHandler.backJump)
        {
            up = false;
            back = true;

            player.inputHandler.jumpedUp = false;

            if (!Helper.FacingRight(player.transform))
            {
                player.ChangeAnimation(PlayerAnimation.Frontflip, 0.1f);
            }
            else
            {
                player.ChangeAnimation(PlayerAnimation.Backflip, 0.1f);
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();

        Reset();
    }

    private void Reset()
    {
        up = player.inputHandler.jumpedUp = false;
        forward = player.inputHandler.forwardJump = false;
        back = player.inputHandler.backJump = false;
        player.jumpDistance = player.startingJumpDistance;
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        jumpTime += Time.deltaTime * player.jumpSpeed;
        var jump = player.jumpAnimationCurve.Evaluate(jumpTime);
        var distanceJump = player.jumpDistanceAnimationCurve.Evaluate(jumpTime);

        if (up)
        {
            player.transform.position = new Vector3(player.transform.position.x, startingYPos + jump * player.jumpHeight, player.transform.position.z);
        }
        else if (forward)
        {
            player.transform.position = new Vector3(startingXPos + distanceJump * player.jumpDistance, startingYPos + jump * player.jumpHeight, player.transform.position.z);
        }
        else
        {
            player.transform.position = new Vector3(startingXPos - distanceJump * player.jumpDistance, startingYPos + jump * player.jumpHeight, player.transform.position.z);
        }
        
        animator.SetFloat("JumpSpeed", jumpTime);

        if(time > 0.1f) 
        {
            if (player.isHitted)
            {
                playerStateMachine.ChangeState(playerStateMachine.hitState);
            }
            
            if (player.groundedCheck.isGrounded)
            {
                Reset();
                SelectState();
            }
        }

        
    }

    private void SelectState()
    {
        if (player.inputHandler.moveInput.x != 0)
        {
            playerStateMachine.ChangeState(playerStateMachine.moveState);
        }
        else if (player.inputHandler.moveInput.x == 0)
        {
            playerStateMachine.ChangeState(playerStateMachine.idleState);
        }
        else if (player.inputHandler.isAttacking)
        {
            playerStateMachine.ChangeState(playerStateMachine.attackState);
        }
        else if (player.isDead)
        {
            playerStateMachine.ChangeState(playerStateMachine.deathState);
        }
        else if (player.isWon)
        {
            playerStateMachine.ChangeState(playerStateMachine.celebrationState);
        }
    }
}
