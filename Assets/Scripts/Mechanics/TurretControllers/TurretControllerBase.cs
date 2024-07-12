using Content;
using Content.Blocks;
using UnityEngine;

namespace Mechanics.TurretControllers
{
    public class TurretControllerBase : MonoBehaviour
    {
        protected WeaponBlock weaponBlock;
        protected float rotationSpeed;
        protected float maxRange;
        protected AimType _aimType;

        protected virtual void Start()
        {
            weaponBlock = GetComponentInParent<WeaponBlock>();

            if (!weaponBlock)
            {
                Debug.LogError("No WeaponBlock component found in parent object.");
            }
            else
            {
                rotationSpeed = weaponBlock.rotationSpeed;
                maxRange = weaponBlock.maxRange;
                _aimType = weaponBlock.aimType;
            }
        }

        protected void Update()
        {
            RotateTowardsMouse();
            if (Input.GetMouseButton(0))
                FireWeapon();
        }

        protected virtual void FireWeapon()
        {
            // Firing logic
            // TODO: Implement firing logic here
        }

        protected void RotateTowardsMouse() 
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = transform.position.z;

            Vector2 direction = mousePosition - transform.position;

            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

            float currentAngle = transform.eulerAngles.z;

            float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);

            transform.rotation = Quaternion.Euler(new Vector3(0, 0, newAngle));
        }
    }
}