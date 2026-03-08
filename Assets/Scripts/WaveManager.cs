using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveManager : MonoBehaviour
{
    private UIManager uiManager;
    public GameObject enemyPrefab;
    public int waveSize = 10;
    public Transform[] spawnPoints;
    public Transform[] patrolPoints;
    public float spawnDelay;
    private float minDelay = 0.5f;
    private float maxDelay = 1f;
    private float delayDecrease = 0.02f;
    //private int enemiesAlive;
    public int enemyKilled;
    private bool isSpawning;
    //private int waveIncrease = 3;
    public int waveRound;
    public int maxWaveReached;
    public int totalEnemiesKilled;
    bool canStartWave;
    public GameObject portalEffect;
    public AudioClip portalSound;
    
    
    void Start()
    {
        uiManager = GameObject.Find("HUD").GetComponent<UIManager>();
        StartCoroutine(StartWave(waveSize, spawnDelay));
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyKilled == waveSize)
        {
            canStartWave = true;
            StartCoroutine(uiManager.ShowWaveFinishedText(waveRound));
            enemyKilled = 0;
            waveSize = Mathf.CeilToInt(waveSize * 1.3f);
            spawnDelay = Mathf.Clamp(spawnDelay - delayDecrease, minDelay,maxDelay);
        }
        if (canStartWave && !isSpawning)
        {
            StartCoroutine(StartWave(waveSize, spawnDelay));
            canStartWave = false;
        }
    }

    IEnumerator SpawnWave(int amount, float delay)
    {
        for (int i = 0; i < amount; i++)
        {
            isSpawning = true;
            //int r = Random.Range(0, enemyPrefab.Length);
            //var curr = enemyPrefab[r];
            //enemiesAlive++;
            int rand = Random.Range(0, spawnPoints.Length);
            Vector2 currentPos = spawnPoints[rand].position;
            AudioSource.PlayClipAtPoint(portalSound,currentPos,0.5f);
            GameObject portal = Instantiate(portalEffect, currentPos, Quaternion.Euler(0f, 0f, 90f));
            yield return new WaitForSeconds(0.5f);
            GameObject enemy = Instantiate(enemyPrefab, currentPos, Quaternion.identity);
            Destroy(portal, 1f);
            yield return new WaitForSeconds(delay);
        }
        isSpawning = false;
    }

    IEnumerator StartWave(int amount, float delay)
    {
        maxWaveReached++;
        waveRound++;
        uiManager.SetEnemiesText(waveSize - enemyKilled);
        uiManager.SetWaveText(waveRound);
        yield return new WaitForSeconds(5f);
        StartCoroutine(SpawnWave(waveSize,spawnDelay));
    }
}
