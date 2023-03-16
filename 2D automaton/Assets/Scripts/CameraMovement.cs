using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float _movementSpeed = 5f;
    private Vector2 _motion;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        _motion = new Vector2(0, Input.GetAxisRaw("Vertical"));
        transform.Translate(_motion * (_movementSpeed * Time.deltaTime));
        
        _camera.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * _movementSpeed;
    }
}
