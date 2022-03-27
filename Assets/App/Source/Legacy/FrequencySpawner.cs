using UnityEngine;
using System.Collections;
using System;

public abstract class FrequencySpawner<T> : MonoBehaviour where T : Component
{
    public event Action<T> Spawned;

    
    public float SpawnPerSecond { get => 1 / _frequency; }
    public bool IsRunned { get => _spawningCoroutuine != null; }
    public bool RunOnStart { get => _runOnStart; }


    [SerializeField] private T _prefab;
    [SerializeField] protected float _frequency = 1;
    [SerializeField] private bool _runOnStart = true;


    private Coroutine _spawningCoroutuine;


    protected abstract void SetupOnInstance(T instance);
    protected virtual void Validate() { }
    protected virtual void OnSpawnerDestroy() { }


    public void RaiseSpawnPerSeconds(float value)
    {
        SetSpawnsPerSecond(SpawnPerSecond + value);
    }


    public void Run()
    {
        if (IsRunned)
        {
            throw new InvalidOperationException("Already runnded");
        }
        _spawningCoroutuine = StartCoroutine(SpawningCoroutine());
    }


    public void Stop()
    {
        if (!IsRunned)
        {
            throw new InvalidOperationException("Trying to stop not runned spawner");
        }
        StopCoroutine(_spawningCoroutuine);
        _spawningCoroutuine = null;
    }


    private void SetSpawnsPerSecond(float value)
    {
        _frequency = 1f / value;
    }


    private void Start()
    {
        if (_runOnStart && !IsRunned)
        {
            Run();
        }
    }


    private void OnDestroy()
    {
        if (IsRunned)
        {
            Stop();
        }
        OnSpawnerDestroy();
    }


    private void OnValidate()
    {
        if (_frequency <= 0)
        {
            _frequency = 1;
            Debug.LogError($"Frequency must be positive and can't equals zero");
        }
        Validate();
    }


    private IEnumerator SpawningCoroutine()
    {
        while (enabled)
        {
            T instance = Instantiate(_prefab);
            SetupOnInstance(instance);
            Spawned?.Invoke(instance);
            yield return new WaitForSeconds(_frequency);
        }
    }



}
