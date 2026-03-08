using System;
using System.Collections;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    UIManager uiManager;
    public GameObject bulletPrefab;
    public Transform firePos;
    public float fireRate = 0.2f;
    public int currentAmmo;
    private int maxAmmo = 50;
    public int ammoBox = 30;
    public AudioClip gunShotClip;
    public AudioClip emptyGunSound;
    private float clipDelay = 0.2f;
    private float playClipTime;
    private float nextFireTime;
    public MuzzleFlash muzzleFlash;

    private void Start()
    {
        uiManager = GameObject.Find("HUD").GetComponent<UIManager>();
        currentAmmo = maxAmmo;
        uiManager.SetAmmoText(currentAmmo);
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            if (currentAmmo > 0)
            {
                SoundsManager.Instance.PlaySFX(gunShotClip,0.3f);
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
            else
            {
                if (Time.time > playClipTime)
                {
                    SoundsManager.Instance.PlaySFX(emptyGunSound,0.7f);
                    //Debug.Log("No Ammo");
                    playClipTime = Time.time + clipDelay;
                }
                
            }
        }
    }

    void Shoot()
    {
        muzzleFlash.TriggerMuzzleFlash();
        Instantiate(bulletPrefab, firePos.position, firePos.rotation);
        currentAmmo--;
        uiManager.SetAmmoText(currentAmmo);
    }

    public void GiveAmmo()
    {
        currentAmmo = Mathf.Clamp(currentAmmo + ammoBox, 0, maxAmmo);
        uiManager.SetAmmoText(currentAmmo);
    }
}
