using UnityEngine;

//script to handle ammo pick up
public class AmmoBox : Item
{
    private SpawnCollectibles ammoSpawner;
    [SerializeField] private int ammo = 30;
    //private bool isPickedUp = false;

    private void Start()
    {
        ammoSpawner = GameObject.Find("CollectiblesSpawner").GetComponent<SpawnCollectibles>();
    }


    public override void PickUp(Collider2D other)
    {
        AssaultRifle weapon = other.GetComponentInChildren<AssaultRifle>();
        if (weapon != null)
        {
            if (weapon.IsFull)
            {
                canBePicked = false;
                return;
            }
            weapon.GiveAmmo(ammo);
            ammoSpawner.SpawnAmmoBox();
            canBePicked = true;
        }
    }

    public override void OnSpawned()
    {
        canBePicked = true;
    }

    public override void OnDespawned()
    {
    }
}
