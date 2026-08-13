using System;
using UnityEngine;

public abstract class PooledBehaviour : MonoBehaviour,IPoolable
{
    private Action releaseCallback;
    

    public abstract void OnSpawned();
    public abstract void OnDespawned();
    
    public void SetReleaseCallback(Action action)
    {
        releaseCallback = action;
    }

    public void Despawn()
    {
        releaseCallback?.Invoke();
    }
}
