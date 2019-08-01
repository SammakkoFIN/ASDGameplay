using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Move")]
public class MoveAction : Action
{
    private PlayerController player;

    public override void Act(StateController controller)
    {
        player.Move();
    }

    public override void OnExit(StateController controller)
    {
    }

    public override void OnStart(StateController controller)
    {
        player = controller.transform.GetComponent<PlayerController>();
    }
}