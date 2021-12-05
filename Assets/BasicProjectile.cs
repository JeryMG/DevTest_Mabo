using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : ShipProjectile
{
    private void OnEnable()
    {
        Invoke("ReleaseToPool", lifetime);
    }

    private void Update()
    {
        ForwardMovement();
    }

    private void ReleaseToPool()
    {
        Spawner.instance._pool.Release(this);
    }
}
