using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public EnemySpawner spawner;

    public int currentWave = 1;
    public float waveDuration = 20f;
    public float timeBetweenWaves = 5f;

    private float timer;
    private bool isWaveActive = true;

    void Start()
    {
        StartWave();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            if (isWaveActive)
                EndWave();
            else
                StartWave();
        }
    }

    void StartWave()
    {
        isWaveActive = true;
        timer = waveDuration;

        spawner.spawnRate *= 0.95f; // spawn faster (slower scaling)
        spawner.enemySpeedMultiplier += 0.1f;

        Debug.Log("Wave " + currentWave);
    }

    void EndWave()
    {
        isWaveActive = false;
        timer = timeBetweenWaves;
        currentWave++;
    }
}