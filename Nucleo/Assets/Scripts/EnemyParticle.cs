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

    private float currentHealth;
    private Transform target;

    void Start()
    {
        currentHealth = baseHealth;
        target = GameObject.FindWithTag("Player").transform;
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
}