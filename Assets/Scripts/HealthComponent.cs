using UnityEngine;

//health script used for both player and enemy
public class HealthComponent : MonoBehaviour
{
    public int health = 100;
    
    [SerializeField] public int maxHealth = 100;
    public int maxUpgradeHealth = 500;
    private int heal = 25;

    private void Start()
    {
        health = maxHealth;
        if (CompareTag("Player"))//show player health
        {
            UIManager.Instance.playerHealthSlider.maxValue = maxHealth;
            UIManager.Instance.playerHealthSlider.value = health;
            UIManager.Instance.SetHealthAmountText(health);
        }
    }

    public void TakeDamage(int dmg)
    {
        health = Mathf.Clamp(health - dmg, 0, maxHealth);
        if (CompareTag("Player")) //update player health bar when taking damage
        {
            UIManager.Instance.SetPlayerHealth(health);
            UIManager.Instance.SetHealthAmountText(health);
        }
            
    }

    public void Heal()
    {
        health = Mathf.Clamp(health + heal, 0, maxHealth);
        if (CompareTag("Player")) //update player health bar when healing
        {
            UIManager.Instance.SetPlayerHealth(health);
            UIManager.Instance.SetHealthAmountText(health);
        }
            
    }

    public void UpgradeMaxHealth(int upgradeAmount)
    {
        maxHealth = Mathf.Clamp(maxHealth + upgradeAmount, 0, maxUpgradeHealth);
        UIManager.Instance.playerHealthSlider.maxValue = maxHealth;
    }
    
}
