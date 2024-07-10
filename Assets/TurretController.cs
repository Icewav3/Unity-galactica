using System.Collections;
using System.Collections.Generic;
using Content;
using Content.Blocks;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    private WeaponBlock weaponBlock;
    private float rotationSpeed;
    private float maxRange;
    private LineRenderer lineRenderer;
    private AimType _aimType;
    
    
    public LayerMask collisionMask; // Set this in the Inspector to specify what layers the laser should collide with
    public Gradient laserColorGradient; // Assign a gradient in the Inspector to create a color pulsating effect
    public float pulseSpeed = 2f; // Speed of the pulsating effect
    public float maxLaserWidth = 0.5f; // Maximum width of the laser
    public float minLaserWidth = 0.1f; // Minimum width of the laser

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
            //grab from weaponblock component
            rotationSpeed = weaponBlock.rotationSpeed;
            maxRange = weaponBlock.maxRange;
            _aimType = weaponBlock.aimType;
        }

        // Get or add a LineRenderer component
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        // Configure the LineRenderer
        lineRenderer.startWidth = minLaserWidth;
        lineRenderer.endWidth = minLaserWidth;
        lineRenderer.positionCount = 2;
        lineRenderer.colorGradient = laserColorGradient;
        lineRenderer.enabled = false; // Initially disable the laser
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

        // If mouse button is held down, update and display the laser beam
        Vector2 turretDirection = transform.up;
        if (Input.GetMouseButton(0))
        {
            lineRenderer.enabled = true; 
            UpdateLaserBeam(turretDirection);
            //PulsateLaser();
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    private void UpdateLaserBeam(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxRange, collisionMask);
        if (hit.collider != null)
        {
            // Laser hit something
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            // Laser did not hit anything - set to max range
            Vector3 normalizedDirection = direction.normalized;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position + normalizedDirection * maxRange);
        }
    }

    private void PulsateLaser()
    {
        // Calculate the pulsating width
        float pulse = Mathf.PingPong(Time.time * pulseSpeed, maxLaserWidth - minLaserWidth) + minLaserWidth;
        lineRenderer.startWidth = pulse;
        lineRenderer.endWidth = pulse;

        // Optionally, you can also change the color gradient over time for an enhanced effect
        // float colorTime = Mathf.PingPong(Time.time * pulseSpeed, 1);
        // lineRenderer.colorGradient = GetColorGradient(colorTime);
    }

    // Optional: Create a color gradient based on the time value (uncomment if using color pulsation)
    // private Gradient GetColorGradient(float time)
    // {
    //     Gradient gradient = new Gradient();
    //     gradient.SetKeys(
    //         new GradientColorKey[] { new GradientColorKey(Color.red, 0.0f), new GradientColorKey(Color.blue, 1.0f) },
    //         new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) }
    //     );
    //     return gradient;
    // }

    private void OnMouseDown()
    {
        // Firing logic
        // TODO: Implement firing logic here
    }
}
