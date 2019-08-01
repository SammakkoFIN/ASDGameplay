using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/State")]
public class State : ScriptableObject
{
    // List of actions we can do while in this state
    [SerializeField] private Action[] actions;
    public Action[] Actions { get => actions; set => actions = value; }

    // Decision list when we need to change to another state if decision is true
    [SerializeField] private Decision[] decisions;
    public Decision[] Decisions { get => decisions; set => decisions = value; }

    /// <summary>
    /// Called inside StateController's Update method
    /// </summary>
    /// <param name="controller"></param>
    public void UpdateState(StateController controller)
    {
        DoActions(controller);
        CheckTransitions(controller);
    }

    /// <summary>
    /// Call state's each actions OnStart when we enter the state
    /// </summary>
    /// <param name="controller"></param>
    public void EnterActions(StateController controller)
    {
        for (int i = 0; i < Actions.Length; i++)
        {
            Actions[i].OnStart(controller);
        }
    }

    /// <summary>
    /// Call state's each actions Act for every frame
    /// </summary>
    /// <param name="controller"></param>
    private void DoActions(StateController controller)
    {
        for (int i = 0; i < Actions.Length; i++)
        {
            Actions[i].Act(controller);
        }
    }

    /// <summary>
    /// Call state's each actions OnExit when we exit the state
    /// </summary>
    /// <param name="controller"></param>
    public void ExitActions(StateController controller)
    {

        for (int i = 0; i < Actions.Length; i++)
        {
            Actions[i].OnExit(controller);
        }
    }

    /// <summary>
    /// CheckTransition is called in StateController's Update method
    /// </summary>
    /// <param name="controller"></param>
    private void CheckTransitions(StateController controller)
    {
        foreach (Decision decision in Decisions)
        {
            State newState = decision.Decide(controller);
            if (newState != null && newState != controller.CurrentState)
            {
                controller.TransitionToState(newState);
                return;
            }
        }
    }
}