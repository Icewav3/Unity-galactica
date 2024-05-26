using Content;
using Content.Blocks.MovementBlocks;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace Client
{
    public class PlayerShipController : MonoBehaviour
    {
        public float maxSpeed = 10f;
        public float speedMult = 100f;
        public float rotationSpeed = 100f;
        private EditorStateController _editorStateController;
        private Rigidbody2D _playerRigidbody;
        private ShipContainer _playerShipContainer;
        private List<ThrusterBlock> _thrusters;

        private void Awake()
        {
            _playerShipContainer = GameObject.Find("ShipContainer").GetComponent<ShipContainer>();
            _playerRigidbody = _playerShipContainer.GetComponent<Rigidbody2D>();
            _editorStateController = GameObject.Find("ShipEditor").GetComponent<EditorStateController>();
            _editorStateController.onEditorModeChanged.AddListener(OnEditorModeChanged);
        }

        private void Update()
        {
            if (_playerRigidbody.velocity.magnitude > maxSpeed)
            {
                _playerRigidbody.velocity = _playerRigidbody.velocity.normalized * maxSpeed;
            }
        }

        private void OnEditorModeChanged(bool inEditorMode)
        {
            if (!inEditorMode)
            {
                _thrusters = new List<ThrusterBlock>(_playerShipContainer.GetComponentsInChildren<ThrusterBlock>());
                print(_thrusters.Count); //succesfully locates thruster
                foreach (var thruster in _thrusters)
                {
                    Debug.Log("Thruster Type: " + thruster.type); // Log each thruster type
                }
            }
        }

        public void DirectionalMovement(InputAction.CallbackContext context)
        {
            Vector2 movementInput = context.ReadValue<Vector2>();

            foreach (var thruster in _thrusters)
            {
                if (thruster.type == ThrusterType.Fixed)
                {
                    _playerRigidbody.AddForce(thruster.direction * thruster.thrustPower * movementInput.y * speedMult);
                }
                else if (thruster.type == ThrusterType.Omni)
                {
                    _playerRigidbody.AddForce(movementInput * thruster.thrustPower * speedMult);
                    print("k"); //doesnt seem to execute
                }
            }
        }

        public void RotationalMovement(InputAction.CallbackContext context)
        {
            float rotationInput = context.ReadValue<float>();
            _playerRigidbody.AddTorque(rotationInput * rotationSpeed);
        }
    }
}
