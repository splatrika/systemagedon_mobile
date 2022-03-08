using UnityEngine;
using System.Collections;
using System;

public abstract class FrequencySpawner<T> : MonoBehaviour where T : Component
{
    public event Action<T> OnSpawned;


    [SerializeField] private T _prefab;
    [SerializeField] private float _frequency;


    private Coroutine _spawningCoroutuine;


    protected abstract void SetupOnInstance(T instance);


    private void OnEnable()
    {
        _spawningCoroutuine = StartCoroutine(SpawningCoroutine());
    }


    private void OnDisable()
    {
        StopCoroutine(_spawningCoroutuine);
    }


    private IEnumerator SpawningCoroutine()
    {
        while (enabled)
        {
            T instance = Instantiate(_prefab);
            SetupOnInstance(instance);
            yield return new WaitForSeconds(_frequency);
        }
    }



}
