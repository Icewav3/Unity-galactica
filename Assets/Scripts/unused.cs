using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class Unused : MonoBehaviour
{
    [FormerlySerializedAs("Player")] public GameObject player;
    [FormerlySerializedAs("ForceModifier")] public float forceModifier = 1;
    private Rigidbody2D _player;
    private Vector2 _appliedForce;
    private float _rot;
    private float _sensitivity;
    void Start()
    {
        player.transform.position =  new Vector3(0, 0, 0);
        _player = player.GetComponent<Rigidbody2D>();
        _sensitivity = 1f;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _appliedForce.y = math.cos(_player.rotation)*forceModifier;
            _appliedForce.x = math.sin(_player.rotation)*forceModifier;
            print(math.cos(_player.rotation));
            print(math.sin(_player.rotation));
        }

        _player.AddForce(_appliedForce);
        
        //Rotation Input
        if (Input.GetKey(KeyCode.A))
        {
            _rot = _player.rotation + _sensitivity;

        }
        if (Input.GetKey(KeyCode.D))
        {
            _rot = _player.rotation - _sensitivity;

        }
        _player.MoveRotation(_rot);
        
    }
}
