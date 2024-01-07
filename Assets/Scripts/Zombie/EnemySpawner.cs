using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING }

    [SerializeField] private Wave[] waves;
    [SerializeField] private float timeBetweenWaves = 3f;
    [SerializeField] private float waveCountdown = 0;

    private SpawnState state = SpawnState.COUNTING;
    [SerializeField] private Transform[] spawners;
    [SerializeField] private List<ZombieStats> enemyList;

    private int currentWave;

    private void Start()
    {
        waveCountdown = timeBetweenWaves;
        currentWave = 0;
    }
    private void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if (!EnemiesAreDead())
            {
                return;
            }
            else
            {
                CompleteWave();
            }
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[currentWave]));
            }

        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }
    private IEnumerator SpawnWave(Wave wave)
    {
        state = SpawnState.SPAWNING;

        for (int i = 0; i < wave.enemiesAmount; i++)
        {
            SpawnZombie(wave.enemy);
            yield return new WaitForSeconds(wave.delay);
        }


        state = SpawnState.WAITING;

        yield break;
    }

    private void SpawnZombie(GameObject enemy)
    {
        int randomInt = Random.Range(0, spawners.Length);
        Transform randomSpawner = spawners[randomInt];
        GameObject newEnemy = Instantiate(enemy, randomSpawner.position, randomSpawner.rotation);
        ZombieStats newEnemyStats = newEnemy.GetComponent<ZombieStats>();

        enemyList.Add(newEnemyStats);
    }

    private bool EnemiesAreDead()
    {
        int i = 0;
        foreach (ZombieStats enemy in enemyList)
        {
            if (enemy.IsDead())
                i++;
            else
            {
                return false;
            }
        }
        return true;
    }
    private void CompleteWave()
    {
        Debug.Log("Wave Completed");

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (currentWave + 1 > waves.Length - 1)
        {
            currentWave = 0;
            Debug.Log("Completed all the waves");
        }
        else
        {
            currentWave++;
        }

    }
}

