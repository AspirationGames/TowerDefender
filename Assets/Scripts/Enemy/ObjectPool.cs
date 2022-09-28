using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    //this script will instantiate our enemy prefab

    [SerializeField] GameObject enemyPrefab;
    [SerializeField][Range(0.1f, 30f)] float spawnDelay = 1f;
    [SerializeField][Range(0, 50)] int poolSize = 5;
    bool levelComplete = false;

    GameObject[] pool;

    void Awake() 
    {
        PopulatePool();
    }
    private void Start() 
    {
        StartCoroutine(SpawnEnemies());
    }

    void PopulatePool() //instantiates game objects
    {
        pool = new GameObject[poolSize];

        for(int i = 0; i < poolSize; i++ )
        {
            pool[i] = Instantiate(enemyPrefab, transform); //adds an instance of enemy to the pool
            pool[i].SetActive(false);
        }
    }

    void EnableEnemies()
    {
        foreach(GameObject obj in pool)
            {
                if(obj.activeInHierarchy == false)
                {
                    obj.SetActive(true);
                    return;
                }
            }
    }
    IEnumerator SpawnEnemies()
    {
        while(!levelComplete)
        {
            EnableEnemies();
            yield return new WaitForSeconds(spawnDelay);
        }

    }
}
