using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class unused : MonoBehaviour
{
    public GameObject Player;
    public float ForceModifier = 1;
    private Rigidbody2D _player;
    private Vector2 AppliedForce;
    private float _rot;
    private float _sensitivity;
    void Start()
    {
        Player.transform.position =  new Vector3(0, 0, 0);
        _player = Player.GetComponent<Rigidbody2D>();
        _sensitivity = 1f;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            AppliedForce.y = math.cos(_player.rotation)*ForceModifier;
            AppliedForce.x = math.sin(_player.rotation)*ForceModifier;
            print(math.cos(_player.rotation));
            print(math.sin(_player.rotation));
        }

        _player.AddForce(AppliedForce);
        
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
