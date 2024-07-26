using UnityEngine;
using Content;
using Content.Defs;
using Mechanics;

public class Projectile : MonoBehaviour
{
    private ProjectileDef currentProjectile;
    private Vector2 direction;
    private GameObject shooter;

    public void Initialize(ProjectileDef projectile, Vector2 launchDirection, GameObject shooter)
    {
        currentProjectile = projectile;
        direction = launchDirection.normalized;
        this.shooter = shooter;
        currentProjectile.projectileHealth = currentProjectile.projectileMaxHealth;

        // Initialize Rigidbody for physics-based movement
        Rigidbody2D rb = currentProjectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * currentProjectile.projectileSpeed;
            rb.gravityScale = 0f; // No gravity
            rb.drag = 0f;         // No drag
            rb.angularDrag = 0f;  // No angular drag
        }

        // Ignore collisions with the shooter
        Collider2D projectileCollider = currentProjectile.GetComponent<Collider2D>();
        Collider2D shooterCollider = shooter.GetComponent<Collider2D>();
        if (projectileCollider != null && shooterCollider != null)
        {
            Physics2D.IgnoreCollision(projectileCollider, shooterCollider);
        }
    }

    private void Update()
    {
        if (currentProjectile)
        {
            CheckHealth();
        }
    }

    private void CheckHealth()
    {
        if (currentProjectile.projectileHealth <= 0)
        {
            if (currentProjectile is ExplosiveShellDef explosiveShell)
            {
                Explode(explosiveShell);
            }
            Destroy(currentProjectile.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        HealthManager healthManager = other.GetComponent<HealthManager>();
        if (healthManager != null)
        {
            healthManager.TakeDamage(currentProjectile.hitDamage);
            currentProjectile.projectileHealth -= 1f;
        }
    }

    private void Explode(ExplosiveShellDef shell)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(shell.transform.position, shell.explosionRadius);
        foreach (var hitCollider in hitColliders)
        {
            HealthManager healthManager = hitCollider.GetComponent<HealthManager>();
            if (healthManager != null)
            {
                healthManager.TakeDamage(shell.explosionDamage);
            }
        }
    }

    // Optional: Draw explosion radius in the editor
    private void OnDrawGizmosSelected()
    {
        if (currentProjectile is ExplosiveShellDef shell)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(shell.transform.position, shell.explosionRadius);
        }
    }
}
