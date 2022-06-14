using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] private Zombie _zombiePrefab;
    [SerializeField] private int numberToSpawn = 50;
    [SerializeField] Transform spawnPositionStartRight, spawnPositionEndLeft; //THESE VARIABLES ARE NOT SUITABLE FOR AUTOMATED LEVEL GENERATION! NEEDS MANUAL ADJUSTMENT

    public float posXRightEdge, posXLeftEdge, posZStart, posZEnd;
    //CHANGE THE UNDERLINE FOR PUBLIC

    /// <summary>
    /// if Auto spawn to be created, I must make sure that obstacles and zombies do not spawn on each other.
    /// 
    /// </summary>

    void Start()
    {
        GetSpawnLimitPoints();
        Spawn();
        //multiply the number to spawn according to difficulty level in options.
    }

    private void GetSpawnLimitPoints()
    {
        posXLeftEdge = spawnPositionEndLeft.position.x;
        posXRightEdge = spawnPositionStartRight.position.x;
        posZStart = spawnPositionStartRight.position.z;
        posZEnd = spawnPositionEndLeft.position.z;
        //random x: is between start right and end left
        //random z: 
    }

    private void Spawn()
    {
        for(int i =0; i<numberToSpawn; i++)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(posXLeftEdge, posXRightEdge), 0.1f, Random.Range(posZStart, posZEnd));
            Instantiate(_zombiePrefab, posToSpawn, Quaternion.Euler(Random.Range(0,360), Random.Range(0,360), Random.Range(0,360)), transform);
        }
    }
}
