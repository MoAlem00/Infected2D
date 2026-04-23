using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesWindow : MonoBehaviour
{
    [SerializeField] private UpgradesManager upgradesManager;
    [SerializeField] private GameObject upgradesPanel;
    [SerializeField] private TextMeshProUGUI healthUpgradeCostText;
    [SerializeField] private TextMeshProUGUI ammoCapacityUpgradeCostText;
    [SerializeField] private TextMeshProUGUI speedUpgradeCostText;
    [SerializeField] private TextMeshProUGUI fireRateUpgradeCostText;
    [SerializeField] private TextMeshProUGUI notEnoughCoinsText;
    [SerializeField] private TextMeshProUGUI healthMaxedOutText;
    [SerializeField] private TextMeshProUGUI speedMaxedOutText;
    [SerializeField] private TextMeshProUGUI fireRateMaxedOutText;
    [SerializeField] private TextMeshProUGUI ammoCapacityMaxedOutText;
    [SerializeField] private Button healthUpgradeButton;
    [SerializeField] private Button speedUpgradeButton;
    [SerializeField] private Button ammoCapacityUpgradeButton;
    [SerializeField] private Button fireRateUpgradeButton;
    [SerializeField] private WeaponShoot weapon;


    public IEnumerator ShowUpgradePanel()
    {
        yield return new WaitForSeconds(4f);
        weapon.enabled = false;
        upgradesPanel.SetActive(true);
        ShowHealthUpgradeCost(upgradesManager.healthUpgradeCost);
        ShowSpeedUpgradeCost(upgradesManager.speedUpgradeCost);
        ShowAmmoCapacityUpgradeCost(upgradesManager.ammoCapacityUpgradeCost);
        ShowFireRateUpgradeCost(upgradesManager.fireRateUpgradeCost);
        Cursor.visible = true;
        Time.timeScale = 0;
    }
    
    public void HideUpgradePanel()
    {
        weapon.enabled = true;
        upgradesPanel.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1;
    }

    public void ShowHealthMaxedOutText()
    {
        healthMaxedOutText.text = "Maxed Out!";
    }

    public void ShowSpeedMaxedOutText()
    {
        speedMaxedOutText.text = "Maxed Out!";
    }

    public void ShowAmmoCapacityMaxedOutText()
    {
        ammoCapacityMaxedOutText.text = "Maxed Out!";
    }

    public void ShowFireRateMaxedOutText()
    {
        fireRateMaxedOutText.text = "Maxed Out!";
    }

    public void ShowHealthUpgradeCost(int cost)
    {
        healthUpgradeCostText.text = cost.ToString();
    }
    
    public void ShowSpeedUpgradeCost(int cost)
    {
        speedUpgradeCostText.text = cost.ToString();
    }

    public void ShowAmmoCapacityUpgradeCost(int cost)
    {
        ammoCapacityUpgradeCostText.text = cost.ToString();
    }

    public void ShowFireRateUpgradeCost(int cost)
    {
        fireRateUpgradeCostText.text = cost.ToString();
    }

    public void DisableHealthButton()
    {
        healthUpgradeButton.interactable = false;
        healthUpgradeButton.image.color = Color.grey;
    }

    public void EnableHealthButton()
    {
        healthUpgradeButton.interactable = true;
        healthUpgradeButton.image.color = Color.white;
    }
    
    public void DisableSpeedButton()
    {
        speedUpgradeButton.interactable = false;
        speedUpgradeButton.image.color = Color.grey;
    }
    public void EnableSpeedButton()
    {
        speedUpgradeButton.interactable = true;
        speedUpgradeButton.image.color = Color.white;
    }

    public void DisableAmmoCapacityButton()
    {
        ammoCapacityUpgradeButton.interactable = false;
        ammoCapacityUpgradeButton.image.color = Color.grey;
    }
    public void EnableAmmoCapacityButton()
    {
        ammoCapacityUpgradeButton.interactable = true;
        ammoCapacityUpgradeButton.image.color = Color.white;
    }

    public void DisableFireRateButton()
    {
        fireRateUpgradeButton.interactable = false;
        fireRateUpgradeButton.image.color = Color.grey;
    }

    public void EnableFireRateButton()
    {
        fireRateUpgradeButton.interactable = true;
        fireRateUpgradeButton.image.color = Color.white;
    }
    
}
