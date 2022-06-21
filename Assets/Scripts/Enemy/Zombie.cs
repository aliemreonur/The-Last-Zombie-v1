using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombie : MonoBehaviour
{
    [SerializeField] private int _zHealth = 3;
    [SerializeField] private bool _isHit;
    [SerializeField] private Image _healthBg;
    [SerializeField] private Image _healthBar;

    private bool _isAlerted, _isAttacking;
    private float _distanceToPlayer;
    private float _maxZHealth;
    private WaitForSeconds hitResetTime = new WaitForSeconds(0.3f);
    private WaitForSeconds healthBarResetTime = new WaitForSeconds(1.5f);


    #region Properties

    public bool IsHit
    {
        get
        {
            return _isHit;
        }
        set
        {
            _isHit = value;
        }
    }

    public bool IsAlive => _zHealth>0;

    public bool IsAlerted
    {
        get
        {
            return _isAlerted;
        }
        set
        {
            _isAlerted = value;
        }
    }

    public bool IsAttacking
    {
        get
        {
            return _isAttacking;
        }
        set
        {
            _isAttacking = value;
        }
    }

    #endregion

    private void Start()
    {
        HealthBarActive(false);
        _distanceToPlayer = 50;
        _maxZHealth = _zHealth;
    }

    public void Damage(bool isMelee = false)
    {
        if (_zHealth > 0)
        {
            if (isMelee && !_isHit)
            {
                WeaponController.Instance.MeleeHit();
            }
            _isHit = true;
            StartCoroutine(ResetHitRoutine());
            HealthBarActive(true);
            _zHealth--;
            _healthBar.fillAmount = _zHealth / _maxZHealth;
            StartCoroutine(ResetHealthBarRoutine());
            if (_zHealth <= 0)
            {
                WaveManager.Instance.currentAlive--;
                UIManager.Instance.UpdateEnemyCount(WaveManager.Instance.currentAlive); 
                Invoke("DisableZombie", 3f);
            }
        }
    }

    public float DistanceToPlayer()
    {
        if (Time.frameCount % 10 == 0)
        {
            _distanceToPlayer = (transform.position - PlayerController.Instance.transform.position).sqrMagnitude;
        }
        return _distanceToPlayer;
    }

    private void HealthBarActive(bool isActive)
    {
        _healthBg.gameObject.SetActive(isActive);
        _healthBar.gameObject.SetActive(isActive);
    }

    private void DisableZombie()
    {
        gameObject.SetActive(false);
    }

    IEnumerator ResetHitRoutine()
    {
        if (_isHit)
        {
            yield return hitResetTime;
            _isHit = false;
        }
    }

    IEnumerator ResetHealthBarRoutine()
    {
        yield return healthBarResetTime;
        HealthBarActive(false);
    }

}
