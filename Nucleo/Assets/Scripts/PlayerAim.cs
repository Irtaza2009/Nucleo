using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public Transform particleSystemTransform;

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);

        // Counter-rotate the particle system to keep it at 0 rotation
        if (particleSystemTransform != null)
        {
            particleSystemTransform.rotation = Quaternion.identity;
        }
    }
}