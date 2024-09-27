using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DeathState : State
{
    private float currentYPos;
    float fallTime;
    public DeathState(Player player, PlayerStateMachine playerStateMachine, Animator animator, Rigidbody rb) : base(player, playerStateMachine, animator, rb)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        fallTime = 0;
        currentYPos = player.transform.position.y;
        player.ChangeAnimation(PlayerAnimation.Death, 0.1f);

        if (!player.groundedCheck.isGrounded)
        {
            Keyframe[] keyframes = player.fallAnimationCurve.keys;

            keyframes[0].value = currentYPos;

            keyframes[1].value = currentYPos + 1f;

            keyframes[keyframes.Length - 1].value = 2.24f;

            player.fallAnimationCurve.keys = keyframes;

            //AnimationUtility.SetKeyLeftTangentMode(player.fallAnimationCurve, 1, AnimationUtility.TangentMode.Auto);
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

        fallTime += Time.deltaTime * player.fallSpeed;
        

        var fall = player.fallAnimationCurve.Evaluate(fallTime);

        if (!player.groundedCheck.isGrounded)
        {
            player.transform.position = new Vector3(player.transform.position.x, fall, player.transform.position.z);
        }
    }
}
