using Mechanics;
using UnityEngine;

[CreateAssetMenu(fileName = "NewProjectile", menuName = "Projectiles/ProjectileDef")]
public class ProjectileDef : ScriptableObject
{
    public float damage = 1f;
    public float maxHealth = 1f;
    public float speed = 1f;
    public float maxDistance = 100f; // Add this line
    public GameObject prefab;

    public virtual void OnHit(GameObject target, Vector2 hitPoint) { }
}

// ExplosiveProjectileDef.cs
[CreateAssetMenu(fileName = "NewExplosiveProjectile", menuName = "Projectiles/ExplosiveProjectileDef")]
public class ExplosiveProjectileDef : ProjectileDef
{
    public float explosionRadius = 1f;
    public float explosionDamage = 1f;

    public override void OnHit(GameObject target, Vector2 hitPoint)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(hitPoint, explosionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.TakeDamage(explosionDamage);
            }
        }
    }
}