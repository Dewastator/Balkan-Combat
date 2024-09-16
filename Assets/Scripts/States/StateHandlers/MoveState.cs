using System.Collections;
using System.Collections.Generic;
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
    }
}
