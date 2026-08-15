using UnityEngine;

public class Bandage : Item
{
    private SpawnCollectibles heartSpawner;
    [SerializeField] private int healAmount = 25;

    private void Start()
    {
        heartSpawner = GameObject.FindGameObjectWithTag("CollectiblesSpawner").GetComponent<SpawnCollectibles>();
    }

    public override void PickUp(Collider2D other)
    {
        HealthComponent health =  other.GetComponent<HealthComponent>();
        if (health != null)
        {
            if(health.IsFull) return;
            health.Heal(healAmount);
            heartSpawner.SpawnHeals();
        }
    }
}
