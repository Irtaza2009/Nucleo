using UnityEngine;

public class EnemyParticle : MonoBehaviour
{
    public float speed = 2f;
    public float damage = 5f;

    private Transform target;

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (target == null) return;

        Vector2 direction = (target.position - transform.position).normalized;
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerCore>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
