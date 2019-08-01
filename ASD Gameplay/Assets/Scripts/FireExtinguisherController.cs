using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisherController : PickupableObject
{
    private ParticleSystem pSystem;                                 // The particle system to set and use for the extinguisher

    [SerializeField, Range(1.0f, 25.0f)] private int range = 10;    // The range of the the extinguisher
    private float particleLifetime;                                 // Particle lifetime, calculated by using the range
    private List<ParticleCollisionEvent> collisionEvents;
    public Transform shaderOutlineObject;                           // For disabling outline shader in this part of the object
    [SerializeField] private new AudioSource audio;
    public AudioSource Audio { get => audio; set => audio = value; }

    private void Start()
    {
        pSystem = GetComponentInChildren<ParticleSystem>(); //Get the particle system
        pSystem.Stop();                                     //Disable particle system to be able to change its values
        audio = GetComponent<AudioSource>();
        var main = pSystem.main;                            //Get the main module from the particle system
        main.loop = false;                                  //Disable looping to make sure the extinguisher actually stops when the player releases the button
        var shape = pSystem.shape;                          //Get the shape of the cone
        shape.length = range;                               //Set its length to the desired range
        rigid = GetComponent<Rigidbody>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    /// <summary>
    /// Actual extinguisher-specific function that's used in the overrided Use() function
    /// </summary>
    /// <param name="target"></param>
    public void Extinguish(GameObject target)
    {
        //But only do something if it was targeted at a fire
        if (target != null && target.transform.tag == "Flammable")
        {
            FireGrid fireScript = target.GetComponent<FireGrid>();
            fireScript.DecreaseFire();
        }
    }

    // Called from Action
    public override void Use(GameObject target)
    {
        //Always spawn particles
        pSystem.Play();
        if (!audio.isPlaying)
            audio.Play();
    }

    // Change tag to untagged so outline shader wont be visible
    public override void Pickup()
    {
        base.Pickup();
        shaderOutlineObject.tag = "Untagged";
    }

    //Function to determine the object has been dropped (and thus can't be used)
    public override void Drop()
    {
        base.Drop();
        shaderOutlineObject.tag = "Interactable";
    }
}
