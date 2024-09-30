using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockState : State
{
    public BlockState(Player player, PlayerStateMachine playerStateMachine, Animator animator, Rigidbody rb) : base(player, playerStateMachine, animator, rb)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        startTime = Time.time;
        player.ChangeAnimation(PlayerAnimation.Block.ToString(), 0f);
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

        if(player.inputHandler.isBlocking)
        {
            if (player.isHitted)
            {
                player.ChangeAnimation(PlayerAnimation.Block_Hit.ToString(), 0.1f);
                player.isHitted = false;
            }

            if (time > 0.1f)
            {
                
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
                {
                    player.ChangeAnimation(PlayerAnimation.Block.ToString(), 0f);
                }
            }
        }
        else
        {
            if(player.inputHandler.moveInput.x == 0)
            {
                playerStateMachine.ChangeState(playerStateMachine.idleState);
            }
            else if(player.inputHandler.moveInput.x != 0)
            {
                playerStateMachine.ChangeState(playerStateMachine.moveState);
            }
            else if (player.inputHandler.IsInAir())
            {
                playerStateMachine.ChangeState(playerStateMachine.jumpState);
            }
            else if (player.isHitted)
            {
                playerStateMachine.ChangeState(playerStateMachine.hitState);
            }
            else if (player.inputHandler.isCrouching)
            {
                playerStateMachine.ChangeState(playerStateMachine.crouchState);
            }
        }
        
    }

}
