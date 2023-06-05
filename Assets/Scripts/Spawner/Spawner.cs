using Assets.Scripts.SaveLoad;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using Random = UnityEngine.Random;

public enum SpawnModes
{
    Fixed,
    Random
}

public class Spawner : MonoBehaviour
{
    public static Action OnWaveCompleted;
    
    [Header("Settings")]
    [SerializeField] private SpawnModes spawnMode = SpawnModes.Fixed;
    [SerializeField] private int enemyCount = 10;
    [SerializeField] private float delayBtwWaves = 1f;

    [Header("Fixed Delay")]
    [SerializeField] private float delayBtwSpawns;
    
    [Header("Random Delay")]
    [SerializeField] private float minRandomDelay;
    [SerializeField] private float maxRandomDelay;

    [Header("Poolers")] 
    [SerializeField] private ObjectPooler enemyWave10Pooler;
    [SerializeField] private ObjectPooler enemyWave11To20Pooler;
    [SerializeField] private ObjectPooler enemyWave21To30Pooler;
    [SerializeField] private ObjectPooler enemyWave31To40Pooler;
    [SerializeField] private ObjectPooler enemyWave41To50Pooler;

    public int EnemySpawned() { return _enemiesSpawned; }
    public int EnemyRemain() { return _enemiesRamaining; }

    private float _spawnTimer;
    private int _enemiesSpawned;
    private int _enemiesRamaining;
    
    private Waypoint _waypoint;

    private void Start()
    {
        _waypoint = GetComponent<Waypoint>();

        _enemiesRamaining = enemyCount;

        if (PropertiesApplication.RequestLoadEnemy)
        {
            foreach(var enemy in PropertiesApplication.enemyProperties)
            {
                GameObject newInstance = GetPooler().GetInstanceFromPool();
                Enemy enemy1 = newInstance.GetComponent<Enemy>();
                enemy1.ResetEnemy();
                EnemyHealth health = enemy1.GetComponent<EnemyHealth>();

                health.InitFileHelCur = enemy.healthCur;
                enemy1.InitIndex = enemy.CurrentWaypointIndex;
                enemy1.Waypoint = _waypoint;

                enemy1.transform.localPosition = transform.position;

                newInstance.transform.position = new Vector2(enemy.posx, enemy.posy);
                newInstance.SetActive(true);

                _enemiesSpawned = PropertiesApplication.enemiesSpawned;
                _enemiesRamaining = PropertiesApplication.enemiesRemain;

                _enemiesSpawned++;
            }
            PropertiesApplication.RequestLoadEnemy = false;
        }
    }

    private void Update()
    {
        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer < 0)
        {
            _spawnTimer = GetSpawnDelay();
            if (_enemiesSpawned < enemyCount)
            {
                _enemiesSpawned++;
                SpawnEnemy();
            }
        }
    }

    private void SpawnEnemy()
    {
        GameObject newInstance = GetPooler().GetInstanceFromPool();
        Enemy enemy = newInstance.GetComponent<Enemy>();
        enemy.ResetEnemy();
        enemy.Waypoint = _waypoint;

        enemy.transform.localPosition = transform.position;
        newInstance.SetActive(true);
    }

    private float GetSpawnDelay()
    {
        float delay = 0f;
        if (spawnMode == SpawnModes.Fixed)
        {
            delay = delayBtwSpawns;
        }
        else
        {
            delay = GetRandomDelay();
        }

        return delay;
    }
    
    private float GetRandomDelay()
    {
        float randomTimer = Random.Range(minRandomDelay, maxRandomDelay);
        return randomTimer;
    }

    private ObjectPooler GetPooler()
    {
        int currentWave = LevelManager.Instance.CurrentWave;
        if (currentWave <= 10) // 1- 10
        {
            return enemyWave10Pooler;
        }

        if (currentWave > 10 && currentWave <= 20) // 11- 20
        {
            return enemyWave11To20Pooler;
        }
        
        if (currentWave > 20 && currentWave <= 30) // 21- 30
        {
            return enemyWave21To30Pooler;
        }
        
        if (currentWave > 30 && currentWave <= 40) // 31- 40
        {
            return enemyWave31To40Pooler;
        }
        
        if (currentWave > 40 && currentWave <= 50) // 41- 50
        {
            return enemyWave41To50Pooler;
        }

        return null;
    }
    
    private IEnumerator NextWave()
    {
        yield return new WaitForSeconds(delayBtwWaves);
        _enemiesRamaining = enemyCount;
        _spawnTimer = 0f;
        _enemiesSpawned = 0;
    }
    
    private void RecordEnemy(Enemy enemy)
    {
        _enemiesRamaining--;
        if (_enemiesRamaining <= 0)
        {
            OnWaveCompleted?.Invoke();
            StartCoroutine(NextWave());
        }
    }
    
    private void OnEnable()
    {
        Enemy.OnEndReached += RecordEnemy;
        EnemyHealth.OnEnemyKilled += RecordEnemy;
    }

    private void OnDisable()
    {
        Enemy.OnEndReached -= RecordEnemy;
        EnemyHealth.OnEnemyKilled -= RecordEnemy;
    }
}
