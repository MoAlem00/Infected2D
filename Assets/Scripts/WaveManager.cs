using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

//script that handles zombie waves spawning and progression
public class WaveManager : MonoBehaviour
{
    [SerializeField] private AudioClip waveFinishedSound;
    [SerializeField] private float spawnDelay = 1f;
    [SerializeField] private UpgradesWindow upgradesPanel;
    [SerializeField] private UpgradesManager upgradesManager;

    [SerializeField] private EnemySpawner enemySpawner;
    private float minDelay = 0f;
    private float maxDelay = 1f;
    private float delayDecrease = 0.05f;
    private bool waveFinished;
    private bool upgradePanelShowing;
    [SerializeField] private float waveMultiplier = 1.3f;
    
    public int waveRound;
    public int totalEnemiesKilled;
    public int waveSize = 10;
    public int enemyKilled;
    
    private void Start()
    {
        StartCoroutine(StartWave());//spawn first wave at start
    }
    

    private void HandleWaveFinished()
    {
        SoundsManager.Instance.PlaySFX(waveFinishedSound,0.5f);
        waveFinished = true;
        StartCoroutine(UIManager.Instance.ShowWaveFinishedText(waveRound));
        enemyKilled = 0;
        waveSize = Mathf.CeilToInt(waveSize * waveMultiplier); //updating next wave size
        spawnDelay = Mathf.Clamp(spawnDelay - delayDecrease, minDelay,maxDelay);//updating delay between every enemy spawned
    }

    private void SpawnWave(int amount, float delay)//spawn wave with given size and given delay
    {
        StartCoroutine(enemySpawner.SpawnEnemyWave(amount,delay));
    }

    private IEnumerator StartWave()//starts new wave
    {
        waveRound++;
        UIManager.Instance.SetEnemiesText(waveSize);//when new wave starts update enemies left ui
        UIManager.Instance.SetWaveText(waveRound);//update wave round text
        yield return new WaitForSeconds(7f);//wait 5s until spawner start spawning enemies
        SpawnWave(waveSize,spawnDelay);//spawn enemies
    }

    public void OnButtonClick()
    {
        upgradesPanel.HideUpgradePanel();
        upgradePanelShowing = false;
        waveFinished = false;
        StartCoroutine(StartWave());
    }

    private void HandleEnemyKilled()
    {
        enemyKilled++;
        totalEnemiesKilled++;
        if (enemyKilled >= waveSize && !waveFinished)//if all enemies in that wave is killed -> wave is finished
        {
            HandleWaveFinished();
        }
        if (waveFinished && !upgradePanelShowing)//check if we can start the next wave
        {
            upgradePanelShowing = true;
            StartCoroutine(upgradesPanel.ShowUpgradePanel());
        }
        UIManager.Instance.SetEnemiesText(waveSize - enemyKilled);//update enemies left
    }

    private void OnEnable()
    {
        Enemy.OnEnemyDead += HandleEnemyKilled;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDead -= HandleEnemyKilled;
    }
}
