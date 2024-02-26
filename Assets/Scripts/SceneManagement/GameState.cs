using System.Collections.Generic;
using Content;
using Helper;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SceneManagement
{
    public class GameState : MonoBehaviour
    {
        public bool DebugMode = true;

        public float Acceleration = 0.01f;
        public float TopSpeed = 3f;
        public bool _editor = false;
        public UnityEvent<bool> OnEditorModeChanged = new UnityEvent<bool>();

        private Camera _camera;
        private GameObject _canvas;
        private Text _damageIndicator;
        private bool _dead;
        private GameObject _hudCanvas;
        private int _id;
        private Vector3 _mousePos;
        private ShipContainer _playerShipContainer;


        public void Start()
        {
            if (DebugMode)
            {
                Debug.Log("starting Gamestate");
            }

            if (_playerShipContainer == null)
            {
                _playerShipContainer = GameObject.Find("ShipContainer").GetComponent<ShipContainer>();
            }

            StateMachine.ShipContainer = _playerShipContainer;
            InitializeComponents();
            InitializeFuelTypes();
        }

        public void Update()
        {
            bool tab = Input.GetKeyDown(KeyCode.Tab);
            _mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);

            if (tab) //this is a keybind
            {
                ToggleEditorMode();
            }
            else
            {
                if (_dead)
                {
                    //Player.Create(_id);
                }

                HandleInput();
            }
        }

        private void InitializeComponents()
        {
            if (DebugMode)
            {
                Debug.Log("initalizing");
            }

            GameObject playerObject = GameObject.Find("Player");
            _camera = Util.FindOrCreateComponent<Camera>("Main Camera");
            _canvas = GameObject.Find("Canvas");

            if (DebugMode)
            {
                Debug.Log("setting parents");
            }

            //  Set camera and canvas as children of player
            _camera.transform.parent = playerObject.transform;
            _canvas.transform.parent = playerObject.transform;

            if (DebugMode)
            {
                Debug.Log("looking for canvas");
            }

            Util.CheckForNull(playerObject, "Player");
            Util.CheckForNull(_camera, "Camera");
            Util.CheckForNull(_canvas, "Canvas");

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
            if (_canvas != null)
            {
                _canvas.SetActive(false);
            }
        }

        public void ToggleEditorMode()
        {
            _editor = !_editor;
            OnEditorModeChanged.Invoke(_editor);
            _canvas?.SetActive(_editor);
            Time.timeScale = _editor ? 0 : 1;
            if (DebugMode)
            {
                Debug.Log("Editor state: " + _editor);
                Debug.Log("Sim speed: " + Time.timeScale);
            }
        }

        private void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
            }

            if (Input.GetKey(KeyCode.W))
            {
                if (_playerShipContainer.rb.velocity.y < TopSpeed)
                {
                    _playerShipContainer.rb.velocity += new Vector2(0, Acceleration);
                }
                else
                {
                    _playerShipContainer.rb.velocity = new Vector2(_playerShipContainer.rb.velocity.x, TopSpeed);
                }

                if (DebugMode)
                {
                    Debug.Log("w");
                }
            }

            if (Input.GetKey(KeyCode.S))
            {
                if (_playerShipContainer.rb.velocity.y > -TopSpeed)
                {
                    _playerShipContainer.rb.velocity += new Vector2(0, -Acceleration);
                }
                else
                {
                    _playerShipContainer.rb.velocity = new Vector2(_playerShipContainer.rb.velocity.x, -TopSpeed);
                }

                if (DebugMode)
                {
                    Debug.Log("s");
                }
            }

            if (Input.GetKey(KeyCode.A))
            {
                if (_playerShipContainer.rb.velocity.x > -TopSpeed)
                {
                    _playerShipContainer.rb.velocity += new Vector2(-Acceleration, 0);
                }
                else
                {
                    _playerShipContainer.rb.velocity = new Vector2(-TopSpeed, _playerShipContainer.rb.velocity.y);
                }

                if (DebugMode)
                {
                    Debug.Log("a");
                }
            }

            if (Input.GetKey(KeyCode.D))
            {
                if (_playerShipContainer.rb.velocity.x < TopSpeed)
                {
                    _playerShipContainer.rb.velocity += new Vector2(Acceleration, 0);
                }
                else
                {
                    _playerShipContainer.rb.velocity = new Vector2(TopSpeed, _playerShipContainer.rb.velocity.y);
                }

                if (DebugMode)
                {
                    Debug.Log("d");
                }
            }
        }
    }
}