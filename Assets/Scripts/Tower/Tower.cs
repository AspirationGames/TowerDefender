using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int towerCost;
    [SerializeField] float buildDelay = 6f;

    Bank bank;

    private void Awake() 
    {
        StartCoroutine(BuildTower());
    }
    public bool PlaceTower(Vector3 tilePosition)
    {
        bank = FindObjectOfType<Bank>(); 

        
        if(bank.CurrentBalance < towerCost)
        {
            Debug.Log("not enough gold");
            return false;
        }

        Instantiate(gameObject, tilePosition, Quaternion.identity);
        bank.DecreaseBalance(towerCost);
        return true;
    }

    IEnumerator BuildTower()
    {
        foreach(Transform child in gameObject.transform)
        {
            Transform grandChild = child.GetComponentInChildren<Transform>();
            if(grandChild != null)
            {
                Debug.Log("setting grandchild to inactive");
                grandChild.gameObject.SetActive(false);
            }

            child.gameObject.SetActive(false);
        
        }
        foreach(Transform child in gameObject.transform)
        {
            yield return new WaitForSecondsRealtime(buildDelay);
            child.gameObject.SetActive(true);
            
            Transform grandChild = child.GetComponentInChildren<Transform>();
            if(grandChild != null)
            {
                yield return new WaitForSecondsRealtime(buildDelay);
                grandChild.gameObject.SetActive(true);
            }
        }

    }
}
