using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int goldReward = 25; //gold given for killing enemy
    [SerializeField] int goldPenalty = 25; //gold taken if they reach goal

    Bank bank;

    private void Awake() 
    {
        bank = FindObjectOfType<Bank>();    
    }

    public void RewardGold()
    {
        if(bank == null)
        {
            return;
        }
        bank.IncreaseBalance(goldReward);
    }

    public void StealGold()
    {
        if(bank == null)
        {
            return;
        }
        bank.DecreaseBalance(goldReward);
    }


}
