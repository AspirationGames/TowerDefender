using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelection : MonoBehaviour
{
    [SerializeField] bool isSelectable;
    [SerializeField] Tower tower;

    GridManager gridManager;
    PathFinding pathFinding;

    Vector2Int tileCoordinate;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinding = FindObjectOfType<PathFinding>();
    }
    private void Start() 
    {
        
        if(gridManager != null)
        {
            tileCoordinate = gridManager.GetCoordinatesFromPosition(transform.position);
            SetIsWalkable();
        }


    }
    public bool IsSelectable
    {
        get {return isSelectable;}
    }

    private void OnMouseDown() 
    {
        if(gridManager.GetGridNode(tileCoordinate).isWalkable && !pathFinding.WillBlockPath(tileCoordinate))
        {
            bool towerPlaced = tower.PlaceTower(transform.position);
            
            if(towerPlaced)
            {
                gridManager.BlockNode(tileCoordinate);
                pathFinding.NotifyRecievers();
            }
            
        }
    }

    private void SetIsWalkable()
    {   
        
        if(isSelectable != false)
        {
            return;
        }
        else
        {
            gridManager.BlockNode(tileCoordinate);
        }
    }
}
