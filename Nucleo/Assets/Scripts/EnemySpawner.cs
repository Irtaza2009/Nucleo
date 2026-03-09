using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float spawnRate = 2f;
    public float spawnRadius = 8f;
    public float enemySpeedMultiplier = 1f;

    public float randomness = 0.2f;

    private bool isSpawning = true;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    public void SetSpawning(bool spawn)
    {
        isSpawning = spawn;
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (isSpawning)
            {
                float randomWait = Random.Range(spawnRate * Mathf.Clamp(1 - randomness, 0, 1), spawnRate * (1 + randomness));
                yield return new WaitForSeconds(randomWait);
                if (!isSpawning)
                    continue;
                SpawnEnemy();
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }
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
