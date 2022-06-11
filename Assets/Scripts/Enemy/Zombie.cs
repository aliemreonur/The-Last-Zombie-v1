using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombie : MonoBehaviour
{
    [SerializeField] GameObject _bloodPrefab;
    [SerializeField] private int _zHealth = 3;
    [SerializeField] private bool _isHit;
    [SerializeField] private Image _healthBg;
    [SerializeField] private Image _healthBar;

    //private float _zSpeed;
    private bool _isAlerted, _isAttacking;
    private float _distanceToPlayer;
    private WaitForSeconds hitResetTime = new WaitForSeconds(0.5f);
    private WaitForSeconds healthBarResetTime = new WaitForSeconds(1.5f);
    private float _maxZHealth;


    #region Properties
    public int ZHealth
    {
        get
        {
            return _zHealth;
        }
    }

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

    public bool IsAlive
    {
        get
        {
            return _zHealth > 0;
        }
    }


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

    public void Damage()
    {
        _isHit = true;
        HealthBarActive(true);
        _zHealth--;
        _healthBar.fillAmount = _zHealth/_maxZHealth;
        StartCoroutine(ResetHitRoutine());
        StartCoroutine(ResetHealthBarRoutine());
        if (_zHealth <= 0)
        {
            Destroy(this.gameObject, 3f);
        }
    }

    public float DistanceToPlayer()
    {
        if (Time.frameCount % 4 == 0)
        {
            _distanceToPlayer = Vector3.Distance(this.transform.position, PlayerController.Instance.transform.position);
        }
        return _distanceToPlayer;
    }


    private void HealthBarActive(bool isActive)
    {
        _healthBg.gameObject.SetActive(isActive);
        _healthBar.gameObject.SetActive(isActive);
    }

    IEnumerator ResetHitRoutine()
    {
        if(_isHit)
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
