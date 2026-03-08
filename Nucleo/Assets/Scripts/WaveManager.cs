using UnityEngine;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public EnemySpawner spawner;
    public UpgradeManager upgradeManager;
    public TextMeshProUGUI waveText;

    public int currentWave = 1;
    public float waveDuration = 20f;
    public float timeBetweenWaves = 5f;

    private float timer;
    private bool isWaveActive = true;
    private bool isChoosingUpgrade = false;

    void Start()
    {
        StartWave();
    }

    void Update()
    {
        if (isWaveActive)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                EndWave();
            }
            return;
        }

        if (isChoosingUpgrade)
        {
            if (upgradeManager == null || upgradeManager.upgradePanel == null || !upgradeManager.upgradePanel.activeSelf)
            {
                StartWave();
                isChoosingUpgrade = false;
            }
            return;
        }

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if (upgradeManager != null)
            {
                upgradeManager.ShowUpgrades();
                isChoosingUpgrade = true;
            }
            else
            {
                StartWave();
            }
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