using UnityEngine;
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

        GetComponent<SpriteRenderer>().color = Color.red;
        Invoke(nameof(ResetColor), 0.1f);

        FindObjectOfType<CameraShake>().Shake();

        if (currentHealth <= 0)
        {
            //Die();
        }
    }

    void Die()
    {
        Debug.Log("Core Destroyed!");
        Time.timeScale = 0f;
    }

    public void UseEnergy(float amount)
    {
        currentEnergy -= amount;
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
        UpdateEnergyUI();
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