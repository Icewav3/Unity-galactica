using UnityEngine;

namespace Mechanics.TurretControllers
{
    [RequireComponent(typeof(AudioSource))]
    public class KineticTurretController : TurretControllerBase
    {
        [SerializeField] private ProjectileDef projectileDef;
        [SerializeField] private Transform firePoint;
        [SerializeField] private AudioClip fireClip;
        [SerializeField] private float fireRate = 1f;

        private AudioSource _audioSource;
        private float _nextFireTime;

        protected override void Start()
        {
            base.Start();
            _audioSource = GetComponent<AudioSource>();
        }

        protected override void FireWeapon()
        {
            if (firePoint == null || projectileDef == null) return;

            _audioSource.PlayOneShot(fireClip);

            GameObject projectileInstance = Instantiate(projectileDef.prefab, firePoint.position, firePoint.rotation);
            if (projectileInstance.TryGetComponent<Projectile>(out var projectile))
            {
                projectile.Initialize(projectileDef, firePoint.up, gameObject);
            }

            _nextFireTime = Time.time + 1f / fireRate;
        }
    }
}