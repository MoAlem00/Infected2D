using System;
using UnityEngine;
using Random = UnityEngine.Random;

//script that handles spawn collectibles(ammo box, heals, power ups)
public class SpawnCollectibles : MonoBehaviour
{
    
    /*[SerializeField] private GameObject ammoBox;
    [SerializeField] private GameObject healBox;*/
    [SerializeField] private Transform collectibles;
    [SerializeField] private GameObject barrelsParent;
    [SerializeField] private LayerMask unSpawnableLayer;
    [SerializeField] private GameObject[] barrels;
    [SerializeField] private AmmoBox ammoBox;
    [SerializeField] private Bandage bandage;
    [SerializeField] private int initialPoolSize = 5;
    private ObjectPooler<AmmoBox> ammoBoxPooler;
    private ObjectPooler<Bandage> bandagePooler;
    
    private int boxesAtStart = 3;
    private int barrelsAmount = 10;
    private Vector3 spawnPosition;
    //map borders so it dont spawn outside the map
    private int minY = -3;
    private int maxY = 52;
    private int minX = -32;
    private int maxX = 48;
    
    private void Awake()
    {
        ammoBoxPooler = new ObjectPooler<AmmoBox>(ammoBox,collectibles,initialPoolSize);
        bandagePooler = new ObjectPooler<Bandage>(bandage,collectibles,initialPoolSize);
    }

    private void Start()
    {
        for (int i = 0; i < boxesAtStart; i++)//spawn ammo boxes and heals at the start
        {
            SpawnAmmoBox();
            SpawnHeals();
        }
        for (int i = 0; i < barrelsAmount; i++)
        {
            SpawnBarrels();
        }

    }

    public void SpawnBarrels()
    {
        int randomX = Random.Range(minX, maxX);
        int randomY = Random.Range(minY, maxY);
        spawnPosition = new Vector2(randomX, randomY);
        Collider2D hit = Physics2D.OverlapCapsule(spawnPosition,new Vector2(3f,3f),0f, unSpawnableLayer);
        if (hit != null)
        {
            SpawnBarrels();
        }
        else
        {
            GameObject barrel = Instantiate(barrels[Random.Range(0, barrels.Length)], spawnPosition, Quaternion.identity);
            barrel.transform.SetParent(barrelsParent.transform);
        }
            
    }
    public void SpawnAmmoBox()//spawn ammo box
    {
        int randomX = Random.Range(minX, maxX);//pick random x inside map
        int randomY = Random.Range(minY, maxY);//pick random y inside map
        spawnPosition = new Vector2(randomX, randomY);//set the x and y for position
        //check if the given position overlaps blocked layers so it dont spawn on buildings, tress .... where player cant reach it
        Collider2D hit = Physics2D.OverlapBox(spawnPosition,new Vector2(3f,3f),0f, unSpawnableLayer); 
        if (hit != null)
        {
            //Debug.Log(hit.gameObject.name);
            SpawnAmmoBox();
        }
        else
        {
            ammoBoxPooler.GetPooledObject(spawnPosition,Quaternion.identity);
        }
    }

    public void SpawnHeals()//same logic as above
    {
        int randomX = Random.Range(minX, maxX);
        int randomY = Random.Range(minY, maxY);
        spawnPosition = new Vector2(randomX, randomY);
        Collider2D hit = Physics2D.OverlapBox(spawnPosition,new Vector2(3f,3f),0f, unSpawnableLayer);
        if (hit != null)
        {
            SpawnHeals();
        }
        else
        {
            bandagePooler.GetPooledObject(spawnPosition,Quaternion.identity);
        }
    }
}
