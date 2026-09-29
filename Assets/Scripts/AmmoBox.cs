using UnityEngine;

//script to handle ammo pick up
public class AmmoBox : Item
{
    private SpawnCollectibles ammoSpawner;
    [SerializeField] private int ammo = 30;

    private void Start()
    {
        ammoSpawner = GameObject.Find("CollectiblesSpawner").GetComponent<SpawnCollectibles>();
    }

    protected override bool CanCollect(Collider2D other)
    {
        AssaultRifle weapon = other.GetComponentInChildren<AssaultRifle>();
        if (weapon == null || weapon.IsFull ) return false;
        return true;
    }

    protected override void PickUp(Collider2D other)
    {
        other.GetComponentInChildren<AssaultRifle>().GiveAmmo(ammo);
        ammoSpawner.SpawnAmmoBox();
    }
}
