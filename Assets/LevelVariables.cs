using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelVariables : MonoBehaviour
{
    public Transform ShipFrontPoint;
    public Transform ShipRightPoint;
    public Transform ShipLeftPoint;

    public int enemiesInLevel = 0;

    private void Start()
    {
        enemiesInLevel = FindObjectsOfType<Enemy>().ToList().Count;
        GameManager.OnGameEnd += CheckWin;
    }

    private void CheckWin(bool b)
    {
        if (b)
        {
            if (enemiesInLevel == 0)
            {
                GameManager.Instance.EndGame(true);
                LevelManager.Instance.CreateCurrentLevel(true);
            }
        }
    }

    private void OnDestroy()
    {
        GameManager.OnGameEnd -= CheckWin;
    }
}
