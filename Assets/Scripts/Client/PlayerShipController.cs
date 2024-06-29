using Content;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Client
{
    public class PlayerShipController : MonoBehaviour
    {
        public float maxSpeed = 10f;
        public float rotationSpeed = 100f;
        private EditorStateController _editorStateController;
        private Rigidbody2D _playerRigidbody;
        private ShipContainer _playerShipContainer;


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
            if (inEditorMode)
            {
                print(_playerShipContainer
                    .ShipName); //TODO change to using addforce and just have shipcontainer update mass and let physics engine do its magic this will be done indepenantly for each block in it's own monobehavoir and called via events
            }
        }

        public void DirectionalMovement(InputAction.CallbackContext context)
        {
            Vector2 movementInput = context.ReadValue<Vector2>();
            Debug.Log(movementInput);
            ApplyForce(movementInput);
        }

        public void RotationalMovement(InputAction.CallbackContext context)
        {
            float rotationInput = context.ReadValue<float>();
            Debug.Log(rotationInput);
            //transform.Rotate(Vector3.forward, rotationInput);
        }
        private void ApplyForce(Vector2 direction)
        {
            Vector2 force = direction * 1;
            _playerRigidbody.AddForce(force);
        }
    }
}