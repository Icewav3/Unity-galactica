using Mechanics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(ProjectileHealthManager))]
public class Projectile : MonoBehaviour
{
    private ProjectileDef _def;
    private Rigidbody2D _rb;
    private ProjectileHealthManager _healthManager;
    private Vector2 _startPosition;

    public void Initialize(ProjectileDef def, Vector2 direction, GameObject shooter)
    {
        _def = def;
        _rb = GetComponent<Rigidbody2D>();
        _healthManager = GetComponent<ProjectileHealthManager>();

        _rb.velocity = direction * def.speed;
        _healthManager.SetMaxHealth(_def.maxHealth);
        _startPosition = transform.position; // Record the start position

        if (shooter.TryGetComponent<Collider2D>(out var shooterCollider))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), shooterCollider);
        }
    }

    private void Update()
    {
        // Destroy the projectile if it has traveled beyond maxDistance
        if (Vector2.Distance(_startPosition, transform.position) >= _def.maxDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(_def.damage);
            _def.OnHit(other.gameObject, transform.position);
            Destroy(gameObject);
        }
    }
}