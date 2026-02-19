using UnityEngine;

public class RadiationProjectile : MonoBehaviour
{
    public float speed;
    public float damage;
    public float range;
    public RadiationType type;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
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
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
