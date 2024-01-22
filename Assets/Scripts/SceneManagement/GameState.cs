using System.Collections.Generic;
using Client;
using Content;
using Helper;
using UnityEngine;
using UnityEngine.UI;

namespace SceneManagement
{
    public class GameState : MonoBehaviour
    {
        public bool DebugMode = true;

        public float Acceleration = 0.01f;
        public float TopSpeed = 3f;

        private Camera _camera;
        private GameObject _canvas;
        private Text _damageIndicator;
        private bool _dead;
        private bool _editor = false;
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
            
            // Load the Canvas prefab from Resources folder and instantiate it under the `_canvas` GameObject
            /*var canvasPrefab = Resources.Load("Prefabs/Canvas") as GameObject;
            if (canvasPrefab != null)
            {
                _canvas = Instantiate(canvasPrefab, _canvas.transform, true);
                _canvas.SetActive(false);
            }
            else
            {
                Debug.LogError(
                    "Can't load 'Canvas' prefab from Resources Folder. Please make sure the prefab exists and is placed in Assets/Resources folder");
            }*/

            Util.CheckForNull(playerObject, "Player");
            Util.CheckForNull(_camera, "Camera");
            Util.CheckForNull(_canvas, "Canvas");

            InitializeClient();
        }

        private void InitializeFuelTypes() // Load Fuel types
        {
            List<Fuel> fuelList = FuelManager.GetFuelTypes();

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

        private void ToggleEditorMode()
        {
            _editor = !_editor;
            _canvas?.SetActive(_editor);
            Time.timeScale = _editor ? 0 : 1;

            if (DebugMode)
            {
                Debug.Log("Editor state: " + _editor);
                Debug.Log("Sim speed: "+Time.timeScale);
            }
        }

        private void HandleInput()
        {
            // Handle player input here...
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //Debug.Log(Player.Menu());
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