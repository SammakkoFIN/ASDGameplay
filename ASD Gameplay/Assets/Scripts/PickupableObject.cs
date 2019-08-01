using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickupableObject : UsableObject
{
    [SerializeField] private Item_SO itemData;  //For adding this itemData to toolbar when the item is picked up
    public Item_SO ItemData { get => itemData; set => itemData = value; }

    protected bool isPickedUp;
    public bool IsPickedUp => isPickedUp;
    protected Rigidbody rigid;                  // Some items might have rigidbody attached to them

    //Function to determine the object has been picked up (and thus can be used)
    public virtual void Pickup()
    {
        isPickedUp = true;
        if (rigid != null)
        {
            rigid.useGravity = false;
            rigid.isKinematic = true;
            rigid.velocity = Vector3.zero;
        }
    }

    //Function to determine the object has been dropped (and thus can't be used)
    public virtual void Drop()
    {
        isPickedUp = false;
        if (rigid != null)
        {
            rigid.useGravity = true;
            rigid.isKinematic = false;
        }
    }
}