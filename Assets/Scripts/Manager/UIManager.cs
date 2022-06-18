using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Text _reloadText, _ammoText, _weaponText;
    [SerializeField] GameObject successPanel, failPanel;
    [SerializeField] Sprite[] healthImgs;
    [SerializeField] private Image _currentHealthImg;
    [SerializeField] private Text _currentWaveText, _numberAliveText;

    private WaitForSeconds reloadFlashTime = new WaitForSeconds(0.25f);
    private bool _isLowHealth = false;

    public void UpdateAmmoCount(int currentAmmo, int maxAmmo, bool isEmpty = false)
    {
        _ammoText.text = currentAmmo + "/" + maxAmmo;
        if(isEmpty)
        {
            StartCoroutine(AmmoOutRoutine());
        }
    }

    void Start()
    {
        Weapon.Instance.OnPlayerReload += Reloading;
        successPanel.gameObject.SetActive(false);
        failPanel.gameObject.SetActive(false);
        _currentHealthImg.sprite = healthImgs[0]; //green
    }

    public void UpdatePlayerHealth(int health)
    {
        float currentHealth = (float)health / 100;
        switch(currentHealth)
        {
            case > .75f:
                _currentHealthImg.sprite = healthImgs[0];
                break;
            case > .25f:
                _currentHealthImg.sprite = healthImgs[1];
                break;
            case >.15f:
                _currentHealthImg.sprite = healthImgs[2];
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

    public void UpdateEnemyCount(uint numberOfEnemiesAlive)
    {
        //this needs to change, not suitable for calling on each zombie death
        _numberAliveText.text = "Zombies: " + numberOfEnemiesAlive.ToString();
    }

    public void UpdateWave(int waveIndex)
    {
        _currentWaveText.text = "Wave: " + waveIndex.ToString();
    }

    public void ChangeWeapon(string weaponName, bool isMelee)
    {
        _weaponText.text = weaponName;
        if(!isMelee)
        {
            _ammoText.gameObject.SetActive(true);
        }
        else
        {
            _ammoText.gameObject.SetActive(false);
        }
    }

    private void GameLost()
    {
        failPanel.gameObject.SetActive(true);
    }

    public void GameWon()
    {
        successPanel.gameObject.SetActive(true);
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
            yield return reloadFlashTime;
            _reloadText.gameObject.SetActive(false);
            yield return reloadFlashTime;
        }
    }

    IEnumerator AmmoOutRoutine()
    {
        for(int i=0; i<4; i++)
        {
            _ammoText.gameObject.SetActive(false);
            yield return reloadFlashTime;
            _ammoText.gameObject.SetActive(true);
            yield return reloadFlashTime;
        }
    }

    IEnumerator HealthLowRoutine()
    {
        while(true)
        {
            //health up power up mechanic need to be added here.
            _currentHealthImg.gameObject.SetActive(false);
            yield return reloadFlashTime;
            _currentHealthImg.gameObject.SetActive(true);
            yield return reloadFlashTime;
        }
    }

    private void OnEnable()
    {
        Weapon.Instance.OnPlayerReload += Reloading;
        PlayerController.Instance.OnPlayerDeath += GameLost;
    }

    private void OnDisable()
    {
        Weapon.Instance.OnPlayerReload -= Reloading;
        PlayerController.Instance.OnPlayerDeath -= GameLost;
    }
}
