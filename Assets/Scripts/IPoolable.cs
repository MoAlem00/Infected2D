using System;

public interface IPoolable
{
    void OnSpawned();
    void OnDespawned();
    void SetReleaseCallback(Action action);
}
