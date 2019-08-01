using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Use")]
public class UseDecision : Decision
{
    [SerializeField] private State moveState;
    public State MoveState { get => moveState; set => moveState = value; }

    public override State Decide(StateController controller)
    {
        if (Input.GetButtonUp("Fire1"))
            return MoveState;
        return null;
    }
}