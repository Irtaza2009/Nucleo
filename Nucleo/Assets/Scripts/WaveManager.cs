using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public EnemySpawner spawner;
    public TextMeshProUGUI waveText;

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

        spawner.SetSpawning(true);
        spawner.spawnRate *= 0.95f; 
        spawner.enemySpeedMultiplier += 0.1f;

        UpdateWaveUI();
        Debug.Log("Wave " + currentWave);
    }

    void EndWave()
    {
        isWaveActive = false;
        spawner.SetSpawning(false);
        
        if (waveText != null)
            waveText.text = "Wave " + currentWave + " Complete!";

        timer = timeBetweenWaves;
        currentWave++;
    }

    void UpdateWaveUI()
    {
        if (waveText != null)
            waveText.text = "Wave " + currentWave;
    }
}