using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
   
    [SerializeField] Transform weapon; 
    [SerializeField] float fireRange = 15f;
    [SerializeField] ParticleSystem projectileParticles;
    Transform target;

    private void Update() 
    {
        FindClosestTarget();
        AimWeapon();

    }

    void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>(); 
        Transform closestEnemy = null;
        float currentEnemyDistance = Mathf.Infinity;

        foreach(Enemy enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if(distance < currentEnemyDistance)
            {
                closestEnemy = enemy.transform;
                currentEnemyDistance = distance;
            }
        }

        target = closestEnemy;
        
    }

    void AimWeapon()
    {
        float targetDistance = Vector3.Distance(transform.position, target.position);

        weapon.LookAt(target);
        
        if(targetDistance <= fireRange)
        {
            Attack(true);
        }
        else
        {
            Attack(false);
        }
        
    }

    void Attack(bool isActive)
    {
        var particleEmission = projectileParticles.emission;
        particleEmission.enabled = isActive;
        
    }

    

}
