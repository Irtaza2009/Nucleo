using UnityEngine;

public class RadiationShooter : MonoBehaviour
{
    public GameObject projectilePrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            Shoot(RadiationType.Alpha);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            Shoot(RadiationType.Beta);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            Shoot(RadiationType.Gamma);
    }

    void Shoot(RadiationType type)
    {

        float cost = 0;

        switch (type)
        {
            case RadiationType.Alpha:
                cost = 12f;
                break;
            case RadiationType.Beta:
                cost = 15f;
                break;
            case RadiationType.Gamma:
                cost = 18f;
                break;
        }
        
        PlayerCore core = GetComponent<PlayerCore>();

        if (core.CurrentEnergy < cost) return;

        core.UseEnergy(cost);

        GameObject proj = Instantiate(projectilePrefab, transform.position, transform.rotation);
        RadiationProjectile projectile = proj.GetComponent<RadiationProjectile>();
        projectile.type = type;

        switch (type)
        {
            case RadiationType.Alpha:
                projectile.speed = 3f;
                projectile.damage = 18f;
                projectile.range = 2f;
                break;

            case RadiationType.Beta:
                projectile.speed = 6f;
                projectile.damage = 12f;
                projectile.range = 4f;
                break;

            case RadiationType.Gamma:
                projectile.speed = 10f;
                projectile.damage = 16f;
                projectile.range = 10f;
                break;
        }

        projectile.ApplyColor(type);
    }
}