using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    [SerializeField] private Vector3 gridWorldSize;
    public Vector3 GridWorldSize { get => gridWorldSize; set => gridWorldSize = value; }

    [SerializeField] private float nodeRadius;
    public float NodeRadius { get => nodeRadius; set => nodeRadius = value; }

    [SerializeField] private bool showGridGizmos = true;
    public bool ShowGridGizmos { get => showGridGizmos; set => showGridGizmos = value; }

    [SerializeField] private Node[,,] grid;
    public Node[,,] GridProp { get => grid; set => grid = value; }

    private int gridSizeX;
    [HideInInspector] public int GridSizeX { get => gridSizeX; set => gridSizeX = value; }

    private int gridSizeY;
    [HideInInspector] public int GridSizeY { get => gridSizeY; set => gridSizeY = value; }

    private int gridSizeZ;
    [HideInInspector] public int GridSizeZ { get => gridSizeZ; set => gridSizeZ = value; }

    [SerializeField] private LayerMask floorLayer;
    public LayerMask FloorLayer { get => floorLayer; set => floorLayer = value; }

    private Vector3 worldBottomLeft;

    // Diameter for calculations
    float nodeDiameter;

    private void Start()
    {
        CreateGrid();
    }

    /// <summary>
    /// Initializes Grid by using gridWorldSize and nodeRadius.
    /// </summary>
    public void CreateGrid()
    {
        nodeDiameter = NodeRadius * 2;
        GridSizeX = Mathf.RoundToInt(GridWorldSize.x / nodeDiameter);
        GridSizeY = Mathf.RoundToInt(GridWorldSize.y / nodeDiameter);
        GridSizeZ = Mathf.RoundToInt(GridWorldSize.z / nodeDiameter);

        grid = new Node[GridSizeX, GridSizeY, GridSizeZ];  // initialize grid list's length by numbers of gridSizes.
        worldBottomLeft = transform.position - Vector3.right * GridWorldSize.x / 2 - Vector3.up * GridWorldSize.y / 2 - Vector3.forward * GridWorldSize.z / 2;     // bottom left corner

        for (int r = 0; r < gridSizeX; r++)       // r = row
        {
            for (int c = 0; c < gridSizeY; c++)   // c = col
            {
                for (int d = 0; d < gridSizeZ; d++)
                {
                    Vector3 worldPoint = worldBottomLeft + Vector3.right * (r * nodeDiameter + nodeRadius) + Vector3.up * (c * nodeDiameter + nodeRadius) + Vector3.forward * (d * nodeDiameter + nodeRadius);         // for prefabs like enemies, ground etc.

                    grid[r, c, d] = new Node(r, c, d, worldPoint); // store data. the Node is automatically RoomType.None by constructor

                    RaycastHit hit;
                    Physics.Raycast(worldPoint, Vector3.down, out hit, 3f, floorLayer);

                    if (hit.transform != null)
                    {
                        var worldP = worldPoint;
                        worldP.y = hit.point.y;

                        grid[r, c, d].Type = NodeType.Ground;
                        grid[r, c, d].WorldPoint = worldP;
                    }
                    else
                        grid[r, c, d].Type = NodeType.Empty;
                }
            }
        }
    }

    // Draw gizmos on editor mode
    private void OnDrawGizmos()
    {
        // White wire cube which shows size of the grid in editor only
        Gizmos.DrawWireCube(transform.position, new Vector3(GridWorldSize.x, GridWorldSize.y, GridWorldSize.z));
        if (ShowGridGizmos)
            GizmosGrid();
    }

    /// <summary>
    /// Display Gizmos in Unity Editor
    /// </summary>
    void GizmosGrid()
    {
        var nodeDiameter = nodeRadius * 2;
        var gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        var gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        var gridSizeZ = Mathf.RoundToInt(gridWorldSize.z / nodeDiameter);

        var grid = new Node[gridSizeX, gridSizeY];  // initialize grid list's length by numbers of gridSizes.
        var worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2 - Vector3.forward * gridWorldSize.z / 2;

        for (int r = 0; r < gridSizeX; r++)       // r = row
        {
            for (int c = 0; c < gridSizeY; c++)   // c = col
            {
                for (int d = 0; d < gridSizeZ; d++)
                {
                    Vector3 worldPoint = worldBottomLeft + Vector3.right * (r * nodeDiameter + nodeRadius) + Vector3.up * (c * nodeDiameter + nodeRadius) + Vector3.forward * (d * nodeDiameter + nodeRadius);

                    Color colour = new Color();
                    colour.a = 0.3f;
                    Gizmos.color = colour;

                    Gizmos.DrawCube(worldPoint, new Vector3(1f, 1f, 1f) * (nodeDiameter - .1f));
                }
            }
        }
    }

    /// <summary>
    /// Returns grid[x,y] based on position in world space
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <returns></returns>
    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        worldPosition -= transform.position;
        float percentX = (worldPosition.x / gridWorldSize.x) + .5f;
        float percentY = (worldPosition.y / gridWorldSize.y) + .5f;
        float percentZ = (worldPosition.z / gridWorldSize.z) + .5f;

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);
        percentZ = Mathf.Clamp01(percentZ);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        int z = Mathf.RoundToInt((gridSizeZ - 1) * percentZ);
        return grid[x, y, z];
    }

    /// <summary>
    /// Returns list of grid neighbours in current Node (8 directions).
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int z = -1; z <= 1; z++)
            {
                if (x == 0 && z == 0)
                    continue;

                int checkX = node.GridX + x;
                int checkZ = node.GridZ + z;

                if (checkX >= 0 && checkX < gridSizeX && checkZ >= 0 && checkZ < gridSizeZ)
                {
                    neighbours.Add(GridProp[checkX, node.GridY , checkZ]);
                }
            }
        }

        return neighbours;
    }
}