using UnityEngine;

namespace Content.Defs
{
    public class ProjectileDef : MonoBehaviour
    {
        public float hitDamage = 1f;
        public float projectileMaxHealth = 1f;
        public float projectileHealth = 1f;
        public float projectileSpeed = 1f;
    }

    public class ExplosiveShellDef : ProjectileDef
    {
        public float explosionRadius = 1f;
        public float explosionDamage = 1f;
    }
}