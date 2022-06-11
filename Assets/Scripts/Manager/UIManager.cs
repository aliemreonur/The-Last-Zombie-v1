using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Text _reloadText, _ammoText;

    private WaitForSeconds reloadFlashTime = new WaitForSeconds(0.25f);
    private float _flashTime;

    public void UpdateAmmoCount(int currentAmmo, int maxAmmo, bool isEmpty = false)
    {
        _ammoText.text = currentAmmo + "/" + maxAmmo;
        if(isEmpty)
        {
            StartCoroutine(AmmoOutRoutine());
        }
    }

    /// <summary>
    /// TO be added:
    /// 1- Player Health bar
    /// 2- Number of bullets
    /// 3- Reload bar
    /// 4- Current weapon
    /// 
    /// </summary>
    // Start is called before the first frame update
    void Start()
    {
        PlayerController.Instance.OnPlayerReload += Reloading;
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

    private void OnDisable()
    {
        PlayerController.Instance.OnPlayerReload -= Reloading;
    }
}
