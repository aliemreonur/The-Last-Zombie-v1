using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public bool IsRunning { get; private set; }
    public Action OnGameEnd;

    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    public void EndGame(bool isSuccess)
    {
        IsRunning = false;
        OnGameEnd?.Invoke();
        if (isSuccess)
            UIManager.Instance.GameWon();
        else
            UIManager.Instance.GameLost();
    }

    public void OnGameStart()
    {
        IsRunning = true;
        Time.timeScale = 1;
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(2);
    }
    
}
