using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject portalEffect;
    [SerializeField] private AudioClip portalSound;
    [SerializeField] private float spawnDelay = 1f;
    private ObjectPooler<EnemyController> enemyPooler;
    [SerializeField] private EnemyController enemyPrefab;
    [SerializeField] private Transform parent;
    [SerializeField] private int initialPoolSize = 20;
    
    public Transform[] SpawnPoints => spawnPoints;

    private void Awake()
    {
        enemyPooler = new ObjectPooler<EnemyController>(enemyPrefab,parent,initialPoolSize);
    }

    public IEnumerator SpawnEnemyWave(int amount,float delay)
    {
        for (int i = 0; i < amount; i++)
        {
            int rand = Random.Range(0, spawnPoints.Length);//picking random number
            //spawn points are empty game objects placed on the map
            Vector2 currentPos = spawnPoints[rand].position;//picking the spawn point using the random number
            AudioSource.PlayClipAtPoint(portalSound,currentPos,0.5f);//playing portal sound
            GameObject portal = Instantiate(portalEffect, currentPos, Quaternion.Euler(0f, 0f, 90f));//spawn portal at the given spawn point
            yield return new WaitForSeconds(0.3f);
            enemyPooler.GetPooledObject(currentPos,Quaternion.Euler(0f, 0f, 0f));
            Destroy(portal, 1f);//destroy the portal
            yield return new WaitForSeconds(delay);
        }
    }
}
