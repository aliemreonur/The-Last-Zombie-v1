using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    //public bool IsRunning { get; private set; }

    private void OnEnable()
    {
        PlayerController.Instance.OnPlayerDeath += OnGameFail;
        Time.timeScale = 0;
    }

    public void OnGameStart()
    {
        Time.timeScale = 1;
    }

    public void OnGameFail()
    {
        UIManager.Instance.GameLost();
    }


    public void OnGameWon()
    {
        UIManager.Instance.GameWon();
        Time.timeScale = 0; 
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(2);
    }

    
}
