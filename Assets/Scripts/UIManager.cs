using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;
using Image = UnityEngine.UI.Image;


//script that handles game UI
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI enemiesText;
    [SerializeField] private TextMeshProUGUI ammoCapacityText;
    [SerializeField] private TextMeshProUGUI waveFinishText;
    [SerializeField] private TextMeshProUGUI enemyKilledText;
    [SerializeField] private TextMeshProUGUI maxWaveText;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI healthAmountText;
    
    [SerializeField] public Slider playerHealthSlider;
    [SerializeField] public Image damagePowerUp;
    [SerializeField] public Image speedPowerUp;
    [SerializeField] public Image invinciblePowerUp;
    [SerializeField] public Image fireRatePowerUp;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void SetHealthAmountText(int health)
    {
        healthAmountText.text = "" + health;
    }

    public void SetAmmoText(int ammo)//to update ammo amount
    {
        ammoText.text = "" + ammo;
    }

    public void SetAmmoCapacityText(int capacity)//to update ammo capacity amount
    {
        ammoCapacityText.text = "" + capacity;
    }

    public void SetWaveText(int wave)//to update current wave number
    {
        waveText.text = "Wave " + wave;
    }

    public void SetEnemiesText(int enemies)//to update how many enemies left
    {
        enemiesText.text = "" + enemies;
    }

    public void SetPlayerHealth(int playerHealth)//to update player health bar
    {
        playerHealthSlider.value = playerHealth;
    }

    public IEnumerator ShowWaveFinishedText(int wave)//to show a message when a wave is finished
    {
        yield return new WaitForSeconds(0.5f);
        waveFinishText.text = "Wave " + wave + " Finished";
        yield return new WaitForSeconds(2f);
        waveFinishText.text = "";
    }
    
    public void ShowResultText(int wave, int enemiesKilled)//to show the final result when the game is over
    {
        maxWaveText.text = "Highest Wave: " + wave;
        enemyKilledText.text = "Enemies Killed: " + enemiesKilled;
    }

    public void ShowSpeedPowerUp(float time,float powerUpTime)//to show how much time left for Speed power up
    {
        speedPowerUp.fillAmount = time/powerUpTime;
    }
    public void ShowDamagePowerUp(float time,float powerUpTime)//to show how much time left for Damage power up
    {
        damagePowerUp.fillAmount = time/powerUpTime;
    }
    public void ShowInvinciblePowerUp(float time,float powerUpTime)//to show how much time left for Invincibility power up
    {
        invinciblePowerUp.fillAmount = time/powerUpTime;
    }
    public void ShowFireRatePowerUp(float time,float powerUpTime)//to show how much time left for FireRate power up
    {
        fireRatePowerUp.fillAmount = time/powerUpTime;
    }

    public void UpdateCoinsText(int coins)
    {
        coinsText.text = "" + coins;
    }
}
