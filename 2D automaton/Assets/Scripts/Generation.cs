using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Generation
{
    private readonly int _width, _height;

    public Generation(int width, int height)
    {
        _width = width;
        _height = height;
    }

    private int CountNeighbors(int[,] grid, int x, int y)
    {
        var sum = 0;
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                var col = (x + i + _width) % _width;
                var row = (y + j + _height) % _height;

                sum += grid[col, row];
            }
        }

        sum -= grid[x, y];
        return sum;
    }

    public void DoNewGeneration(int[,] newState, int[,] oldState)
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                var state = oldState[i,j];
                var neighbors = CountNeighbors(oldState, i, j);

                switch (state)
                    { 
                        case 0 when neighbors == 3:
                            newState[i, j] = 1;
                            break;
                        case 1 when neighbors is <2 or >3:
                            newState[i, j] = 0;
                            break;
                        default:
                            newState[i, j] = state;
                            break;
                    }
            }
        }
    }
}
