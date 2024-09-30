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
        //rb.constraints &= ~RigidbodyConstraints.FreezePositionY;

        if (player.inputHandler.jumpedUp)
        {
            up = true;
            player.ChangeAnimation(PlayerAnimation.Jumping_Up.ToString(), 0.1f);
            rb.velocity = new Vector3(0f, player.jumpHeight, 0f);
        }
        if (player.inputHandler.forwardJump)
        {
            up = false;
            forward = true;

            player.inputHandler.jumpedUp = false;


            if (!Helper.FacingRight(player.transform))
            {
                player.ChangeAnimation(PlayerAnimation.Backflip.ToString(), 0.1f);
                rb.velocity = new Vector3(player.jumpDistance, player.jumpHeight, 0f);
            }
            else
            {
                player.ChangeAnimation(PlayerAnimation.Forward_Flip.ToString(), 0.1f);
                rb.velocity = new Vector3(player.jumpDistance, player.jumpHeight, 0f);
            }
        }
        if (player.inputHandler.backJump)
        {
            up = false;
            back = true;

            player.inputHandler.jumpedUp = false;


            if (!Helper.FacingRight(player.transform))
            {
                player.ChangeAnimation(PlayerAnimation.Forward_Flip.ToString(), 0.1f);
                rb.velocity = new Vector3(-player.jumpDistance, player.jumpHeight, 0f);
            }
            else
            {
                player.ChangeAnimation(PlayerAnimation.Backflip.ToString(), 0.1f);
                rb.velocity = new Vector3(-player.jumpDistance, player.jumpHeight, 0f);
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();

        animator.speed = 1;
        Reset();
    }

    private void Reset()
    {
        up = player.inputHandler.jumpedUp = false;
        forward = player.inputHandler.forwardJump = false;
        back = player.inputHandler.backJump = false;
        player.jumpDistance = player.startingJumpDistance;
        animator.SetFloat("JumpSpeed", 1);
        //rb.constraints = RigidbodyConstraints.FreezeAll;
        //rb.constraints &= ~RigidbodyConstraints.FreezePositionX;
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();


    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        jumpTime = Helper.Map(rb.velocity.y, player.jumpHeight, -player.jumpHeight, 0, 1, true);

        animator.SetFloat("JumpSpeed", jumpTime);


        if (time > 0.1f) 
        {
            animator.speed = 0;

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
