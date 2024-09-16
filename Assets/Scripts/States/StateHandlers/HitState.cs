using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : State
{
    private int hitTimes = 0;
    public HitState(Player player, PlayerStateMachine playerStateMachine, Animator animator, Rigidbody rb) : base(player, playerStateMachine, animator, rb)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        startTime = Time.time;
        player.ChangeAnimation(PlayerAnimation.Hit, 0.1f);
        hitTimes = player.currentHitTimes;
    }

    public override void OnExit()
    {
        base.OnExit();
        hitTimes = 0;
        player.currentHitTimes = 0;
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
            if (hitTimes != player.currentHitTimes)
            {
                player.ChangeAnimation(PlayerAnimation.Hit, 0.1f);
                hitTimes = player.currentHitTimes;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
            {
                player.isHitted = false;
            }
        }
    }
}
