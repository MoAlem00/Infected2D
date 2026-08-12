using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

//script that handles zombie waves spawning and progression
public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private BasicObjectPooler enemyPool;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject portalEffect;
    [SerializeField] private AudioClip portalSound;
    [SerializeField] private AudioClip waveFinishedSound;
    [SerializeField] private float spawnDelay = 1f;
    [SerializeField] private UpgradesWindow upgradesPanel;
    [SerializeField] private UpgradesManager upgradesManager;
    
    private float minDelay = 0f;
    private float maxDelay = 1f;
    private float delayDecrease = 0.05f;
    private bool waveFinished;
    private bool upgradePanelShowing;
    [SerializeField] private float waveMultiplier = 1.3f;
    
    public int waveRound;
    public int totalEnemiesKilled;
    public Transform[] patrolPoints;
    public int waveSize = 10;
    public int enemyKilled;
    
    private void Start()
    {
        StartCoroutine(StartWave());//spawn first wave at start
    }

    // Update is called once per frame
    private void Update()
    {
        if (enemyKilled == waveSize && !waveFinished)//if all enemies in that wave is killed -> wave is finished
        {
            HandleWaveFinished();
        }
        if (waveFinished && !upgradePanelShowing)//check if we can start the next wave
        {
            upgradePanelShowing = true;
            StartCoroutine(upgradesPanel.ShowUpgradePanel());
        }
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

    private IEnumerator SpawnWave(int amount, float delay)//spawn wave with given size and given delay
    {
        for (int i = 0; i < amount; i++)
        {
            int rand = Random.Range(0, spawnPoints.Length);//picking random number
            //spawn points are empty game objects placed on the map
            Vector2 currentPos = spawnPoints[rand].position;//picking the spawn point using the random number
            AudioSource.PlayClipAtPoint(portalSound,currentPos,0.5f);//playing portal sound
            GameObject portal = Instantiate(portalEffect, currentPos, Quaternion.Euler(0f, 0f, 90f));//spawn portal at the given spawn point
            yield return new WaitForSeconds(0.3f);
            GameObject enemy = enemyPool.GetPooledObject();
            enemy.transform.position = currentPos;
            /*GameObject enemy = Instantiate(enemyPrefab, currentPos, Quaternion.identity);//spawn enemy at the given spawn point*/
            Destroy(portal, 1f);//destroy the portal
            yield return new WaitForSeconds(delay);
        }
    }

    private IEnumerator StartWave()//starts new wave
    {
        waveRound++;
        UIManager.Instance.SetEnemiesText(waveSize);//when new wave starts update enemies left ui
        UIManager.Instance.SetWaveText(waveRound);//update wave round text
        yield return new WaitForSeconds(7f);//wait 5s until spawner start spawning enemies
        StartCoroutine(SpawnWave(waveSize,spawnDelay));//spawn enemies
    }

    public void OnButtonClick()
    {
        upgradesPanel.HideUpgradePanel();
        upgradePanelShowing = false;
        waveFinished = false;
        StartCoroutine(StartWave());
    }
    
    
}
