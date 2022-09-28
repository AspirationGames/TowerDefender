using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;
    
    [Tooltip("The world grid size should match the unity editor snap settings")]
    [SerializeField] int unityGridSize = 15;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>(); 
    
    public Dictionary<Vector2Int, Node> Grid => grid;

    public int UnityGridSize => unityGridSize;

    void Awake() 
    {
       CreateGrid(); 
    }

    public Node GetGridNode(Vector2Int coordinate)
    {
        if(!grid.ContainsKey(coordinate))
        {
            Debug.Log($"grid doese not contain coordinate {coordinate}");
            return null;
        }

        return grid[coordinate];
    }

    public void BlockNode(Vector2Int coordinates)
    {
        if(grid.ContainsKey(coordinates))
        {
            grid[coordinates].isWalkable = false;
        }
    }

    public void ResetNodes()
    {
        foreach(KeyValuePair<Vector2Int, Node> entry in grid)
        {
            entry.Value.connectedTo = null;
            entry.Value.isExplored = false;
            entry.Value.isPath = false;
        }
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 worldPosition)
    {
        Vector2Int coordinates = new Vector2Int();

        coordinates.x = Mathf.RoundToInt(worldPosition.x/ unityGridSize);
        coordinates.y = Mathf.RoundToInt(worldPosition.z/ unityGridSize);

        return coordinates;
    }

    public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
    {
        Vector3 worldPosition = new Vector3 ();

        worldPosition.x = coordinates.x * unityGridSize;
        worldPosition.z = coordinates.y * unityGridSize;

        return worldPosition;
    }
    void CreateGrid()
    {
        for(int x = 0; x <= gridSize.x; x++)
        {
            for(int y = 0; y <= gridSize.y; y++)
            {
                Vector2Int coordinates = new Vector2Int(x,y);
                grid.Add(coordinates, new Node(coordinates , true) );
            }

        }
    }
}
