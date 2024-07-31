using UnityEngine;

namespace Mechanics.TurretControllers
{
    [RequireComponent(typeof(AudioSource))]
    public class KineticTurretController : TurretControllerBase
    {
        [SerializeField] private ProjectileDef projectileDef;
        [SerializeField] private AudioClip fireClip;
        [SerializeField] private float fireRate = 1f;

        private AudioSource _audioSource;
        private float _nextFireTime;
        private Transform[] _firePoints;
        private int _currentFirePoint;

        protected override void Start()
        {
            base.Start();
            _audioSource = GetComponent<AudioSource>();
            //Barrel alternates between two fire points
            _firePoints = new Transform[2];
            _firePoints[0] = transform.Find("FirePoint1");
            _firePoints[1] = transform.Find("FirePoint2");
        }

        protected override void FireWeapon()
        {
            if (Time.time < _nextFireTime) return;
            if (_firePoints[_currentFirePoint] == null || projectileDef == null) return;

            _audioSource.PlayOneShot(fireClip);

            GameObject projectileInstance = Instantiate(projectileDef.prefab, _firePoints[_currentFirePoint].position,
                _firePoints[_currentFirePoint].rotation);
            if (projectileInstance.TryGetComponent<Projectile>(out var projectile))
            {
                projectile.Initialize(projectileDef, _firePoints[_currentFirePoint].up, gameObject);
            }

            _nextFireTime = Time.time + 1f / fireRate;

            // Switching to the next fire point
            _currentFirePoint++;
            if (_currentFirePoint >= _firePoints.Length)
            {
                _currentFirePoint = 0;
            }
        }
    }
}