using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : Singleton<MainMenu>
{

    public void PlayGame()
    {
        SceneManager.LoadScene(2);
    }

    public void Options()
    {
        SceneManager.LoadScene(1);
    }
}
