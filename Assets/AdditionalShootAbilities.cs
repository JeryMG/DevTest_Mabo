using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalShootAbilities : ShipProjectile
{
    public float range = 10f;
    private Transform target;
    public Transform firePoint;

    private LineRenderer lr;

    private void Start()
    {
        lr = GetComponentInChildren<LineRenderer>();
        InvokeRepeating("UpdateTarget",0f,0.25f);
    }

    private void Update()
    {
        if (target == null)
        {
            if (lr.enabled)
            {
                lr.enabled = false;
            }
            return;
        }
        
        LockOnTarget();
    }

    void LockOnTarget()
    {
        if (!lr.enabled)
        {
            lr.enabled = true;
        }
        lr.SetPosition(0, firePoint.position);
        lr.SetPosition(1, target.position);
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
        else
        {
            target = null;
        }
    }
}
