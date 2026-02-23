using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate = 2f;
    public float spawnRadius = 8f;
    public float enemySpeedMultiplier = 1f;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            float randomWait = Random.Range(spawnRate * 0.8f, spawnRate * 1.2f);
            yield return new WaitForSeconds(randomWait);
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPosition = Random.insideUnitCircle.normalized * spawnRadius;
        EnemyParticle enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity)
            .GetComponent<EnemyParticle>();

        enemy.speed *= enemySpeedMultiplier;
    }
}
