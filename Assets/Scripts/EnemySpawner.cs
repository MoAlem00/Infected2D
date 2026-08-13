using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject portalEffect;
    [SerializeField] private AudioClip portalSound;
    [SerializeField] private float spawnDelay = 1f;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator SpawnEnemy(int amount,float delay)
    {
        for (int i = 0; i < amount; i++)
        {
            int rand = Random.Range(0, spawnPoints.Length);//picking random number
            //spawn points are empty game objects placed on the map
            Vector2 currentPos = spawnPoints[rand].position;//picking the spawn point using the random number
            AudioSource.PlayClipAtPoint(portalSound,currentPos,0.5f);//playing portal sound
            GameObject portal = Instantiate(portalEffect, currentPos, Quaternion.Euler(0f, 0f, 90f));//spawn portal at the given spawn point
            yield return new WaitForSeconds(0.3f);
            /*GameObject enemy = Instantiate(enemyPrefab, currentPos, Quaternion.identity);//spawn enemy at the given spawn point*/
            Destroy(portal, 1f);//destroy the portal
            yield return new WaitForSeconds(delay);
        }
    }
}
