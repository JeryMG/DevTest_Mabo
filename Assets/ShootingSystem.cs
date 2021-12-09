using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSystem : MonoBehaviour
{
    private float currentState;
    public List<float> rateOfFire;
    private float timer;

    public bool isShooting;

    private void Update()
    {
        if (isShooting)
        {
            timer += Time.deltaTime;
            if (timer >= rateOfFire[0])
            {
                Shoot();
                timer = 0;
            }
        }
    }

    private void OnGameStart()
    {
        isShooting = true;
    }

    void OnGameEnd(bool isVictory)
    {
        isShooting = false;
    }

    void Shoot()
    {
        Spawner.instance._particlesPool.Get();
        Spawner.instance._pool.Get();
    }

    private void OnDestroy()
    {
        GameManager.OnGameStart -= OnGameStart;
        GameManager.OnGameEnd -= OnGameEnd;
    }
}
