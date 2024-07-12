using System.Collections;
using System.Collections.Generic;
using Content;
using Content.Blocks;
using Mechanics.TurretControllers;
using UnityEngine;

public class LaserTurretController : TurretControllerBase
{
    public LayerMask collisionMask; 
    public Gradient laserColorGradient;
    public float pulseSpeed = 2f;
    public float maxLaserWidth = 0.5f;
    public float minLaserWidth = 0.1f;
 
    private LineRenderer lineRenderer;

    protected override void Start()
    {
        base.Start();

        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        lineRenderer.startWidth = minLaserWidth;
        lineRenderer.endWidth = minLaserWidth;
        lineRenderer.positionCount = 2;
        lineRenderer.colorGradient = laserColorGradient;
        lineRenderer.enabled = false; // Initially disable the laser
    }

    private void UpdateLaserBeam(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxRange, collisionMask);
        if (hit.collider != null)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            Vector3 normalizedDirection = direction.normalized;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position + normalizedDirection * maxRange);
        }
    }

    private void PulsateLaser()
    {
        float pulse = Mathf.PingPong(Time.time * pulseSpeed, maxLaserWidth - minLaserWidth) + minLaserWidth;
        lineRenderer.startWidth = pulse;
        lineRenderer.endWidth = pulse;
    }

    protected override void FireWeapon()
    {
        lineRenderer.enabled = true; 
        UpdateLaserBeam(transform.up);
        // PulsateLaser();
    }
}
