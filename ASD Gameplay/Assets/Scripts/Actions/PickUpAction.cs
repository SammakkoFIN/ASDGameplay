using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/PickUp")]
public class PickUpAction : Action
{
    [SerializeField] private Indicator pickUpItemIndicator;
    public Indicator PickUpItemIndicator { get => pickUpItemIndicator; set => pickUpItemIndicator = value; }

    private InteractionController interactionController;

    public override void Act(StateController controller)
    {
        PickupableObject pickup = interactionController.CheckForObject<PickupableObject>();
        PickupableObject equipedItem = controller.transform.GetComponent<PlayerController>().PlayerHand.GetComponentInChildren<PickupableObject>();

        if (pickup != null && pickup != equipedItem)
        {
            ButtonIndicator.BI.DisplayButtonIndicator(PickUpItemIndicator);
            if (Input.GetButtonDown("Interact")) 
            {
                // Get the item data
                // If storeInToolBar false, don't add it to toolbar list
                // Add it to the hand and set currentActiveItem to -1 or something
                // else if true, add it and equip it

                if (pickup.ItemData != null)
                {
                    if (!pickup.ItemData.StoreInToolBar)
                    {
                        // Get toolbar
                        // instead of using GameObject.Find, we could link everything to player later on
                        // But since its not done yet, we'll use this solution for now

                        // Clear first
                        pickup.transform.SetParent(controller.GetComponent<PlayerController>().PlayerHand);
                        pickup.transform.localPosition = Vector3.zero;
                        pickup.transform.localRotation = Quaternion.identity;

                        // Active the item so we can use it
                        pickup.Pickup();
                    }
                }
                else
                {
                    Debug.LogError("Item we are trying to pick up, doesn't have Item_SO data attached to it!", pickup);
                }
            }
        }
    }

    // Not in use
    public override void OnExit(StateController controller) {}

    public override void OnStart(StateController controller)
    {
        interactionController = controller.transform.GetChild(0).GetComponent<InteractionController>();
    }
}