using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Text _reloadText, _ammoText, _weaponText;
    [SerializeField] private GameObject _successPanel, _failPanel, _welcomePanel;
    [SerializeField] private Sprite[] _healthImgs;
    [SerializeField] private Image _currentHealthImg;
    [SerializeField] private Text _currentWaveText, _numberAliveText;

    private WaitForSeconds _reloadFlashTime = new WaitForSeconds(0.25f);
    private bool _isLowHealth = false;

    #region GameStart
    private void OnEnable()
    {
        WeaponController.Instance.OnPlayerReload += Reloading;
    }

    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _successPanel.gameObject.SetActive(false);
        _failPanel.gameObject.SetActive(false);
        _currentHealthImg.sprite = _healthImgs[0]; //green
        CheckFirstTime();
    }

    public void PassWelcomeScreen()
    {
        _welcomePanel.gameObject.SetActive(false);
        PlayerPrefs.SetInt("FirstTime5", 0);
        PlayerPrefs.Save();
        GameManager.Instance.OnGameStart();
    }

    private void CheckFirstTime()
    {
        if (PlayerPrefs.HasKey("FirstTime5"))
        {
            int firstTime = PlayerPrefs.GetInt("FirstTime5");
            if (firstTime == 0) // meaning that player has started the game before
            {
                _welcomePanel.gameObject.SetActive(false);
                GameManager.Instance.OnGameStart();
            }
        }
        else
        {
            _welcomePanel.gameObject.SetActive(true);
        }
    }
    #endregion

    #region ActiveGame
    public void UpdatePlayerHealth(int health)
    {
        float currentHealth = (float)health / 100;
        switch(currentHealth)
        {
            case > .75f:
                _currentHealthImg.sprite = _healthImgs[0];
                break;
            case > .25f:
                _currentHealthImg.sprite = _healthImgs[1];
                break;
            case >.15f:
                _currentHealthImg.sprite = _healthImgs[2];
                break;
            default:
                if(!_isLowHealth)
                {
                    StartCoroutine(HealthLowRoutine());
                    _isLowHealth = true;
                }
                break;
        }
        _currentHealthImg.fillAmount = (float)health/100;
    }

    public void UpdateEnemyCount(int numberOfEnemiesAlive)
    {
        _numberAliveText.text = "Zombies: " + numberOfEnemiesAlive.ToString();
    }

    public void UpdateWave(int waveIndex)
    {
        _currentWaveText.text = "Wave: " + waveIndex.ToString();
    }

    public void ChangeWeapon(string weaponName, bool isMelee)
    {
        _weaponText.text = weaponName;
        bool isAmmoOn = !isMelee;
        _ammoText.gameObject.SetActive(isAmmoOn);
    }

    public void UpdateAmmoCount(int currentAmmo, int maxAmmo, bool isEmpty = false)
    {
        _ammoText.text = currentAmmo + "/" + maxAmmo;
        if (isEmpty)
        {
            StartCoroutine(AmmoOutRoutine());
        }
    }
    private void Reloading()
    {
        StartCoroutine(ReloadRoutine());
    }

    IEnumerator ReloadRoutine()
    {
        for(int i=0; i<4; i++)
        {
            _reloadText.gameObject.SetActive(true);
            yield return _reloadFlashTime;
            _reloadText.gameObject.SetActive(false);
            yield return _reloadFlashTime;
        }
    }

    IEnumerator AmmoOutRoutine()
    {
        for(int i=0; i<4; i++)
        {
            _ammoText.gameObject.SetActive(false);
            yield return _reloadFlashTime;
            _ammoText.gameObject.SetActive(true);
            yield return _reloadFlashTime;
        }
    }

    IEnumerator HealthLowRoutine()
    {
        while(true)
        {
            //health up power up mechanic need to be added here.
            _currentHealthImg.gameObject.SetActive(false);
            yield return _reloadFlashTime;
            _currentHealthImg.gameObject.SetActive(true);
            yield return _reloadFlashTime;
        }
    }
    #endregion

    #region GameEnd
    public void GameWon()
    {
        _successPanel.gameObject.SetActive(true);
    }

    public void GameLost()
    {
        _failPanel.gameObject.SetActive(true);
    }


    private void OnDisable()
    {
        WeaponController.Instance.OnPlayerReload -= Reloading;
    }

    #endregion
}
