using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerCore : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public TextMeshProUGUI healthText;

    [Header("Energy")]
    public float maxEnergy = 100f;
    public float energyRegen = 10f;
    private float currentEnergy;
    public TextMeshProUGUI energyText;
    public float CurrentEnergy => currentEnergy;

    private Color originalColor;

    void Start()
    {
        currentHealth = maxHealth;
        currentEnergy = maxEnergy;
        originalColor = GetComponent<SpriteRenderer>().color;
        UpdateEnergyUI();
        UpdateHealthUI();
    }

    void Update()
    {
        RegenerateEnergy();
    }

    void RegenerateEnergy()
    {
        currentEnergy += energyRegen * Time.deltaTime;
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
        UpdateEnergyUI();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayHealthLoss();

        GetComponent<SpriteRenderer>().color = Color.red;
        Invoke(nameof(ResetColor), 0.1f);

        FindObjectOfType<CameraShake>().Shake();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Core Destroyed!");
        Time.timeScale = 0f;

        if (ScoreManager.Instance != null)
            ScoreManager.Instance.SaveScoreAndHighScore();

        StartCoroutine(LoadGameOver());
    }

    IEnumerator LoadGameOver()
    {
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameOver");
    }

    public void UseEnergy(float amount)
    {
        currentEnergy -= amount;
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
        UpdateEnergyUI();
    }

    public void AddMaxEnergy(float amount)
    {
        maxEnergy += amount;
        currentEnergy = Mathf.Clamp(currentEnergy + amount, 0, maxEnergy);
        UpdateEnergyUI();
    }

    public void AddMaxHealth(float amount)
    {
        maxHealth += amount;
        UpdateHealthUI();
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();
    }

    void UpdateEnergyUI()
    {
        if (energyText != null)
            energyText.text = "Stability: " + currentEnergy.ToString("F0") + " / " + maxEnergy.ToString("F0");
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
            healthText.text = "Health: " + currentHealth.ToString("F0") + " / " + maxHealth.ToString("F0");
    }

    void ResetColor()
    {
        GetComponent<SpriteRenderer>().color = originalColor;
    }
}