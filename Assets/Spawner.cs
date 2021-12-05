using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;
    
    private LevelManager _levelManager;
    public ObjectPool<ShipProjectile> _pool;
    public ShipProjectile _projectilePrefab;

    private void Awake()
    {
        instance = this;
        
        _levelManager = GetComponent<LevelManager>();
    }
    
    void Start()
    {
        _pool = new ObjectPool<ShipProjectile>(
            CreateProjectile, 
            OnTakeFromPool,
            OnReturnToPool,
            OnDestroy, 
            false,
            20,40 );
    }

    private void OnGameStart()
    {
        
    }

    private ShipProjectile CreateProjectile()
    {
        var projectile = Instantiate(_projectilePrefab, transform);
        return projectile;
    }

    private void OnTakeFromPool(ShipProjectile projectile)
    {
        projectile.transform.position = _levelManager.levelVariables.ShipFrontPoint.position;
        projectile.gameObject.SetActive(true);
    }

    private void OnReturnToPool(ShipProjectile projectile)
    {
        projectile.gameObject.SetActive(false);
    }

    private void OnDestroy(ShipProjectile projectile)
    {
        Destroy(projectile);
    }
}
