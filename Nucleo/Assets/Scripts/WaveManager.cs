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
        
        // Award wave completion bonus
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.AddWaveBonus(currentWave);
        
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

// Todo:
// - Add wave complete UI with timer until next wave
// - Add boss waves every 5 waves with unique enemies and mechanics
// - Add 3 cards to choose from between waves that provide
//   buffs for the next wave (e.g. increased stability, health,
//   max health)