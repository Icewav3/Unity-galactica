using System.Collections;
using System.Collections.Generic;
using Content.Blocks;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    private WeaponBlock weaponBlock;
    private void Start()
    {
        // Get the attached WeaponBlock from the parent object
        weaponBlock = GetComponentInParent<WeaponBlock>();
        
        if (!weaponBlock) 
        {
            Debug.LogError("No WeaponBlock component found in parent object.");
        }
    }
    void Update() //TODO implement rotation speed limit
    {
        // get the mouse position in world space (not screen space)
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var rotationSpeedLimit = weaponBlock.rotationSpeed;
        // calculate angle to rotate
        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
        );

        transform.up = direction;

        // If mouse button down is detected, trigger custom function `OnMouseDown`.
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseDown();
        }
    }

    void OnMouseDown()
    {
        // Firing logic
        // TODO: Implement firing logic here
    }
}