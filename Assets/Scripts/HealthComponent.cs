using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100;
    private int heal = 25;
    private UIManager uiManager;

    private void Start()
    {
        uiManager = GameObject.Find("HUD").GetComponent<UIManager>();
        health = maxHealth;
        if (CompareTag("Player"))
        {
            uiManager.playerHealthSlider.maxValue = maxHealth;
            uiManager.playerHealthSlider.value = health;
        }
    }

    public void TakeDamage(int dmg)
    {
        health = Mathf.Clamp(health - dmg, 0, maxHealth);
        if(CompareTag("Player"))
            uiManager.SetPlayerHealth(health);
    }

    public void ResetHealth()
    {
        health = maxHealth;
    }

    public void Heal()
    {
        health = Mathf.Clamp(health + heal, 0, maxHealth);
        if(CompareTag("Player"))
            uiManager.SetPlayerHealth(health);
    }
    
}
