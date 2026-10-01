using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;

    private Transform _spawnPoint;

    [field: SerializeField] 
    private float _spawnRate;

    [field: SerializeField] 
    private float _nextSpawnTime;

    private int _enemyCount = 0;

    private void Start()
    {
        _spawnPoint = GameObject.Find("SpawnPoint").transform;
        _spawnRate = 5f;
        _nextSpawnTime = Time.time + _spawnRate;
    }

    private void Update()
    {
        if (Time.time >= _nextSpawnTime && _enemyCount < 10)
        {
            SpawnEnemy();
            _nextSpawnTime = Time.time + _spawnRate;
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(_enemyPrefab, _spawnPoint.position, _spawnPoint.rotation);
        _enemyCount++;
    }
}
