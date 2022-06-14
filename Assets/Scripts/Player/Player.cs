using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : Singleton<Player>
{
    [SerializeField] int _playerHealth = 100;
    public Action OnPlayerDeath;

    public void Damage(int damageAmount)
    {
        _playerHealth -= damageAmount;
        UIManager.Instance.UpdatePlayerHealth(_playerHealth);
        if (_playerHealth <= 0)
        {
            OnPlayerDeath?.Invoke();
        }
    }


}
