using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
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

        GameObject randomEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        EnemyParticle enemy = Instantiate(randomEnemy, spawnPosition, Quaternion.identity)
            .GetComponent<EnemyParticle>();

        enemy.speed *= enemySpeedMultiplier;
    }
}
