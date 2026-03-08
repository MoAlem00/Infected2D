using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    private SpawnCollectibles ammo;
    public AudioClip pickUpSound;

    private void Start()
    {
        ammo = GameObject.Find("CollectiblesSpawner").GetComponent<SpawnCollectibles>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            WeaponShoot weapon = other.GetComponentInChildren<WeaponShoot>();
            if (weapon != null)
            {
                SoundsManager.Instance.PlaySFX(pickUpSound);
                weapon.GiveAmmo();
                ammo.SpawnAmmoBox();
                Debug.Log($"Picked up {weapon.ammoBox} Ammo");
                Destroy(gameObject);
            }
        }
    }
}
