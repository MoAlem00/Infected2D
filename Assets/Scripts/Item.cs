using System;
using UnityEngine;

public abstract class Item :  MonoBehaviour
{
    [SerializeField] private AudioClip pickUpSound;
    public abstract void PickUp(Collider2D other);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SoundsManager.Instance.PlaySFX(pickUpSound,0.5f);
            PickUp(other);
            Destroy(gameObject);
        }
    }
}
