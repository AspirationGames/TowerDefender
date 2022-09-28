using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{   
    [SerializeField] Vector2Int startCoordinates;
    [SerializeField] Vector2Int goalCoordinates;

    Node startNode;
    Node goalNode;
    Node currentSearchNode;
    Dictionary<Vector2Int, Node> explored = new Dictionary<Vector2Int, Node>();
    Queue<Node> frontier = new Queue<Node>();
    Vector2Int[] searchDirectionOrder = {Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down};

    GridManager gridManager;

    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    public Vector2Int StartCoordinates => startCoordinates;
    public Vector2Int GoalCoordinates => goalCoordinates;

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        grid = gridManager.Grid;

        startNode = grid[startCoordinates];
        goalNode = grid[goalCoordinates];
        
    }

    void Start() 
    {
        

        GetNewPath();

    }

    public List<Node> GetNewPath()
    {
        return GetNewPath(startCoordinates);
    }

    public List<Node> GetNewPath(Vector2Int coordinates)
    {
        gridManager.ResetNodes();
        BreadthFirstSearch(coordinates);
        return BuildPath();
    }

    void UpdatePath()
    {

    }

    private void ExploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();
        

        foreach(Vector2Int direction in searchDirectionOrder)
        {
            Vector2Int searchCoordinate = currentSearchNode.coordinates + direction;
            if(grid.ContainsKey(searchCoordinate))
            {
                neighbors.Add(grid[searchCoordinate]);
            }
        }

        foreach(Node neighbor in neighbors)
        {
            if(!explored.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
            {
                neighbor.connectedTo = currentSearchNode;
                frontier.Enqueue(neighbor);
                explored.Add(neighbor.coordinates, neighbor);
            }
        }
    
    }

    void BreadthFirstSearch(Vector2Int coordinates)
    {
        startNode.isWalkable = true;
        goalNode.isWalkable = true;

        //resets pathfinding
        frontier.Clear();
        explored.Clear(); 

        //Starts Path Finding
        bool isRunning = true;

        frontier.Enqueue(grid[coordinates]);
        explored.Add(coordinates, grid[coordinates]);

        while(frontier.Count > 0 && isRunning)
        {
          currentSearchNode = frontier.Dequeue(); //we are taking the first item in the Queue or next item and it will be our current search node
          currentSearchNode.isExplored = true;
          ExploreNeighbors(); //explores the current search nodes neighbors
          if(currentSearchNode.coordinates == goalCoordinates)
          {
                isRunning = false;
                break;
          }
        }

    }

    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = goalNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        while(currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
        }
        
        path.Reverse();
        return path;

    }

    public bool WillBlockPath(Vector2Int coordinates) //this method checks if placing a block will prevent the enemey from finding an alternate path (i.e. completlely block path)
    {                                                   //it is used as a check for the Tile selection script when placing a tower`

        if(grid.ContainsKey(coordinates))
        {
            bool previousState = grid[coordinates].isWalkable;


            grid[coordinates].isWalkable = false;
            List<Node> newPath = GetNewPath();
            grid[coordinates].isWalkable = previousState;

            if(newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }

            return false;
         
        }

        return false;
    }

    public void NotifyRecievers()
    {
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }
}
