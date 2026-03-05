using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float spawnRate = 2f;
    public float spawnRadius = 8f;
    public float enemySpeedMultiplier = 1f;

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
                float randomWait = Random.Range(spawnRate * 0.8f, spawnRate * 12f);
                yield return new WaitForSeconds(randomWait);
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
