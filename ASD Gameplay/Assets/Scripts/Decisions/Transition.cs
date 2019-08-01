using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Transition
{
    [SerializeField] private Decision[] decisions;
    public Decision[] Decisions { get => decisions; set => decisions = value; }

    [SerializeField] private State trueState;
    public State TrueState { get => trueState; set => trueState = value; }

    [SerializeField] private State falseState;
    public State FalseState { get => falseState; set => falseState = value; }
}