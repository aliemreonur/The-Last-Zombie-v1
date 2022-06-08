using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGameStart()
    {
        IsRunning = true;
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
}
