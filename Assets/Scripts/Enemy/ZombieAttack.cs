using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
           
            Player.Instance.Damage(5);
            //this damage amount will be changed according to the difficulty level!
        }
    }
}
