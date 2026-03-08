using UnityEngine;

public enum EnemyType
{
    Organic,
    Metallic,
    Light
}

public class EnemyParticle : MonoBehaviour
{
    public EnemyType enemyType;

    public float baseHealth = 20f;
    public float speed = 2f;
    public float damage = 10f;

    private float currentHealth;
    private Transform target;

    void Start()
    {
        // Set health based on enemy type
        switch (enemyType)
        {
            case EnemyType.Organic:
                baseHealth = 25f;
                break;
            case EnemyType.Metallic:
                baseHealth = 20f;
                break;
            case EnemyType.Light:
                baseHealth = 15f;
                break;
        }

        currentHealth = baseHealth;
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
            target = playerObject.transform;
    }

    void Update()
    {
        if (target == null) return;

        Vector2 direction = (target.position - transform.position).normalized;
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    public void TakeDamage(float damage, RadiationType radiationType)
    {
        float multiplier = GetDamageMultiplier(radiationType);

        currentHealth -= damage * multiplier;

        if (currentHealth <= 0)
        {
            // Award score based on enemy type and radiation effectiveness
            if (ScoreManager.Instance != null)
            {
                int baseScore = GetBaseScore();
                bool isWeakness = multiplier > 1f;
                ScoreManager.Instance.AddScore(baseScore, isWeakness);
            }
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerCore playerCore = collision.GetComponent<PlayerCore>();
            if (playerCore != null)
            {
                playerCore.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }

    float GetDamageMultiplier(RadiationType radiationType)
    {
        switch (enemyType)
        {
            case EnemyType.Organic:
                if (radiationType == RadiationType.Alpha) return 2f;
                if (radiationType == RadiationType.Gamma) return 0.5f;
                break;

            case EnemyType.Metallic:
                if (radiationType == RadiationType.Gamma) return 2f;
                if (radiationType == RadiationType.Alpha) return 0.5f;
                break;

            case EnemyType.Light:
                if (radiationType == RadiationType.Beta) return 2f;
                if (radiationType == RadiationType.Alpha) return 0.5f;
                break;
        }

        return 1f;
    }

    int GetBaseScore()
    {
        switch (enemyType)
        {
            case EnemyType.Organic:
                return 100; // Tankier, more points
            case EnemyType.Metallic:
                return 75;  // Balanced
            case EnemyType.Light:
                return 50;  // Fragile, fewer points
            default:
                return 50;
        }
    }
}