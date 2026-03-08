using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI enemiesText;
    public Slider playerHealthSlider;
    public TextMeshProUGUI waveFinishText;
    public TextMeshProUGUI enemyKilledText;
    public TextMeshProUGUI maxWaveText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAmmoText(int ammo)
    {
        ammoText.text = "" + ammo;
    }

    public void SetWaveText(int wave)
    {
        waveText.text = "Wave " + wave;
    }

    public void SetEnemiesText(int enemies)
    {
        enemiesText.text = "" + enemies;
    }

    public void SetPlayerHealth(int playerHealth)
    {
        playerHealthSlider.value = playerHealth;
    }

    public IEnumerator ShowWaveFinishedText(int wave)
    {
        yield return new WaitForSeconds(0.5f);
        waveFinishText.text = "Wave " + wave + " Finished";
        yield return new WaitForSeconds(2f);
        waveFinishText.text = "";
    }
    
    public void ShowResultText(int wave, int enemiesKilled)
    {
        maxWaveText.text = "Highest Wave: " + wave;
        enemyKilledText.text = "Enemies Killed: " + enemiesKilled;
    }
}
