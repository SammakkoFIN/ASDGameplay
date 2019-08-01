using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ActiveItemDecision : Decision
{
    [SerializeField] private Item_SO[] requiredItems;
    public Item_SO[] RequiredItems { get => requiredItems; set => requiredItems = value; }

    private Transform playerHand;

    protected bool IsItemActive(StateController controller)
    {
        if (playerHand == null)
            playerHand = controller.transform.GetComponent<PlayerController>().PlayerHand;
        Item_SO activeItem = new Item_SO();
        if (playerHand.GetChild(0).GetComponent<PickupableObject>() != null)
            activeItem = playerHand.GetChild(0).GetComponent<PickupableObject>().ItemData;

        return RequiredItems.Contains(activeItem);
    }
}