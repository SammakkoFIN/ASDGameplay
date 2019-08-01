using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fire", menuName = "ScriptableObjects/Fire")]
public class Fire : ScriptableObject
{
    [SerializeField] private float spreadTimeInSeconds = 5f;
    public float SpreadTimeInSeconds { get => spreadTimeInSeconds; set => spreadTimeInSeconds = value; }

    // Fire is in a list so that we can always add or decrease the number of different fire's
    // Higher number in the list, the bigger fire.
    // Smallest fire starts from the [0].
    [SerializeField] private Transform[] fires;
    public Transform[] Fires { get => fires; set => fires = value; }

    [SerializeField] private Transform smoke;
    public Transform Smoke { get => smoke; set => smoke = value; }
}