using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newWave.asset", menuName = "Scriptable Objects/New Wave")]
public class Wave : ScriptableObject
{
    public int numberToSpawn;
    public float spawnInterval;
    //public List<Zombie> zombiesToSpawn; //this is for adding different zombie types in the future.
    //maybe a cost variable for upgraded zombies later on
}
