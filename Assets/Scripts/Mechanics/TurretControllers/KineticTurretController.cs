using System;
using System.Collections;
using System.Collections.Generic;
using Content;
using Content.Blocks;
using Content.Defs;
using UnityEngine;

namespace Mechanics.TurretControllers
{
    public class KineticTurretController : TurretControllerBase
    {
        [Header("Turret Settings")]
        public AudioClip fireClip;
        public GameObject projectilePrefab;
        public Transform firePoint;

        [Header("Projectile Settings")]
        public float projectileSpeed = 10f;
        public float projectileMaxHealth = 1f;
        public float hitDamage = 10f;
        public bool isExplosive = false;
        public float explosionRadius = 0f;
        public float explosionDamage = 0f;

        private AudioSource _audioSource;

        protected override void Start()
        {
            base.Start();
            _audioSource = GetComponent<AudioSource>();
            if (_audioSource == null)
            {
                Debug.LogWarning("No AudioSource");
            }
        }

        protected override void FireWeapon()
        {
            if (firePoint == null || projectilePrefab == null)
            {
                Debug.LogWarning("FirePoint or ProjectilePrefab is not assigned.");
                return;
            }

            // Play firing sound if not already playing
            if (fireClip != null && !_audioSource.isPlaying)
            {
                _audioSource.Play();
            }

            // Instantiate and launch projectile
            GameObject projectileInstance = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Projectile projectile = projectileInstance.GetComponent<Projectile>();
            if (projectile != null)
            {
                Vector2 launchDirection = firePoint.up; // Assuming the firePoint's up direction is the firing direction
                ProjectileDef projectileData = projectileInstance.GetComponent<ProjectileDef>();
                if (projectileData != null)
                {
                    projectileData.projectileSpeed = projectileSpeed;
                    projectileData.projectileMaxHealth = projectileMaxHealth;
                    projectileData.hitDamage = hitDamage;
                    if (isExplosive && projectileData is ExplosiveShellDef explosiveShell)
                    {
                        explosiveShell.explosionRadius = explosionRadius;
                        explosiveShell.explosionDamage = explosionDamage;
                    }
                    projectile.Initialize(projectileData, launchDirection, gameObject);
                }
            }
        }
    }
}
