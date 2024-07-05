using System.Collections;
using System.Collections.Generic;
using Content.Blocks;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    private WeaponBlock weaponBlock;
    private float rotationSpeed;

    private void Start()
    {
        // Get the attached WeaponBlock from the parent object
        weaponBlock = GetComponentInParent<WeaponBlock>();

        if (!weaponBlock)
        {
            Debug.LogError("No WeaponBlock component found in parent object.");
        }
        else
        {
            rotationSpeed = weaponBlock.rotationSpeed; // Set rotation speed
        }
    }

    void Update()
    {
        // Get the mouse position in world space (not screen space)
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z; // Ensure Z coordinates are the same

        // Calculate the direction to the target
        Vector2 direction = mousePosition - transform.position;

        // Calculate the target angle
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; // Adjust based on the object's default orientation

        // Get the current angle of the object
        float currentAngle = transform.eulerAngles.z;

        // Calculate the new angle with a speed limit
        float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);

        // Apply the new rotation
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, newAngle));

        // If mouse button down is detected, trigger custom function `OnMouseDown`.
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseDown();
        }
    }

    private void OnMouseDown()
    {
        // Firing logic
        // TODO: Implement firing logic here
    }
}