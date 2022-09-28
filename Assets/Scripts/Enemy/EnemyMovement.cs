using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    
    [SerializeField] [Range(0,5)] float moveSpeed = 1f;

    Enemy enemy;
    List<Node> path = new List<Node>();

    GridManager gridManager;
    PathFinding pathFinding;

    private void Awake() 
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinding = FindObjectOfType<PathFinding>();    
        enemy = GetComponent<Enemy>();    
    }
    private void Start() 
    {
        
    }
    private void OnEnable() 
    {
        ReturnToStart(); //moves enemy to first tile in path
        RecalculatePath(true);
           
    }

    void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates;

        if(resetPath)
        {
            coordinates = pathFinding.StartCoordinates;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }

        StopAllCoroutines(); //stop the enemy from moving while it finds a new path

        path.Clear();
        path = pathFinding.GetNewPath(coordinates);

        StartCoroutine(SetDestination()); 
    }

    void ReturnToStart()
    {
        transform.position =  gridManager.GetPositionFromCoordinates(pathFinding.StartCoordinates);
    }

    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false); //once for loop is complete disable game object
    }
    IEnumerator SetDestination()
    {
       
        for(int i = 1; i < path.Count; i++)
        {
            Vector3 destination = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            transform.LookAt(destination);
            yield return Move(destination);
        }

        FinishPath();
        
    }

    IEnumerator Move(Vector3 destination)
    {   
        float travelPercentage = 0f;
        Vector3 startPosition = transform.position;

        while(travelPercentage < 1)
        {       
            travelPercentage += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(startPosition, destination, travelPercentage);
            yield return new WaitForEndOfFrame();
        }
        
        
        
    }
}
