using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStateMachine
{
    #region States
    public State CurrentPlayerState { get; set; }

    public IdleState idleState { get; private set; }
    public MoveState moveState { get; private set; }
    public JumpState jumpState { get; private set; }
    public AttackState attackState { get; private set; }
    public HitState hitState { get; private set; }
    public DeathState deathState { get; private set; }
    public CelebrationState celebrationState { get; private set; }
    public RotationState rotationState { get; private set; }
    public CrouchState crouchState{ get; private set; }
    public BlockState blockState{ get; private set; }

    #endregion
    

    public void Initialize(Player player, Animator animator, Rigidbody rb)
    {
        idleState = new IdleState(player, this, animator, rb);
        moveState = new MoveState(player, this, animator, rb);
        jumpState = new JumpState(player, this, animator, rb);
        attackState = new AttackState(player, this, animator, rb);
        hitState = new HitState(player, this, animator, rb);
        deathState = new DeathState(player, this, animator, rb);
        celebrationState = new CelebrationState(player, this, animator, rb);
        rotationState = new RotationState(player, this, animator, rb);
        crouchState = new CrouchState(player, this, animator, rb);
        blockState = new BlockState(player, this, animator, rb);

        CurrentPlayerState = idleState;
        CurrentPlayerState.OnEnter();
    }

    public void ChangeState(State newState)
    {
        if(newState != CurrentPlayerState)
        {
            CurrentPlayerState.OnExit();
            CurrentPlayerState = newState;
            CurrentPlayerState.OnEnter();
        }
    }
}
