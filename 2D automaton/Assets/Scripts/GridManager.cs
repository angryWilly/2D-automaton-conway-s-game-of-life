using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Tile _tilePrefab;
    private GameObject _tilesParent;
    private Camera _camera;
    private bool _buttonPressed;


    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private float _delay;
    
    private Random rnd = new Random();
    private Tile[,] _grid;
    private Generation _generation;
    private int[,] _oldStates;
    private int[,] _newStates;

    private void Start()
    {
        Time.fixedDeltaTime = _delay;
        GrabCamera();

        _generation = new Generation(_width, _height);
        
        _oldStates = new int[_width, _height];
        SetupArrayWithRandomStates(_oldStates);

        _grid = new Tile[_width, _height];
        _grid = GenerateGrid(_width, _height, _oldStates);
    }

    private void GrabCamera()
    {
        _camera = Camera.main;
        _camera!.transform.position = new Vector3((float)_width / 2 - .5f, (float)_height/2 - .5f, -10);
    }

    private void SetupArrayWithRandomStates(int[,] states)
    {
        for (int i = 0; i < states.GetLength(0); i++)
        {
            for (int j = 0; j < states.GetLength(1); j++)
            {
                states[i, j] = rnd.Next(2);
            }
        }
    }
    
    private static void SetupArrayWithZeroStates(int[,] states)
    {
        for (int i = 0; i < states.GetLength(0); i++)
        {
            for (int j = 0; j < states.GetLength(1); j++)
            {
                states[i, j] = 0;
            }
        }
    }

    private Tile[,] GenerateGrid(int width, int height, int[,] states)
    {
        var tilesParent = new GameObject($"StateTiles");
        var arrTiles = new Tile[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                arrTiles[x,y] = Instantiate(_tilePrefab, new Vector2(x, y), Quaternion.identity,
                    tilesParent.transform);
                arrTiles[x,y].name = x + " " + y;
                if (states[x,y] == 1)
                {
                    arrTiles[x,y].ChangeColor(true);
                }
            }
        }
        _tilesParent = tilesParent;

        return arrTiles;
    }

    private void FillTheGrid(Tile[,] grid, int[,] states)
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                grid[x, y].ChangeColor(states[x, y] == 1);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _buttonPressed = _buttonPressed == false;
    }

    private void FixedUpdate()
    {
        if (_buttonPressed)
            DrawEvolution();
    }

    private void DrawEvolution()
    {
        _newStates = new int[_width, _height];
        SetupArrayWithZeroStates(_newStates);
        _generation.DoNewGeneration(_newStates, _oldStates);
        _oldStates = _newStates;
        FillTheGrid(_grid, _newStates);
    }
}
