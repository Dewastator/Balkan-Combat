using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HitState : State
{
    private int hitTimes = 0;
    private float currentYPos;

    private float fallTime = 0;

    public HitState(Player player, PlayerStateMachine playerStateMachine, Animator animator, Rigidbody rb) : base(player, playerStateMachine, animator, rb)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        startTime = Time.time;
        currentYPos = player.transform.position.y;
        fallTime = 0;

        if (!player.groundedCheck.isGrounded)
        {
            player.ChangeAnimation(PlayerAnimation.Air_Hit.ToString(), 0.1f);
            Keyframe[] keyframes = player.fallAnimationCurve.keys;

            keyframes[0].value = currentYPos;
            keyframes[1].value = currentYPos + 1f;
            keyframes[keyframes.Length - 1].value = 2.24f;

            player.fallAnimationCurve.keys = keyframes;

            //AnimationUtility.SetKeyLeftTangentMode(player.fallAnimationCurve, 1, AnimationUtility.TangentMode.Auto);
        }
        else
        {
            player.ChangeAnimation(PlayerAnimation.Head_Hit.ToString(), 0.1f);
        }
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


        if (player.isRotated && player.groundedCheck.isGrounded)
        {
            playerStateMachine.ChangeState(playerStateMachine.rotationState);
        }

        if (time > 0.1f)
        {
            if (hitTimes != player.currentHitTimes)
            {
                //if (!player.groundedCheck.isGrounded)
                //{
                //    player.ChangeAnimation(PlayerAnimation.AirHit, 0.1f);
                //}
                //else
                //{
                //    player.ChangeAnimation(PlayerAnimation.Hit, 0.1f);
                //}
                //hitTimes = player.currentHitTimes;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && player.groundedCheck.isGrounded)
            {
                player.isHitted = false;
                SelectState();
            }
        }
        if (player.isDead)
        {
            playerStateMachine.ChangeState(playerStateMachine.deathState);
        }
        if (!player.groundedCheck.isGrounded)
        {
            float fall = HandleHitAnimation();

            animator.SetFloat("FallSpeed", fallTime);

            if (player.transform.position.y > 2.24f)
            {
                player.transform.position = new Vector3(player.transform.position.x, fall, player.transform.position.z);
            }
            else
            {
                player.isHitted = false;
                SelectState();
            }
        }

    }

    private float HandleHitAnimation()
    {
        fallTime += Time.deltaTime * player.fallSpeed;
       
        var fall = player.fallAnimationCurve.Evaluate(fallTime);
        return fall;
    }

    private void SelectState()
    {
        if (player.inputHandler.moveInput.x == 0 && player.groundedCheck.isGrounded)
        {
            playerStateMachine.ChangeState(playerStateMachine.idleState);
        }
        else if (player.isDead)
        {
            playerStateMachine.ChangeState(playerStateMachine.deathState);
        }
        else if (player.isWon)
        {
            playerStateMachine.ChangeState(playerStateMachine.celebrationState);
        }
        else if (player.inputHandler.IsInAir())
        {
            playerStateMachine.ChangeState(playerStateMachine.jumpState);
        }
        else if (player.inputHandler.isAttacking)
        {
            playerStateMachine.ChangeState(playerStateMachine.attackState);
        }
    }
}
