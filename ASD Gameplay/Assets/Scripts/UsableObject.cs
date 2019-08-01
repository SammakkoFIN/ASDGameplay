using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UsableObject : MonoBehaviour
{
    public abstract void Use(GameObject target); //Function to use the item you are currently holding
}