using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/FireExtinguishDecision")]
public class FireExtinguishDecision : ActiveItemDecision
{
    [SerializeField] private State fireExtinguishState;
    public State FireExtinguishState { get => fireExtinguishState; set => fireExtinguishState = value; }

    public override State Decide(StateController controller)
    {
        if (Input.GetButtonDown("Fire1"))
            return FireExtinguishState;
        return null;
    }
}