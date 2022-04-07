using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Systemagedon.App.Gameplay;

public class OnDestroySpawner : MonoBehaviour
{
    [SerializeField] private Transform _prefab;
    [SerializeField] private Planet _planet;


    public void Start()
    {
        _planet.Ruined += OnRuined;
    }


    public void OnDestroy()
    {
        _planet.Ruined -= OnRuined;
    }


    private void OnRuined(Planet sender)
    {
        print("destroy");
        Transform instance = Instantiate(_prefab);
        instance.transform.localScale = transform.localScale;
        instance.transform.position = transform.position;
        instance.transform.rotation = transform.rotation;
    }
}
