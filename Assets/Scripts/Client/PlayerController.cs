using UnityEngine;
using UnityEngine.InputSystem;

namespace Client
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D _playerRigidbody;

        private void Awake()
        {
            _playerRigidbody = GetComponentInChildren<Rigidbody2D>();
        }

        private void Update()
        {
            //testing
            //_playerRigidbody.velocity = Vector2.up;
        }

        public void DirectionalMovement(InputAction.CallbackContext context)
        {
            Vector2 movementInput = context.ReadValue<Vector2>();
            _playerRigidbody.velocity = movementInput;
        }

        public void RotationalMovement(InputAction.CallbackContext context)
        {
            float rotationInput = context.ReadValue<float>();
            transform.Rotate(Vector3.forward, rotationInput);
        }
    }
}