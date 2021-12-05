using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	#region Members
	public static LevelManager Instance;

	public LevelSettings levelSettings;

	public Transform gameContainer;
	
	#endregion

	#region MonoBehaviors
	void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		GameManager.OnGameStart += OnGameStart;
		GameManager.OnGameEnd += OnGameEnd;
		GameManager.OnGameReset += CreateCurrentLevel;

		CreateCurrentLevel(true);
	}
	#endregion

	protected void OnGameStart()
	{

	}

	public void CreateCurrentLevel(bool isVictory)
	{
		int _currentLevel = levelSettings.currentLevel;
		DestroyPastLevel();

		Instantiate(levelSettings.GetLevel(), gameContainer);

		GameManager.Instance.LevelCreated();
	}

	public void ProgressToNextLevel()
	{
		levelSettings.currentLevel++ ;
	}

	protected void OnGameEnd(bool isVictory)
	{
		if (isVictory)
			ProgressToNextLevel();
	}

	protected void DestroyPastLevel()
	{
		for (int i = gameContainer.childCount - 1; i >= 0; i--)
		{
			DestroyImmediate(gameContainer.GetChild(i).gameObject);
		}
	}
}

