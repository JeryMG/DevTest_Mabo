using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SaveObject", menuName = "GameSO/SaveSettings", order = 1)]

public class SaveVariables : ScriptableObject
{
    public int currentGameLv;
    
    [Header("Ship Variables :")]
    public int shipLevel = 1;
    public float currentExp = 0;
    public float expToNextLv = 0;
}
