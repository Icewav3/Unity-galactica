using System.Collections.Generic;
using Content;
using UnityEngine;

namespace SceneManagement
{
    public class GameState : BaseState
    {
        private Camera _camera;
        private GameObject _canvas;
        private bool _dead;
        private bool _editor;
        private int _id;
        private Vector3 _mousePos;
        private Rigidbody2D _player;
        public List<ShipContainer> Ships { get; private set; } // Use properties instead of public fields
        public float Acceleration { get; private set; } = 0.01f;
        public float TopSpeed { get; private set; } = 3f;

        private void Start()
        {
            InitializeComponents();
            InitializeClient();
            InitializeFuelTypes();
        }

        public override void Update()
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
                    StateMachine.TransitionTo("MainMenu");
                }

                HandleInput();
            }
        }

        private void InitializeComponents()
        {
            _player = GetComponentInChildren<Rigidbody2D>();
            _camera = GetComponentInChildren<Camera>();
            _canvas = gameObject.transform.Find("Canvas")?.gameObject;

            Helper.Util.CheckForNull(_player, "Player");
            Helper.Util.CheckForNull(_camera, "Camera");
            Helper.Util.CheckForNull(_canvas, "Canvas");

            InitializeClient(); // Combine Client initialization into a separate method
        }

        private void InitializeFuelTypes() // Load Fuel types
        {
            List<Fuel> fuelList = FuelManager.GetFuelTypes();

            if (StateMachine.DebugMode)
            {
                foreach (var fuel in fuelList)
                {
                    Debug.Log($"Fuel Type: {fuel.FuelName}");
                    Debug.Log($"Fuel Color: {fuel.FuelColor}");
                    Debug.Log($"Base Price: {fuel.BasePricePerUnit}");
                }
            }
        }

        private void InitializeClient()
        {
            _dead = true;
            _id = Time.frameCount;

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

            if (StateMachine.DebugMode)
            {
                Debug.Log("Editor state: " + _editor);
            }
        }

        private void HandleInput()
        {
            // Handle player input here...
        }
    }
}