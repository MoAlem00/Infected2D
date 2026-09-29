using UnityEngine;

public class Bandage : Item
{
    private SpawnCollectibles heartSpawner;
    [SerializeField] private int healAmount = 25;
    //private bool isPickedUp = false;

    private void Start()
    {
        heartSpawner = GameObject.FindGameObjectWithTag("CollectiblesSpawner").GetComponent<SpawnCollectibles>();
    }

    protected override bool CanCollect(Collider2D other)
    {
        HealthComponent health =  other.GetComponent<HealthComponent>();
        if (health == null || health.IsFull) return false;
        return true;
    }

    protected override void PickUp(Collider2D other)
    {
        HealthComponent health =  other.GetComponent<HealthComponent>();
        health.Heal(healAmount);
        heartSpawner.SpawnHeals();
    }
}
