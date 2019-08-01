using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{
    // Current state we are on at the moment
    // We will also start from this state 
    [SerializeField] private State currentState;
    public State CurrentState { get => currentState; set => currentState = value; }

    private void OnEnable()
    {
        CurrentState.EnterActions(this);
    }

    private void Update()
    {
        CurrentState.UpdateState(this);
    }

    /// <summary>
    /// Switch currentState to another state
    /// </summary>
    /// <param name="nextState"></param>
    public void TransitionToState(State nextState)
    {
        if (nextState != CurrentState)
        {
            CurrentState.ExitActions(this);

            CurrentState = nextState;

            CurrentState.EnterActions(this);
        }
    }

    public State GetCurrentState()
    {
        return currentState;
    }
}