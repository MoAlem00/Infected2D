using UnityEngine;


public class UpgradesManager : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private HealthComponent health;
    [SerializeField] private AssaultRifle weapon;
    [SerializeField] private CoinsManager coins;
    [SerializeField] private UpgradesWindow upgradesWindow;
    private int healthUpgradeAmount = 50;
    private float moveSpeedUpgradeAmount = 0.5f ;
    private int ammoCapacityUpgradeAmount = 30;
    private float fireRateUpgradeAmount = 0.1f;
    public int healthUpgradeCost = 10;
    public int speedUpgradeCost = 10;
    public int ammoCapacityUpgradeCost = 10;
    public int fireRateUpgradeCost = 10;


    private void Update()
    {

        if (health.maxHealth >= health.maxUpgradeHealth)
        {
            upgradesWindow.DisableHealthButton();
            upgradesWindow.ShowHealthMaxedOutText();
        }
        else if (coins.GetCoins() < healthUpgradeCost)
        {
            upgradesWindow.DisableHealthButton();
        }
        else
        {
            upgradesWindow.EnableHealthButton();
        }

        /////////////////////////////
        if (player.moveSpeed >= player.maxMoveSpeed)
        {
            upgradesWindow.DisableSpeedButton();
            upgradesWindow.ShowSpeedMaxedOutText();
        }
        else if (coins.GetCoins() < speedUpgradeCost)
        {
            upgradesWindow.DisableSpeedButton();
        }
        else
        {
            upgradesWindow.EnableSpeedButton();
        }

        ///////////////////////////
        if (weapon.ammoCapacity >= weapon.maxAmmoCapacityUpgrade)
        {
            upgradesWindow.DisableAmmoCapacityButton();
            upgradesWindow.ShowAmmoCapacityMaxedOutText();
        }
        else if (coins.GetCoins() < ammoCapacityUpgradeCost)
        {
            upgradesWindow.DisableAmmoCapacityButton();
        }
        else
        {
            upgradesWindow.EnableAmmoCapacityButton();
        }

        /////////////////////////////
        if (weapon.fireRate <= weapon.minFireRateUpgrade)
        {
            upgradesWindow.DisableFireRateButton();
            upgradesWindow.ShowFireRateMaxedOutText();
        }
        else if (coins.GetCoins() < fireRateUpgradeCost)
        {
            upgradesWindow.DisableFireRateButton();
        }
        else
        {
            upgradesWindow.EnableFireRateButton();
        }
    }


    public void UpgradeHealth()
    { 
        coins.SubtractCoin(healthUpgradeCost);
        health.UpgradeMaxHealth(healthUpgradeAmount);
        healthUpgradeCost = Mathf.CeilToInt(healthUpgradeCost * 1.3f);
        upgradesWindow.ShowHealthUpgradeCost(healthUpgradeCost);
    }
    
    public void UpgradeSpeed()
    {
        coins.SubtractCoin(speedUpgradeCost);
        player.UpgradeSpeed(moveSpeedUpgradeAmount);
        speedUpgradeCost = Mathf.CeilToInt(speedUpgradeCost * 1.3f);
        upgradesWindow.ShowSpeedUpgradeCost(speedUpgradeCost);
    }
    
    public void UpgradeAmmoCapacity()
    {
        coins.SubtractCoin(ammoCapacityUpgradeCost);
        weapon.UpgradeAmmoCapacity(ammoCapacityUpgradeAmount);
        ammoCapacityUpgradeCost = Mathf.CeilToInt(ammoCapacityUpgradeCost * 1.3f);
        upgradesWindow.ShowAmmoCapacityUpgradeCost(ammoCapacityUpgradeCost);
    }

    public void UpgradeFireRate()
    {
        coins.SubtractCoin(fireRateUpgradeCost);
        weapon.UpgradeFireRate(fireRateUpgradeAmount);
        fireRateUpgradeCost = Mathf.CeilToInt(fireRateUpgradeCost * 1.5f);
        upgradesWindow.ShowFireRateUpgradeCost(fireRateUpgradeCost);
    }

    
}
