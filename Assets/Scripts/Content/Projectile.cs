using UnityEngine;

namespace Content
{
    public class Projectile : MonoBehaviour
    {
        public float hitDamage = 1f;
        public float projectileMaxHealth = 1f;
        public float projectileHealth = 1f;
        public float projectileSpeed = 1f;
    }

    public class ExplosiveShell : Projectile
    {
        public float explosionRadius = 1f;
        public float explosionDamage = 1f;
    }
}