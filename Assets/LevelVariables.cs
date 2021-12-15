using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelVariables : MonoBehaviour
{
    public static LevelVariables instance;
    
    public Transform ShipFrontPoint;
    public Transform ShipRightPoint;
    public Transform ShipLeftPoint;

    public int enemiesInLevel = 0;
    [HideInInspector]public List<Enemy> enemies = new List<Enemy>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        enemies = FindObjectsOfType<Enemy>().ToList();
        enemiesInLevel = FindObjectsOfType<Enemy>().ToList().Count;
    }

    public void CheckWin()
    {
        if (enemiesInLevel == 0)
        {
            GameManager.Instance.EndGame(true);
            //LevelManager.Instance.CreateCurrentLevel(true);
        }
    }
}
