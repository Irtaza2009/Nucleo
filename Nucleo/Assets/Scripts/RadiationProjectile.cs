using UnityEngine;

public class RadiationProjectile : MonoBehaviour
{
    public float speed;
    public float damage;
    public float range;
    public RadiationType type;
    private Color alphaColor = new Color(1f, 0.45f, 0.2f);
    private Color betaColor = new Color(0.2f, 0.6f, 1f);
    private Color gammaColor = new Color(0.9f, 0.9f, 0.2f);

    private Vector3 startPosition;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        startPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        ApplyColor(type);
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        if (Vector3.Distance(startPosition, transform.position) > range)
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
