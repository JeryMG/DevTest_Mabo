using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalShootAbilities : ShipProjectile
{
    public float range = 10f;
    private Transform target;
    public Transform firePoint;

    public LineRenderer lr;

    private void Start()
    {
        InvokeRepeating("UpdateTarget",0f,0.5f);
    }

    private void Update()
    {
        if (target == null) return;
        
    }

    void LockOnTarget()
    {
        
    }

    void UpdateTarget()
    {
        float shortestDistance = Mathf.Infinity;
        Enemy closest = null;
        
        for (int i = 0; i < LevelVariables.instance.enemiesInLevel; i++)
        {
            float distance = Vector3.Distance(transform.position, LevelVariables.instance.enemies[i].transform.position);
            if (distance <= shortestDistance)
            {
                shortestDistance = distance;
                closest = LevelVariables.instance.enemies[i];
            }
        }

        if (closest != null && shortestDistance <= range)
        {
            target = closest.transform;
        }
    }
}
