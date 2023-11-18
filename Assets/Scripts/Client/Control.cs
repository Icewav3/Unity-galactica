using System;
using System.Collections.Generic;
using Client;
using Content;
using Content.Blocks.MovementBlocks;
using UnityEngine;

public class Control : MonoBehaviour
{
    public bool debug = true;
    public float acceleration = 1f;
    public float topSpeed = 3;
    private Camera _camera;
    private GameObject _canvas;
    private bool _dead;
    private bool _editor;
    private int _id;
    private Vector3 _mousePos;
    private Rigidbody2D _player;
    private Vector2 movement;
    public Vector2 Deceleration = new Vector2(0.1f, 0.1f);
    private Vector2 targetVelocity;


    /// <summary>
    /// /Attach this to the parent node of the player, Camera, and Canvas, it manages movement and menu positioning
    /// </summary>
    void Start()
    {
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
        //Check for nulls
        Helper.Util.CheckForNull(_player, "Player");
        Helper.Util.CheckForNull(_camera, "Camera");
        Helper.Util.CheckForNull(_canvas, "Canvas");
        //Client initialization
        _dead = true;
        _id = Time.frameCount;
        _canvas.SetActive(false);
    }

    void Update()
    {
        //Build mode
        bool tab = Input.GetKeyDown(KeyCode.Tab);
        _mousePos = _camera.ScreenToWorldPoint(Input.mousePosition); //gets real mouse position
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

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
            Vector2 movement = new Vector2(horizontalInput, verticalInput);
            Debug.Log(movement);
            movement = movement.normalized;
            //Debug.Log(movement);
            if (_player.velocity.magnitude < topSpeed)
            {
                // Use AddForce for physics-based movement
                targetVelocity = movement * acceleration;
            }
            else
            {
                // If the velocity exceeds topSpeed, clamp the magnitude while preserving the direction of travel.
                targetVelocity = _player.velocity.normalized * topSpeed;
            }
            
            float smoothTime = 0.1f;

            // Smoothly interpolate the current velocity towards the target velocity
            _player.velocity = Vector2.Lerp(_player.velocity, targetVelocity, smoothTime);
            
            //Debug.Log(movement);


            /*if (_dead)
            {
                Player.Create(_id, true);
            }

            //onkeypress
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                print(Player.Menu());
            }

            Input.

            //on keyhold
            if (Input.GetKey(KeyCode.W))
            {
                if (_player.velocity.y < topSpeed)
                {
                    _player.velocity += new Vector2(0, acceleration);
                }
                else
                {
                    _player.velocity = new Vector2(_player.velocity.x, topSpeed);
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
                    _player.velocity = new Vector2(_player.velocity.x, -topSpeed);
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
                    _player.velocity = new Vector2(-topSpeed, _player.velocity.y);
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
                    _player.velocity = new Vector2(topSpeed, _player.velocity.y);
                }

                if (debug)
                {
                    print("d");
                }
            }*/
        }
    }

    private void FixedUpdate()
    {
        _player.AddForce(new Vector2(movement.x * acceleration, movement.y * acceleration), ForceMode2D.Force);
    }
}