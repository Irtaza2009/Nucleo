using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float minSpawnTime = 1f;
    public float maxSpawnTime = 3f;
    public float spawnRadius = 8f;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            float randomWait = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(randomWait);
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPosition = Random.insideUnitCircle.normalized * spawnRadius;
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
