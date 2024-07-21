using System.Collections;
using System.Collections.Generic;
using Content;
using Content.Blocks;
using UnityEngine;

namespace Mechanics.TurretControllers
{
    public class KineticTurretController : TurretControllerBase
    {
        public AudioClip fireClip;
        public GameObject projectilePrefab;
        public Transform firePoint;
        private AudioSource _audioSource;

        protected override void Start()
        {
            base.Start();
            _audioSource = GetComponent<AudioSource>();
            _audioSource.clip = fireClip;
        }

        protected override void FireWeapon()
        {
            // Play firing sound if not already playing
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }

            // Instantiate and launch projectile
            GameObject projectileInstance = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            ProjectileHandler projectileHandler = projectileInstance.GetComponent<ProjectileHandler>();
            if (projectileHandler != null)
            {
                Vector2 launchDirection = firePoint.up; // Assuming the firePoint's up direction is the firing direction
                Projectile projectileData = projectileInstance.GetComponent<Projectile>();
                if (projectileData != null)
                {
                    projectileHandler.Initialize(projectileData, launchDirection, gameObject);
                }
            }
        }
    }
}