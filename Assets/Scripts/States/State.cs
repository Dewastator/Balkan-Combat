using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected Player player;
    protected PlayerStateMachine playerStateMachine;
    protected Animator animator;
    protected Rigidbody rb;

    protected float startTime;
    protected float time => Time.time - startTime;

    
    public State(Player player, PlayerStateMachine playerStateMachine, Animator animator, Rigidbody rb)
    {
        this.player = player;
        this.playerStateMachine = playerStateMachine;
        this.animator = animator;
        this.rb = rb;
    }
    public virtual void OnEnter() { }
    public virtual void OnUpdate() { }
    public virtual void OnFixedUpdate() { }
    public virtual void OnExit() { }
}
