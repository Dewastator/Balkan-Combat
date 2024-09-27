using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchState : State
{
    public CrouchState(Player player, PlayerStateMachine playerStateMachine, Animator animator, Rigidbody rb) : base(player, playerStateMachine, animator, rb)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        startTime = Time.time;
        player.ChangeAnimation(PlayerAnimation.StandingToCrouch, 0.1f);
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
        if (player.inputHandler.isCrouching)
        {
            if (time > 0.1f)
            {
                if (player.isHitted)
                {
                    playerStateMachine.ChangeState(playerStateMachine.hitState);
                }
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
                {
                    player.ChangeAnimation(PlayerAnimation.CrouchIdle, 0f);
                }
            }
        }
        else
        {
            playerStateMachine.ChangeState(playerStateMachine.idleState);
            
        }
        

        
    }
}
