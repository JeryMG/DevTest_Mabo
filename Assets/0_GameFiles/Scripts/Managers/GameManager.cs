using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager Instance;

    #endregion
    
    #region Static Members

    public static event Action OnGameStart;
    public static event Action<bool> OnGameEnd;
    public static event Action<bool> OnGameReset;
    public static event Action OnLevelCreated;

    #endregion

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        
        //DontDestroyOnLoad(this);
    }
    
    #region Public Methods

    public void StartGame()
    {
        OnGameStart?.Invoke();
    }

    public void EndGame(bool success)
    {
        OnGameEnd?.Invoke(success);
    }

    public void ResetGame(bool succes)
    {
        OnGameReset?.Invoke(succes);
    }

    public void LevelCreated()
    {
        OnLevelCreated?.Invoke();
    }

    #endregion
}
