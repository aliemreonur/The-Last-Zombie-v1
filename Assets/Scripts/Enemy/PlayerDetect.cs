using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetect : MonoBehaviour
{
    private Zombie _zombie;

    void Start()
    {
        _zombie = GetComponentInParent<Zombie>();
        if(_zombie == null)
        {
            Debug.LogError("The zombie detector could not get the zombie script");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _zombie.IsAlerted = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _zombie.IsAlerted = false;
        }
    }
}
