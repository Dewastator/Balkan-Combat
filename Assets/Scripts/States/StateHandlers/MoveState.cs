using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class MoveState : State
{

    public MoveState(Player player, PlayerStateMachine playerStateMachine, Animator animator, Rigidbody rb) : base(player, playerStateMachine, animator, rb)
    {
        
    }

    public override void OnEnter()
    {
        base.OnEnter();

        player.ChangeAnimation(PlayerAnimation.Move, 0.1f);
    }

    public override void OnExit()
    {
        base.OnExit();
        animator.SetFloat("MovementSpeed", 0);
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        var input = player.inputHandler.moveInput;
        var x = player.inputHandler.moveInput.x;

        if (!Helper.FacingRight(player.transform))
        {
            x = -x;
        }

        animator.SetFloat("MovementSpeed", x * player.animMovementSpeed);
        player.Move(input.x);

        if (input.x == 0)
        {
            playerStateMachine.ChangeState(playerStateMachine.idleState);
        }
        else if (player.inputHandler.IsInAir())
        {
            playerStateMachine.ChangeState(playerStateMachine.jumpState);
        }
        else if (player.inputHandler.isAttacking)
        {
            playerStateMachine.ChangeState(playerStateMachine.attackState);
        }
        else if (player.isHitted)
        {
            playerStateMachine.ChangeState(playerStateMachine.hitState);
        }
        else if (player.isDead)
        {
            playerStateMachine.ChangeState(playerStateMachine.deathState);
        }
        else if (player.isWon)
        {
            playerStateMachine.ChangeState(playerStateMachine.celebrationState);
        }
        else if(player.isRotated)
        {
            playerStateMachine.ChangeState(playerStateMachine.rotationState);
        }
        else if (player.inputHandler.isCrouching)
        {
            playerStateMachine.ChangeState(playerStateMachine.crouchState);
        }
    }
}
