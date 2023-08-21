using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Client;
using Content;
using Ui;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine.UIElements;

public class Control : MonoBehaviour
{
    public bool debug = true;
    public float acceleration = 0.01f;
    public float topSpeed = 3;
    private Vector3 _mousePos;
    private bool _dead;
    private int _id;
    private Camera _camera;
    private Rigidbody2D _player;
    private bool _editor;
    private GameObject _canvas;

    void Start()
    {
        //Load Fuel types
        List<Fuel> fuelList = FuelManager.GetFuelTypes();

       if (debug){foreach (var fuel in fuelList)
       {
           Debug.Log($"Fuel Type: {fuel.FuelName}");
           Debug.Log($"Fuel Color: {fuel.FuelColor}");
           Debug.Log($"Base Price: {fuel.BasePricePerUnit}");
       }}
       _player = GetComponentInChildren<Rigidbody2D>();
        _camera = GetComponentInChildren<Camera>();
        _canvas = gameObject.transform.Find("Canvas").gameObject;
        _dead = true;
        _id = Time.frameCount;
        _canvas.SetActive(false);
    }
    
    void Update()
    {
        //Build mode
        bool tab = Input.GetKeyDown(KeyCode.Tab);
        _mousePos = _camera.ScreenToWorldPoint(Input.mousePosition); //gets real mouse position
        
        if (debug)
        {
            //print("Current mouse Position " + _mousePos);
        }
        if (tab)
        {
            _editor = !_editor; 
            _canvas.SetActive(_editor); //Toggles UI
            Time.timeScale = _editor ? 0 : 1; //toggles sim
            if (debug){print("editor state: "+ _editor);}
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
                    _player.velocity += new Vector2(0,acceleration);
                }
                else
                {
                    _player.velocity = new Vector2(_player.velocity.x,topSpeed);
                }
                if (debug){print("w");}
            }
            if (Input.GetKey(KeyCode.S))
            {
                if (_player.velocity.y > -topSpeed)
                {
                    _player.velocity += new Vector2(0,-acceleration);
                }
                else
                {
                    _player.velocity = new Vector2(_player.velocity.x,-topSpeed);
                }
                if (debug){print("s");}
            }
            if (Input.GetKey(KeyCode.A))
            {
                if (_player.velocity.x > -topSpeed)
                {
                    _player.velocity += new Vector2(-acceleration,0);
                }
                else
                {
                    _player.velocity = new Vector2(-topSpeed, _player.velocity.y);
                }
                if (debug){print("a");}
            }
            if (Input.GetKey(KeyCode.D))
            {
                if (_player.velocity.x < topSpeed)
                {
                    _player.velocity += new Vector2(acceleration,0);
                }
                else
                {
                    _player.velocity = new Vector2(topSpeed, _player.velocity.y);
                }
                if (debug){print("d");}
            }
        }
    }
}
