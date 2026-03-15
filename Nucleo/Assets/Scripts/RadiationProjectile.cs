using UnityEngine;

public class RadiationProjectile : MonoBehaviour
{
    public float speed;
    public float damage;
    public float range;
    public RadiationType type;
    public float destroyAnimationLeadTime = 0.1f;
    public float destroyAnimationDuration = 0.5f;
    public float destroyAnimationSpeedMultiplier = 1f;
    public string projectileTypeParameter = "ProjectileType";
    public Sprite gammaProjectileSprite;
    // private Color alphaColor = new Color(1f, 0.45f, 0.2f);
    private Color alphaColor = new Color(1f, 1f, 1f); // white
    private Color betaColor = new Color(0.2f, 0.6f, 1f);
    // private Color gammaColor = new Color(0.9f, 0.9f, 0.2f);
    private Color gammaColor = new Color(1f, 1f, 1f); // white

    private Vector3 startPosition;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool hasStartedDestroyAnimation;

    void Start()
    {
        startPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        // Keep destroy animation from playing on spawn.
        if (animator != null)
            animator.enabled = false;

        ApplyColor(type);
        ApplyProjectileVisuals();
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        float distanceTravelled = Vector3.Distance(startPosition, transform.position);
        float remainingDistance = range - distanceTravelled;
        float triggerDistance = Mathf.Max(0.01f, speed) * destroyAnimationLeadTime;

        if (!hasStartedDestroyAnimation && remainingDistance <= triggerDistance)
        {
            StartRangeDestroyAnimation();
        }

        if (distanceTravelled >= range)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyParticle enemy = collision.GetComponent<EnemyParticle>();
            enemy.TakeDamage(damage, type);

            Destroy(gameObject);
        }
    }

    void StartRangeDestroyAnimation()
    {
        if (hasStartedDestroyAnimation)
            return;

        hasStartedDestroyAnimation = true;
        if (animator != null)
        {
            animator.enabled = true;
            ApplyAnimatorTypeParameter();
            animator.speed = Mathf.Max(0.01f, destroyAnimationSpeedMultiplier);
        }
    }

    void ApplyProjectileVisuals()
    {
        // Optional per-type sprite override.
        if (type == RadiationType.Gamma && gammaProjectileSprite != null && spriteRenderer != null)
            spriteRenderer.sprite = gammaProjectileSprite;

        ApplyAnimatorTypeParameter();
    }

    void ApplyAnimatorTypeParameter()
    {
        if (animator == null || string.IsNullOrEmpty(projectileTypeParameter))
            return;

        animator.SetInteger(projectileTypeParameter, (int)type);
    }

    public void ApplyColor(RadiationType radiationType)
    {
        if (spriteRenderer == null)
            return;

        switch (radiationType)
        {
            case RadiationType.Alpha:
                spriteRenderer.color = alphaColor;
                break;
            case RadiationType.Beta:
                spriteRenderer.color = betaColor;
                break;
            case RadiationType.Gamma:
                spriteRenderer.color = gammaColor;
                break;
        }
    }
}
