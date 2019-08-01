using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Interact")]
public class InteractAction : Action
{
    [SerializeField] private Indicator toggleLeverIndicator;
    public Indicator ToggleLeverIndicator { get => toggleLeverIndicator; set => toggleLeverIndicator = value; }

    private PlayerController player;

    public override void Act(StateController controller)
    {
        InteractionController interactionController = controller.transform.GetChild(0).GetComponent<InteractionController>();
        InteractableObject obj = interactionController.CastInteractionRay();
        if (Input.GetButtonDown("Interact") && obj != null)
            obj.OnInteract(player.gameObject);
    }

    public override void OnExit(StateController controller) {}

    public override void OnStart(StateController controller)
    {
        player = controller.transform.GetComponent<PlayerController>();
        if (player == null)
        {
            throw new System.Exception("InteractAction is not attached to the player");
        }
    }
}