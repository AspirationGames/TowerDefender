using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshProUGUI))]
public class CoordinateLabeler : MonoBehaviour
{

    TextMeshPro coordinateLabel;
    Vector2Int coordinates;
    Color defaultColor;
    GridManager gridManager;

    private void Awake() 
    {
        coordinateLabel = GetComponent<TextMeshPro>();
        gridManager = FindObjectOfType<GridManager>();

        UpdateCoordinates(); //to run once in play mode
    }

    private void Start() 
    {
        defaultColor = Color.white;    
    }

    private void Update() 
    {

        

        if(!Application.isPlaying)
        {
            UpdateCoordinates();
            UpdateObjectName();
        }
        
        UpdateCoordinateColor();
        ToggleLables();
        
        
    }

    private void UpdateCoordinates()
    {
        if(gridManager == null)
        {
            return;
        }

        coordinates.x = Mathf.RoundToInt(transform.parent.position.x/gridManager.UnityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z/gridManager.UnityGridSize);
        coordinateLabel.text = $"{coordinates}";
    }

    private void UpdateObjectName()
    {   
        
        transform.parent.name = $"{coordinates}";
    }

    private void UpdateCoordinateColor()
    {
        if(gridManager == null)
        {
            Debug.Log("no grid found");
            return;
        }

        Node node = gridManager.GetGridNode(coordinates);
        if(node == null)
        {
            Debug.Log("no node found");
            return;
        }

        if(!node.isWalkable)
        {
            coordinateLabel.color = Color.red;
            return;
        }
        else if(node.isPath)
        {
            coordinateLabel.color = Color.cyan;
        }
        else if(node.isExplored)
        {
            coordinateLabel.color = Color.blue;
        }
        else
        {
            coordinateLabel.color = defaultColor;
        }
    }

    void ToggleLables()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            coordinateLabel.enabled = !coordinateLabel.enabled;
            
        }
        
    }

}
