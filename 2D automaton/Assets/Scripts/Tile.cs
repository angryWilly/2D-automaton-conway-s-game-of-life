using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;

    private Generation _generation;
    
    public bool isActive;
    
    private void Start()
    {
        _renderer.color = isActive ? _offsetColor : _baseColor;
    }
    
    public void ChangeColor(bool active)
    {
        isActive = active;
        _renderer.color = active ? _offsetColor : _baseColor;
    }
    
    private void OnMouseDown()
    {
        if (isActive)
        {
            isActive = false;
            _renderer.color = _baseColor;
        }
        else
        {
            isActive = true;
            _renderer.color = _offsetColor;
        }
    }

    private void OnMouseEnter()
    {
        _highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        _highlight.SetActive(false);
    }
}
