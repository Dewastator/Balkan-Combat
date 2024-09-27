using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class RotationState : State
{
    public RotationState(Player player, PlayerStateMachine playerStateMachine, Animator animator, Rigidbody rb) : base(player, playerStateMachine, animator, rb)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        startTime = Time.time;
        if (Helper.FacingRight(player.transform))
        {
            player.transform.LookAt(new Vector3(player.enemyPosToRotateTo.x, player.transform.position.y, player.enemyPosToRotateTo.z), Vector3.up);
        }
        else
        {
            player.transform.LookAt(new Vector3(player.enemyPosToRotateTo.x, player.transform.position.y, player.enemyPosToRotateTo.z), Vector3.up);
        }
        player.isRotated = false;
        if(player.isHitted)
        {
            player.isHitted = false;
        }
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

        player.inputHandler.isAttacking = false;

        playerStateMachine.ChangeState(playerStateMachine.idleState);

        //else if (player.inputHandler.moveInput.x != 0)
        //{
        //    playerStateMachine.ChangeState(playerStateMachine.moveState);
        //}
        //else if (player.inputHandler.IsInAir())
        //{
        //    playerStateMachine.ChangeState(playerStateMachine.jumpState);
        //}
        //else if (player.isHitted)
        //{
        //    playerStateMachine.ChangeState(playerStateMachine.hitState);
        //}
        //else if (player.isDead)
        //{
        //    playerStateMachine.ChangeState(playerStateMachine.deathState);
        //}
        //else if (player.isWon)
        //{
        //    playerStateMachine.ChangeState(playerStateMachine.celebrationState);
        //}
    }

}
