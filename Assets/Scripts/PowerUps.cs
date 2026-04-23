/*
using System.Collections;
using UnityEngine;

//script that handles picking up power ups and activating them
public class PowerUps : MonoBehaviour
{
    private PlayerController player;
    private float powerUpTime = 10f;
    private int normalDamage;
    private int powerUpDamage;
    private float normalFireRate;
    private float powerUpFireRate;
    private float normalSpeed;
    private float powerUpSpeed;
    private WeaponShoot weapon;
    private SpriteRenderer color;
    private SpawnCollectibles powerUpSpawner;
    //to store the active power up coroutine so i can restart them
    private Coroutine damageRoutine;
    private Coroutine invincibilityRoutine;
    private Coroutine speedRoutine;
    private Coroutine fireRateRoutine;
    
    public bool isInvincible = false;
    
    
    [SerializeField] private LayerMask excludeLayer; //this is axe layer so player can be invincible to it
    [SerializeField] private AudioClip powerUpSound;
    
    private void Start()
    {
        powerUpSpawner = GameObject.Find("CollectiblesSpawner").GetComponent<SpawnCollectibles>();
        player = GetComponent<PlayerController>();
        color = player.GetComponent<SpriteRenderer>();
        weapon = player.GetComponentInChildren<WeaponShoot>();
        normalFireRate = weapon.fireRate;//fire rate normal value
        powerUpFireRate = normalFireRate / 2f;//fire rate power up value
        normalDamage = weapon.damage;//damage normal value
        powerUpDamage = normalDamage * 2;//damage power up value
        normalSpeed = player.moveSpeed;//normal speed
        powerUpSpeed = normalSpeed + 2f;//power up speed
    }
    
    private IEnumerator ActivateDamagePowerUp()//activate damage boost power up
    {
        //when power up activate it start new timer
        float currentTime = powerUpTime;//set a timer for power up
        weapon.damage = powerUpDamage;//set new damage for power up
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UIManager.Instance.ShowDamagePowerUp(currentTime,powerUpTime);//update power up icon in ui
            yield return null;
        }
        weapon.damage = normalDamage;//change damage to normal damage
        damageRoutine = null;//resetting coroutine
    }

    private IEnumerator ActivateFireRatePowerUp()
    {
        float currentTime = powerUpTime;
        weapon.fireRate = powerUpFireRate;
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UIManager.Instance.ShowFireRatePowerUp(currentTime, powerUpTime);
            yield return null;
        }
        weapon.fireRate = normalFireRate;
        fireRateRoutine = null;
    }

    private IEnumerator ActivateSpeedPowerUp()
    {
        float currentTime = powerUpTime;
        player.moveSpeed = powerUpSpeed;
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UIManager.Instance.ShowSpeedPowerUp(currentTime, powerUpTime);
            yield return null;
        }
        player.moveSpeed = normalSpeed;
        speedRoutine = null;
    }

    private IEnumerator ActivateInvincibilityPowerUp()
    {
        Collider2D playerCollider = player.GetComponent<Collider2D>();
        if (playerCollider != null)
        {
            isInvincible = true;
            float currentTime = powerUpTime;
            color.color = Color.yellow;
            playerCollider.excludeLayers = excludeLayer;
            while (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                UIManager.Instance.ShowInvinciblePowerUp(currentTime, powerUpTime);
                yield return null;
            }
            playerCollider.excludeLayers = 0;
            color.color = Color.white;
            isInvincible = false;
            invincibilityRoutine = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FireRate"))
        {
            UIManager.Instance.fireRatePowerUp.fillAmount = 1f;//when taking the power up it fills the power up icon
            if (fireRateRoutine != null)//if the coroutine is already playing
                StopCoroutine(fireRateRoutine);//stop it so it can start again
            SoundsManager.Instance.PlaySFX(powerUpSound,0.5f);
            fireRateRoutine = StartCoroutine(ActivateFireRatePowerUp());//play the power coroutine
            powerUpSpawner.SpawnPowerUp();//spawn another power up
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Speed"))//same logic as above
        {
            UIManager.Instance.speedPowerUp.fillAmount = 1f;
            if (speedRoutine != null)
                StopCoroutine(speedRoutine);
            SoundsManager.Instance.PlaySFX(powerUpSound,0.5f);
            speedRoutine = StartCoroutine(ActivateSpeedPowerUp());
            powerUpSpawner.SpawnPowerUp();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Invincible"))//same logic as above
        {
            UIManager.Instance.invinciblePowerUp.fillAmount = 1f;
            if (invincibilityRoutine != null)
                StopCoroutine(invincibilityRoutine);
            SoundsManager.Instance.PlaySFX(powerUpSound,0.5f);
            invincibilityRoutine = StartCoroutine(ActivateInvincibilityPowerUp());
            powerUpSpawner.SpawnPowerUp();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Damage"))//same logic as above
        {
            UIManager.Instance.damagePowerUp.fillAmount = 1f;
            if (damageRoutine != null)
                StopCoroutine(damageRoutine);
            SoundsManager.Instance.PlaySFX(powerUpSound,0.5f);
            damageRoutine = StartCoroutine(ActivateDamagePowerUp());
            powerUpSpawner.SpawnPowerUp();
            Destroy(other.gameObject);
        }
    }
}
*/
