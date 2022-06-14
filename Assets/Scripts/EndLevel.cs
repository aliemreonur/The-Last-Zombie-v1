using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EndLevel : MonoBehaviour
{
    public static Action OnSuccess;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            UIManager.Instance.EndGame(true);
            OnSuccess?.Invoke();
            //Will update the UI Manager to load the necessary screen.
        }
    }
}
