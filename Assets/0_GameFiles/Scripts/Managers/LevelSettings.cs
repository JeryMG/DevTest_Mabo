using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSettings", menuName = "GameSO/LevelSettings", order = 1)]
public class LevelSettings : ScriptableObject
{
	public int currentLevel = 1;
	
    [SerializeField]
	public List<GameObject> simpleLevels;

	public enum LevelType { Normal, Boss };
	
	public GameObject GetLevel()
	{
		return simpleLevels[(currentLevel - 1) % simpleLevels.Count];
	}
}
