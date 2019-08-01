using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InteractionController : MonoBehaviour
{
    [SerializeField] private PlayerRules playerRules;
    public PlayerRules PlayerRules { get => playerRules; set => playerRules = value; }

    private bool interactionKeyInUse = false;

    /// <summary>
    /// Checks if the player is looking at an interactable object when the player presses the interact button
    /// </summary>
    /// <returns>The interactable object the player is looking at</returns>
    public InteractableObject Interact()
    {
        if (Input.GetButtonDown("Interact"))
            return CastInteractionRay();
        else
            interactionKeyInUse = false;
        return null;
    }

    /// <summary>
    /// Returns a shipComponent if the player is looking at a shipcomponent and presses the repair button.
    /// </summary>
    /// <returns>The ship component the player is looking at</returns>
    //public ShipComponent Repair()
    //{
    //    if (IM.IsKeyPressed(InputKey.Interact))
    //    {
    //        InteractableObject interactObject = CastInteractionRay();
    //        if (interactObject is ShipComponent)
    //        {
    //            return (ShipComponent)interactObject;
    //        }
    //    }
    //    return null;
    //}

    /// <summary>
    /// Returns PickupableObject
    /// </summary>
    /// <returns></returns>
    public PickupableObject PickupItem()
    {
        if (Input.GetButtonDown("Interact"))
            return CastPickingRay();

        return null;
    }

    /// <summary>
    /// Casts ray which detects gameobject's with pickupableobject script attached to them and returns that gameobject which has it
    /// </summary>
    /// <returns></returns>
    public PickupableObject CastPickingRay()
    {
        RaycastHit hit = GetRayCast();

        if (hit.transform != null)
        {
            Transform otherObject = hit.collider.transform;
            if (otherObject.GetComponent<PickupableObject>() != null)
            {
                return otherObject.GetComponent<PickupableObject>();
            }
        }

        return null;
    }

    public GameObject GetGameObjectFromCastingRay()
    {
        RaycastHit hit = GetRayCast();
        if (hit.transform != null)
            return hit.transform.gameObject;
        return null;
    }

    public InteractableObject CastInteractionRay()
    {
        RaycastHit hit = GetRayCast();

        if (hit.transform != null)
        {
            Transform otherObject = hit.collider.transform;
            if (otherObject.GetComponent<InteractableObject>() != null)
            {
                return otherObject.GetComponent<InteractableObject>();
            }

        }

        return null;
    }

    private InteractableObject OneTime(InteractableObject obj)
    {
        if (interactionKeyInUse == false)
        {
            interactionKeyInUse = true;
            return obj;
        }
        return null;
    }

    private RaycastHit GetRayCast()
    {
        RaycastHit hit;
        int layerMask = 1 << 10;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;
        // Raycast from the center of the camera
        Ray ray = new Ray();
        if (Camera.main != null)
            ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0), Camera.MonoOrStereoscopicEye.Mono);
        Physics.Raycast(ray, out hit, PlayerRules.InteractionRange, layerMask);
        return hit;
    }

    /// <summary>
    /// Checks if an object of type T is on front of the player
    /// </summary>
    /// <typeparam name="T">Type to check for</typeparam>
    /// <returns>Object of type T or null</returns>
    public T CheckForObject<T>()
    {
        RaycastHit hit = GetRayCast();
        if (hit.transform != null)
        {
            T obj = hit.transform.GetComponent<T>();
            if (obj != null)
            {
                return obj;
            }
        }
        return default;
    }
}