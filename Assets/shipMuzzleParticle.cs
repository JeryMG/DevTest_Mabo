using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipMuzzleParticle : MonoBehaviour
{
    public float lifetime = 0.5f;
    private void OnEnable()
    {
        GetComponent<ParticleSystem>().Play();
        Invoke("ReleaseToPool", lifetime);
    }

    private void ReleaseToPool()
    {
        Spawner.instance._particlesPool.Release(this);
    }
}
