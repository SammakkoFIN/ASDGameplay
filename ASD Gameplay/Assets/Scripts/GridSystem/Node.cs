using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum NodeType { Ground, Empty }
public class Node
{
    [SerializeField] private int gridX;               // gridX position in grid list
    public int GridX { get => gridX; set => gridX = value; }

    [SerializeField] private int gridY;               // gridY position in grid list
    public int GridY { get => gridY; set => gridY = value; }

    [SerializeField] private int gridZ;               // gridZ position in grid list
    public int GridZ { get => gridZ; set => gridZ = value; }

    [SerializeField] private Vector3 worldPoint;
    public Vector3 WorldPoint { get => worldPoint; set => worldPoint = value; }

    [SerializeField] private Transform roomTransform; // transform for UI minimap
    public Transform RoomTransform { get => roomTransform; set => roomTransform = value; }

    [SerializeField] private bool onFire;             // boolean for checking if the grid is on fire so we can spread or make the fire bigger
    public bool OnFire { get => onFire; set => onFire = value; }

    [SerializeField] private NodeType type;
    public NodeType Type { get => type; set => type = value; }

    [SerializeField] private float offSet;
    public float OffSet { get => offSet; set => offSet = value; }

    // Constructor //
    public Node(int _gridX, int _gridY, int _gridZ, Vector3 _worldPoint)
    {
        gridX = _gridX;
        gridY = _gridY;
        gridZ = _gridZ;

        worldPoint = _worldPoint;

        offSet = Random.Range(-0.25f, 0.25f);

        worldPoint.x += offSet;
        worldPoint.z += offSet * -1;
    }

    public void FireOut()
    {
        OnFire = false;
    }

    public void SetOnFire()
    {
        OnFire = true;
    }
}