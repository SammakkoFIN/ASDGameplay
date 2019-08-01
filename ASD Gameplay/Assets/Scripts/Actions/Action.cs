using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : ScriptableObject
{
    public abstract void OnStart(StateController controller);
    public abstract void Act(StateController controller);
    public abstract void OnExit(StateController controller);
}