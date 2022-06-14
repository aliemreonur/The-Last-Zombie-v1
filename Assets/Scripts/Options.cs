using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField] private Text _difficultyText;
    private int _difficulty;
    private string[] _difficultyLevels = { "Easy", "Medium", "Hard" };

    private void Start()
    {
        if (PlayerPrefs.HasKey("difficulty"))
        {
            _difficulty = PlayerPrefs.GetInt("difficulty");
        }
        else
        {
            _difficulty = 1; //medium
        }
        _difficultyText.text = _difficultyLevels[_difficulty];
    }

    public void ChangeDifficulty(bool isHarder)
    {
        
        if(isHarder)
        {
            _difficulty++;
        }
        else
        {
            _difficulty--;
        }
        int _clampedDifficulty = Mathf.Clamp(_difficulty, 0, _difficultyLevels.Length - 1);
        _difficulty = _clampedDifficulty;
        _difficultyText.text = _difficultyLevels[_difficulty];
    }

    public void SaveDifficulty()
    {
        PlayerPrefs.SetInt("difficulty", _difficulty);
        PlayerPrefs.Save();
        SceneManager.LoadScene(0);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
