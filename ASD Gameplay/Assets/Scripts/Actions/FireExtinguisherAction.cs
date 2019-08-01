using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "PluggableAI/Actions/FireExtinguisher")]
public class FireExtinguisherAction : Action
{
    private FireExtinguisherController extinguisher;
    private PlayerController player;

    public override void OnStart(StateController controller)
    {
        //Get the fire extinguisher from player/toolbar
        extinguisher = controller.transform.GetComponent<PlayerController>().PlayerHand.Find("Fireblaster").GetComponent<FireExtinguisherController>();
    }

    public override void Act(StateController controller)
    {
        extinguisher.Use(null);
    }

    public override void OnExit(StateController controller)
    {
        extinguisher.Audio.Stop();
    }

    private InteractableObject getInteractObject(StateController stateController)
    {
        return stateController.transform.Find("Main Camera").GetComponent<InteractionController>().Interact();
    }
}