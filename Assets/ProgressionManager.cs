using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionManager : MonoBehaviour
{
    public int currentShipLevel;
    public int EvolveEveryXlevels = 5;
    private int nextEvolutionLevel;

    public int ShipCurrentState = 0;

    private void Start()
    {
        nextEvolutionLevel = EvolveEveryXlevels;
    }

    public void IncreaseLevel(int Amount)
    {
        currentShipLevel += Amount;
    }

    public void CheckEvolution()
    {
        if (currentShipLevel >= nextEvolutionLevel)
        {
            //Ship evolves
            ShipCurrentState++;
        }
    }
}
