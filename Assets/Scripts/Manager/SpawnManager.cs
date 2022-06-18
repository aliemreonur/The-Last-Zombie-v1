using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] private List<Wave> _waves = new List<Wave>();
    [SerializeField] private Zombie _zombiePrefab;
    [SerializeField] Transform spawnPositionStartRight, spawnPositionEndLeft; //THESE VARIABLES ARE NOT SUITABLE FOR AUTOMATED LEVEL GENERATION! NEEDS MANUAL ADJUSTMENT

    public Action OnGameWon;

    public float posXRightEdge, posXLeftEdge, posZStart, posZEnd;
    public uint currentAlive;

    private uint _numberSpawned;
    private int numberToSpawn;
    private int _currentWaveIndex;
    private WaitForSeconds timeBtwWaves = new WaitForSeconds(5f);
    private bool _wavesFinished;

    void Start()
    {
        GetSpawnLimitPoints(); //this will be depreciated
        StartCoroutine(WaveRoutine());
        //multiply the number to spawn according to difficulty level in options.
    }

    private void GetSpawnLimitPoints()
    {
        posXLeftEdge = spawnPositionEndLeft.position.x;
        posXRightEdge = spawnPositionStartRight.position.x;
        posZStart = spawnPositionStartRight.position.z;
        posZEnd = spawnPositionEndLeft.position.z;
    }

    IEnumerator WaveRoutine()
    {
        while(PlayerController.Instance.IsAlive && !_wavesFinished) //+the number of zombies to spawn is not over
        {
            Wave currentWave = _waves[_currentWaveIndex];
            UIManager.Instance.UpdateWave(_currentWaveIndex +1);
            numberToSpawn = currentWave.numberToSpawn;
            WaitForSeconds timeBtwSpawns = new WaitForSeconds(currentWave.spawnInterval);

            if(_numberSpawned != numberToSpawn)
            {
                for (int i = 0; i < numberToSpawn; i++)
                {
                    _numberSpawned++;
                    Vector3 posToSpawn = new Vector3(UnityEngine.Random.Range(posXLeftEdge, posXRightEdge), 0.1f, posZEnd);
                    Zombie _spawnedZombie = Instantiate(_zombiePrefab, posToSpawn, Quaternion.Euler(0, 180, 0), transform); //better use the obj pool
                    currentAlive++;
                    yield return timeBtwSpawns;
                    UIManager.Instance.UpdateEnemyCount(currentAlive);
                }
            }

            if(currentAlive == 0)
            {
                if(_currentWaveIndex != _waves.Count - 1)
                {
                    yield return timeBtwWaves;
                    numberToSpawn = 0;
                    _numberSpawned = 0;
                    _currentWaveIndex++;
                }
                else
                {
                    GameManager.Instance.OnGameWon();
                }

            }
            yield return new WaitForSeconds(1f);
        }

    }
}
