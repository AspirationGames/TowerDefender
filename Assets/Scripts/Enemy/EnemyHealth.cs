using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHP = 5;

    [Tooltip("Adds amount to max hitpoints when enemy dies")]
    [SerializeField] int difficultyBoost = 1;
    int currentHP;
    Enemy enemy;

    private void Start() 
    {
        enemy = GetComponent<Enemy>();    
    }
    private void OnEnable() 
    {   
        currentHP = maxHP;    
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        currentHP--;

        if (currentHP <= 0)
        {
            HandleEnemyDeath();
        }
    }

    void HandleEnemyDeath()
    {
        enemy.RewardGold();
        gameObject.SetActive(false);
        maxHP += difficultyBoost;
    }
}
