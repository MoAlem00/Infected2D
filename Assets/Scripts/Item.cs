using System;
using UnityEngine;

public abstract class Item :  PooledBehaviour
{
    [SerializeField] private AudioClip pickUpSound;
    protected bool canBePicked;
    public abstract void PickUp(Collider2D other);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"{name} {canBePicked}");
            PickUp(other);
            if(!canBePicked) return;
            SoundsManager.Instance.PlaySFX(pickUpSound,0.5f);
            Despawn();
        }
    }
}
