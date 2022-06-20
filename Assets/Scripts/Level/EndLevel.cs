using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EndLevel : MonoBehaviour
{
    public static Action OnZombieReachedEndPoint;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Zombie"))
        {
            other.gameObject.SetActive(false);
            OnZombieReachedEndPoint?.Invoke();
        }
    }
}
