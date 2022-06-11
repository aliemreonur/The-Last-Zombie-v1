using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Zombie zombie;
    [SerializeField] private int numberToSpawn;
    [SerializeField] Transform[] spawnPositions;

    /// <summary>
    /// if Auto spawn to be created, I must make sure that obstacles and zombies do not spawn on each other.
    /// 
    /// </summary>

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
