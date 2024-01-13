using System.Collections.Generic;
using Client;
using Content;
using Content.Blocks.MovementBlocks;
using Mechanics;
using UnityEngine;
using UnityEngine.UI;

public class Control : MonoBehaviour
{
    public bool debug = true;
    public float acceleration = 0.01f;
    public float topSpeed = 3;
    private Camera _camera;
    private GameObject _canvas;
    private Text _damageIndicator;
    private bool _damageState = false;
    private bool _dead;
    private bool _editor;

    private GameObject
        _hudCanvas; // Added a new HUD canvas for the damage indicator

    private int _id;
    private Vector3 _mousePos;
    private Rigidbody2D _player;


    /// <summary>
    /// /Attach this to the parent node of the player, Camera, and Canvas, it manages movement and menu positioning
    /// </summary>
    void Start()
    {
        //START TEST CODE
        ThrusterBlock thrusterBlock = gameObject.AddComponent<ThrusterBlock>();
        thrusterBlock.type = ThrusterType.Fixed;
        thrusterBlock.thrustPower = 10f;
        thrusterBlock.mass = 10f;
        var blocks = new List<Block>();
        blocks.Add(thrusterBlock);
        var testContainer = new ShipContainer("test", new GameObject(), blocks);
        testContainer.InitalizeShip();
        Debug.Log($"Ship Name: {testContainer.ShipName}");
        testContainer.UpdateShip(new Vector2(1, 1), (1f));
        Debug.Log("testcontainer mass: " + testContainer.Mass);
        Debug.Log("testcontainer name: " + testContainer.Blocks);
        foreach (var variable in blocks)
        {
            Debug.Log("block type: " + variable.GetType());
        }

        print("Linear acceleration: " + testContainer.LinearAcceleration);
        print("Angular acceleration: " + testContainer.AngularAcceleration);
        print("thrust:" + testContainer.PotentialThrustContribution);
        //END TEST CODE

        //Load Fuel types
        List<Fuel> fuelList = FuelManager.GetFuelTypes();

        if (debug)
        {
            foreach (var fuel in fuelList)
            {
                Debug.Log($"Fuel Type: {fuel.FuelName}");
                Debug.Log($"Fuel Color: {fuel.FuelColor}");
                Debug.Log($"Base Price: {fuel.BasePricePerUnit}");
            }
        }

        //Initialize Player, Camera and Canvas
        _player = GetComponentInChildren<Rigidbody2D>();
        _camera = GetComponentInChildren<Camera>();
        _canvas = gameObject.transform.Find("Canvas").gameObject;
        _hudCanvas =
            new GameObject(
                "HUDCanvas"); // Instantiate a new GameObject for HUD Canvas
        _hudCanvas
            .AddComponent<
                Canvas>(); // Attach a Canvas component to the HUDCanvas GameObject.
        _hudCanvas
            .AddComponent<CanvasScaler>(); // Attach a Canvas Scaler component
        _hudCanvas
            .AddComponent<
                GraphicRaycaster>(); // Attach a Raycaster component for UI interaction

        _damageIndicator =
            new GameObject("DamageIndicator").AddComponent<Text>();
        _damageIndicator.transform.SetParent(_hudCanvas
            .transform); // Set the _damageIndicator parent to _hudCanvas
        _damageIndicator.text = "damage";
        _damageIndicator.enabled = true;

        //Check for nulls
        Helper.Util.CheckForNull(_player, "Player");
        Helper.Util.CheckForNull(_camera, "Camera");
        Helper.Util.CheckForNull(_canvas, "Canvas");
        Helper.Util.CheckForNull(_hudCanvas,
            "HudCanvas"); // Check if new HudCanvas is not null

        //Client initialization
        _dead = true;
        _id = Time.frameCount;
        _canvas.SetActive(false);
    }

    void Update()
    {
        //Start TEST CODE
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _damageState = !_damageState;
            _damageIndicator.enabled = _damageState;
        }

        if (_damageState && Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider != null)
            {
                HealthManager health =
                    hit.collider.gameObject.GetComponent<HealthManager>();
                if (health != null)
                    health.TakeDamage(50f);
            }
        }

        if (_damageState)
            _damageIndicator.transform.position =
                Input.mousePosition + new Vector3(10, 10, 0);
        //END TEST CODE
        //Build mode
        bool tab = Input.GetKeyDown(KeyCode.Tab);
        _mousePos =
            _camera.ScreenToWorldPoint(Input
                .mousePosition); //gets real mouse position

        if (debug)
        {
            //print("Current mouse Position " + _mousePos);
        }

        if (tab)
        {
            _editor = !_editor;
            _canvas.SetActive(_editor); //Toggles UI
            Time.timeScale = _editor ? 0 : 1; //toggles sim
            if (debug)
            {
                print("editor state: " + _editor);
            }
        }
        else
        {
            if (_dead)
            {
                Player.Create(_id, true);
            }

            //onkeypress
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                print(Player.Menu());
            }

            //on keyhold
            if (Input.GetKey(KeyCode.W))
            {
                if (_player.velocity.y < topSpeed)
                {
                    _player.velocity += new Vector2(0, acceleration);
                }
                else
                {
                    _player.velocity =
                        new Vector2(_player.velocity.x, topSpeed);
                }

                if (debug)
                {
                    print("w");
                }
            }

            if (Input.GetKey(KeyCode.S))
            {
                if (_player.velocity.y > -topSpeed)
                {
                    _player.velocity += new Vector2(0, -acceleration);
                }
                else
                {
                    _player.velocity =
                        new Vector2(_player.velocity.x, -topSpeed);
                }

                if (debug)
                {
                    print("s");
                }
            }

            if (Input.GetKey(KeyCode.A))
            {
                if (_player.velocity.x > -topSpeed)
                {
                    _player.velocity += new Vector2(-acceleration, 0);
                }
                else
                {
                    _player.velocity =
                        new Vector2(-topSpeed, _player.velocity.y);
                }

                if (debug)
                {
                    print("a");
                }
            }

            if (Input.GetKey(KeyCode.D))
            {
                if (_player.velocity.x < topSpeed)
                {
                    _player.velocity += new Vector2(acceleration, 0);
                }
                else
                {
                    _player.velocity =
                        new Vector2(topSpeed, _player.velocity.y);
                }

                if (debug)
                {
                    print("d");
                }
            }
        }
    }
}