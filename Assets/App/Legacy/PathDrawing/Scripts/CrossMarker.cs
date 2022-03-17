using UnityEngine;
using System;

public class CrossMarker : MonoBehaviour
{
    [SerializeField] Renderer _renderer;


    public void Start()
    {
        if (!_renderer)
        {
            Debug.LogError("_renderer must be assigned");
        }
    }


    public void Show()
    {
        _renderer.enabled = true;
    }


    public void Hide()
    {
        _renderer.enabled = false;
    }
}
