using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class AttackState : State
{
    public AttackState(Player player, PlayerStateMachine playerStateMachine, Animator animator, Rigidbody rb) : base(player, playerStateMachine, animator, rb)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        startTime = Time.time;
        if (player.inputHandler.rightLegKick)
        {
            player.ChangeAnimation(PlayerAnimation.Right_Kick.ToString(), 0.1f);
        }
        if(player.inputHandler.leftLegKick)
        {
            player.ChangeAnimation(PlayerAnimation.Left_Kick.ToString(), 0.1f);
        }
        if (player.inputHandler.leftArmPunch)
        {
            player.ChangeAnimation(PlayerAnimation.Left_Punch.ToString(), 0.1f);
        }
        if (player.inputHandler.rightArmPunch)
        {
            player.ChangeAnimation(PlayerAnimation.Right_Punch.ToString(), 0.1f);
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        player.inputHandler.rightLegKick = false;
        player.inputHandler.rightArmPunch = false;
        player.inputHandler.leftArmPunch = false;
        player.inputHandler.leftLegKick = false;
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if(time > 0.1f)
        {
            if (player.isHitted)
            {
                playerStateMachine.ChangeState(playerStateMachine.hitState);
            }
            else if (player.isRotated)
            {
                playerStateMachine.ChangeState(playerStateMachine.rotationState);
            }
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
            {
                player.inputHandler.isAttacking = false;
                SelectState();
            }
        }

        
    }

    private void SelectState()
    {
        if (player.inputHandler.moveInput.x == 0 && player.groundedCheck.isGrounded)
        {
            playerStateMachine.ChangeState(playerStateMachine.idleState);
        }
        else if (player.inputHandler.moveInput.x != 0)
        {
            playerStateMachine.ChangeState(playerStateMachine.moveState);
        }
        else if (player.inputHandler.IsInAir())
        {
            playerStateMachine.ChangeState(playerStateMachine.jumpState);
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
