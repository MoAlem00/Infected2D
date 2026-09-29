using System;
using UnityEngine;

public abstract class Item :  PooledBehaviour
{
    [SerializeField] private AudioClip pickUpSound;
    private bool isCollected;
    protected abstract void PickUp(Collider2D other);

    protected virtual bool CanCollect(Collider2D other)
    {
        return true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if(isCollected) return;
        if(!CanCollect(other)) return;
        isCollected = true;
        PickUp(other);
        SoundsManager.Instance.PlaySFX(pickUpSound,0.5f);
        Despawn();
    }

    public override void OnDespawned()
    {
        
    }

    public override void OnSpawned()
    {
        isCollected = false;
    }
    
}
