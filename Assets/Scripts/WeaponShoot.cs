using System;
using UnityEngine;

//script that handles player shooting and ammo
public class WeaponShoot : MonoBehaviour
{
    
    //[SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform firePos;
    [SerializeField] private MuzzleFlash muzzleFlash;
    [SerializeField] private AudioClip gunShotClip;
    [SerializeField] private AudioClip emptyGunSound;
    [SerializeField] private Transform parent;
    [SerializeField] private int initialPoolSize = 10;
    
    //public Weapon currWeapon;
    public int currentAmmo;
    private int ammoBox = 30;
    private float clipDelay = 0.2f;
    private float playClipTime;
    private float nextFireTime;
    [SerializeField] public int ammoCapacity = 50;
    public int maxAmmoCapacityUpgrade = 500;
    public float minFireRateUpgrade = 0.05f;
    public float maxFireRateUpgrade = 0.5f;
    
    private int damage = 50;
    public float fireRate = 0.5f;

    private ObjectPooler<Bullet> bulletPooler;
    [SerializeField] private Bullet bullet;

    private void Awake()
    {
        bulletPooler = new ObjectPooler<Bullet>(bullet,parent,initialPoolSize);
    }

    private void Start()
    {
        currentAmmo = ammoCapacity;
        UIManager.Instance.SetAmmoText(currentAmmo);//update ammo ui at the start
        UIManager.Instance.SetAmmoCapacityText(ammoCapacity);//update ammo capacity at the start
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            if (currentAmmo > 0)//if player has ammo
            {
                SoundsManager.Instance.PlaySFX(gunShotClip,0.3f);
                Shoot();
                nextFireTime = Time.time + fireRate;//shoot cooldown
            }
            else//if player doesnt have ammo
            {
                if (Time.time > playClipTime) //cooldown for playing empty gun sound
                {
                    SoundsManager.Instance.PlaySFX(emptyGunSound, 0.7f);
                    //Debug.Log("No Ammo");
                    playClipTime = Time.time + clipDelay;
                }
            }
        }
    }

    private void Shoot()//shoot
    {
        muzzleFlash.TriggerMuzzleFlash();//play muzzle flash effect
        bullet = bulletPooler.GetPooledObject(firePos.position, firePos.rotation);
        bullet.SetDamage(damage);
        currentAmmo--;//decrease ammo
        UIManager.Instance.SetAmmoText(currentAmmo);//update ammo left
    }

    public void GiveAmmo()//give ammo when picking ammo box
    {
        currentAmmo = Mathf.Clamp(currentAmmo + ammoBox, 0, ammoCapacity);
        UIManager.Instance.SetAmmoText(currentAmmo);//update ammo amount in ui
    }

    public void UpgradeAmmoCapacity(int upgradeAmount)
    {
        ammoCapacity = Mathf.Clamp(ammoCapacity + upgradeAmount, 0, maxAmmoCapacityUpgrade);
        UIManager.Instance.SetAmmoCapacityText(ammoCapacity);//update ammo capacity in ui
    }

    public void UpgradeFireRate(float upgradeAmount)
    {
        fireRate = Mathf.Clamp(fireRate - upgradeAmount, minFireRateUpgrade, maxFireRateUpgrade);
    }
}
