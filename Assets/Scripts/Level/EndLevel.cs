using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EndLevel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Zombie"))
        {
            other.gameObject.SetActive(false);
            GameManager.Instance.EndGame(false);
        }
    }
}
