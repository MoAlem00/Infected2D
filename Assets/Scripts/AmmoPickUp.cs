using UnityEngine;

//script to handle ammo pick up
public class AmmoPickUp : MonoBehaviour
{
    private SpawnCollectibles ammoSpawner;
    [SerializeField] private AudioClip pickUpSound;

    private void Start()
    {
        ammoSpawner = GameObject.Find("CollectiblesSpawner").GetComponent<SpawnCollectibles>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if the player picks up the ammo box
        if(other.CompareTag("Player"))
        {
            //get the weapon reference because weapon stores the ammo.
            WeaponShoot weapon = other.GetComponentInChildren<WeaponShoot>();
            if (weapon != null && weapon.currentAmmo < weapon.ammoCapacity)
            {
                SoundsManager.Instance.PlaySFX(pickUpSound,1f);//play pick up sound
                weapon.GiveAmmo();//give ammo to the player
                ammoSpawner.SpawnAmmoBox();//spawn another ammo box
                Destroy(gameObject);
            }
        }
    }
}
