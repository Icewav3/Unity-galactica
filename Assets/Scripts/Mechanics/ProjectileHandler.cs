using UnityEngine;
using Content;
using Mechanics;

public class ProjectileHandler : MonoBehaviour //TODO ROUGH DRAFT
{
    private Projectile currentProjectile;
    private Vector2 Direction;
    private float distanceTravelled = 0f;

    public void Initialize(Projectile projectile, Vector2 launchDirection)
    {
        currentProjectile = projectile;
        Direction = launchDirection.normalized;
        currentProjectile.projectileHealth = currentProjectile.projectileMaxHealth;
    }

    private void Update()
    {
        if (currentProjectile)
        {
            Move();
            CheckHealth();
        }
    }

    private void CheckHealth()
    {
        if (currentProjectile.projectileHealth <= 0)
        {
            if (currentProjectile is ExplosiveShell)
            {
                Explode((ExplosiveShell)currentProjectile);
            }
            Destroy(currentProjectile.gameObject);
        }
    }

    private void Move()
    {
        float moveDist = currentProjectile.projectileSpeed * Time.deltaTime;
        distanceTravelled += moveDist;
        Vector3 direction3D = new Vector3(Direction.x, Direction.y, 0);
        currentProjectile.transform.position += direction3D * moveDist;
    }

    private void OnTriggerEnter(Collider other)
    {
        HealthManager healthManager = other.GetComponent<HealthManager>();
        if (healthManager != null)
        {
            healthManager.TakeDamage(currentProjectile.hitDamage);
            currentProjectile.projectileHealth -= 1f;
        }
    }

    private void Explode(ExplosiveShell shell)
    {
        Collider[] hitColliders = Physics.OverlapSphere(shell.transform.position, shell.explosionRadius);
        foreach (var hitCollider in hitColliders)
        {
            HealthManager healthManager = hitCollider.GetComponent<HealthManager>();
            if (healthManager != null)
            {
                healthManager.TakeDamage(shell.explosionDamage);
            }
        }
    }
}