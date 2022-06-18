using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public bool IsRunning { get; private set; }
    public int numberOfZombiesToFail = 5; //this will be tied to PlayerPrefs hardness!

    void Start()
    {
        OnGameStart(); //this will be tied to a button or tap to screen!
    }

    public void OnGameStart()
    {
        IsRunning = true;
        Time.timeScale = 1;
    }

    public void ZombiePassed()
    {
        numberOfZombiesToFail--;
        if(numberOfZombiesToFail<=0)
        {
            OnGameFail();
        }
    }

    public void OnGameFail()
    {
        IsRunning = false;
        Time.timeScale = 0;
    }


    public void OnGameWon()
    {
        IsRunning = false;
        UIManager.Instance.GameWon();
        Time.timeScale = 0; 
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(2);
    }
}
