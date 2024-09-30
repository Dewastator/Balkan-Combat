using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

public class IdleState : State
{

    public IdleState(Player player, PlayerStateMachine playerStateMachine, Animator animator, Rigidbody rb) : base(player, playerStateMachine, animator, rb)
    {

    }

    public override void OnEnter()
    {
        base.OnEnter();
        startTime = Time.time;
        player.ChangeAnimation(PlayerAnimation.Idle.ToString(), 0.1f);
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (player.inputHandler.moveInput.x != 0)
        {
            playerStateMachine.ChangeState(playerStateMachine.moveState);
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
        else if (player.isRotated)
        {
            playerStateMachine.ChangeState(playerStateMachine.rotationState);
        }
        else if (player.inputHandler.isCrouching)
        {
            playerStateMachine.ChangeState(playerStateMachine.crouchState);
        }
        else if(player.inputHandler.isBlocking)
        {
            playerStateMachine.ChangeState(playerStateMachine.blockState);
        }
    }
}
