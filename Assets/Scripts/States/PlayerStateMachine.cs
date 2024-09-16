using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public State CurrentPlayerState { get; set; }

    public void Initialize(State startingState)
    {
        CurrentPlayerState = startingState;
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
