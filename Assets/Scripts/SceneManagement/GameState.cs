using System.Collections.Generic;
using Content;
using Helper;
using UnityEngine;

namespace SceneManagement
{
    public class GameState : MonoBehaviour
    {
        public bool DebugMode = true;
        public float Acceleration = 0.01f;
        public float TopSpeed = 3f;
        public ShipContainer _playerShipContainer;
        private Camera _camera;
        private bool _dead;
        private GameObject _hudCanvas;
        private int _id;
        private Vector3 _mousePos;

        public void Start()
        {
            if (DebugMode)
            {
                Debug.Log("starting Gamestate");
            }

            _playerShipContainer = GameObject.Find("ShipContainer").GetComponent<ShipContainer>();

            if (_playerShipContainer == null)
            {
                Debug.LogError("ShipContainer GameObject is missing in the scene or not assigned");
                return;
            }

            StateMachine.ShipContainer = _playerShipContainer;
            InitializeComponents();
            InitializeFuelTypes();
        }

        public void Update()
        {
            if (_dead)
            {
                //Player.Create(_id);
            }

            HandleInput();
        }

        private void InitializeComponents()
        {
            if (DebugMode)
            {
                Debug.Log("initalizing");
            }

            GameObject playerObject = GameObject.Find("Player");
            _camera = Util.FindOrCreateComponent<Camera>("Main Camera");

            if (DebugMode)
            {
                Debug.Log("setting parents");
            }

            // Set camera as a child of the player
            _camera.transform.parent = playerObject.transform;

            if (DebugMode)
            {
                Debug.Log("inspecting components");
            }

            Util.CheckForNull(playerObject, "Player");
            Util.CheckForNull(_camera, "Camera");

            InitializeClient();
        }

        /// <summary>
        /// Initializes the fuel types for the game.
        /// </summary>
        private void InitializeFuelTypes()
        {
            List<Fuel> fuelList = FuelInitializer.GetFuelTypes();

            if (DebugMode)
            {
                foreach (var fuel in fuelList)
                {
                    Debug.Log($"Fuel Type: {fuel.FuelName}");
                    Debug.Log($"Fuel Color: {fuel.FuelColor}");
                    Debug.Log($"Base Price: {fuel.BasePricePerUnit}");
                }
            }
        }

        /// <summary>
        /// This method initializes the player. 
        /// It should not be called directly, 
        /// it should be called only from <see cref="InitializeComponents"/>.
        /// </summary>
        private void InitializeClient()
        {
            _dead = true;
        }

        private void HandleInput()
        {
            HandleShipMovement();
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

        private void HandleShipMovement()
        {
            var rb = _playerShipContainer.rb;
            rb.velocity = CalculateNewVelocity(rb.velocity);
        }

        private Vector2 CalculateNewVelocity(Vector2 velocity)
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (velocity.y < TopSpeed)
                    velocity += new Vector2(0, Acceleration);
                else
                    velocity = new Vector2(velocity.x, TopSpeed);
                if (DebugMode)
                    Debug.Log("w");
            }

            if (Input.GetKey(KeyCode.S))
            {
                if (velocity.y > -TopSpeed)
                    velocity += new Vector2(0, -Acceleration);
                else
                    velocity = new Vector2(velocity.x, -TopSpeed);
                if (DebugMode)
                    Debug.Log("s");
            }

            if (Input.GetKey(KeyCode.A))
            {
                if (velocity.x > -TopSpeed)
                    velocity += new Vector2(-Acceleration, 0);
                else
                    velocity = new Vector2(-TopSpeed, velocity.y);
                if (DebugMode)
                    Debug.Log("a");
            }

            if (Input.GetKey(KeyCode.D))
            {
                if (velocity.x < TopSpeed)
                    velocity += new Vector2(Acceleration, 0);
                else
                    velocity = new Vector2(TopSpeed, velocity.y);
                if (DebugMode)
                    Debug.Log("d");
            }

            return velocity;
        }
    }
}