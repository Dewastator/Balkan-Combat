using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            player.ChangeAnimation(PlayerAnimation.RightLegKick, 0.1f);
        }
        if(player.inputHandler.leftLegKick)
        {
            player.ChangeAnimation(PlayerAnimation.LeftLegKick, 0.1f);
        }
        if (player.inputHandler.leftArmPunch)
        {
            player.ChangeAnimation(PlayerAnimation.LeftPunch, 0.1f);
        }
        if (player.inputHandler.rightArmPunch)
        {
            player.ChangeAnimation(PlayerAnimation.RightPunch, 0.1f);
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
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
            {
                player.inputHandler.isAttacking = false;
            }
        }
    }
}
