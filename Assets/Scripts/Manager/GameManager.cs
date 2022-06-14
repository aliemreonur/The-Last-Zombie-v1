using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public bool IsRunning { get; private set; }

    /// <summary>
    /// 1 - On Game Start
    /// 2- On Game End -> Player death or success with
    /// 3- 
    /// </summary>

    // Start is called before the first frame update
    void Start()
    {
        OnGameStart(); //this will be tied to a button or tap to screen!
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGameStart()
    {
        IsRunning = true;
        Time.timeScale = 1;
    }

    public void OnGameEnd(bool isSuccess)
    {
        IsRunning = false;
        if(isSuccess)
        {
            Debug.Log("next level");
        }
        else
        {
            Debug.Log("Restart Level");
        }
    }

    private void StopTime()
    {
        Time.timeScale = 0;
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(2);
    }

    private void OnEnable()
    {
        EndLevel.OnSuccess += StopTime;
    }

    private void OnDisable()
    {
        EndLevel.OnSuccess -= StopTime;
    }

}
