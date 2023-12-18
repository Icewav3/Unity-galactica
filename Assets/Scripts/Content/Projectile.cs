using UnityEngine;

namespace Content
{
    public class Projectile : MonoBehaviour
    {
        public float hitDamage = 1f;
        public float projectileMaxHealth = 1f;
        public float ProjectileHealth { get; private set; }
        public float projectileSpeed = 1f;
        public bool gravityAffected = true;
    }

    public class ExplosiveShell : Projectile
    {
        public float explosionRadius = 1f;
    }
}