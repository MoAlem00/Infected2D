using Unity.VisualScripting;
using UnityEngine;

public class SpawnCollectibles : MonoBehaviour
{
    public GameObject ammoBox;
    public GameObject healBox;
    public GameObject collectibles;
    public int boxesAtStart = 5;
    //private int healsAtStart = 10;
    private Vector3 spawnPosition;
    private int minY = -3;
    private int maxY = 52;
    private int minX = -32;
    private int maxX = 48;
    public LayerMask buildingLayer;
    

    private void Start()
    {
        for (int i = 0; i < boxesAtStart; i++)
        {
            SpawnAmmoBox();
            SpawnHeals();
        }
    }

    public void SpawnAmmoBox()
    {
        int randomX = Random.Range(minX, maxX);
        int randomY = Random.Range(minY, maxY);
        spawnPosition = new Vector2(randomX, randomY);
        Collider2D hit = Physics2D.OverlapBox(spawnPosition,new Vector2(3f,3f),0f, buildingLayer);
        if (hit != null)
        {
            //Debug.Log(hit.gameObject.name);
            SpawnAmmoBox();
        }
        else
        {
            GameObject ammo;
            ammo = Instantiate(ammoBox, spawnPosition, Quaternion.identity);
            ammo.transform.SetParent(collectibles.transform);
        }
    }

    public void SpawnHeals()
    {
        int randomX = Random.Range(minX, maxX);
        int randomY = Random.Range(minY, maxY);
        spawnPosition = new Vector2(randomX, randomY);
        Collider2D hit = Physics2D.OverlapBox(spawnPosition,new Vector2(3f,3f),0f, buildingLayer);
        if (hit != null)
        {
            SpawnHeals();
        }
        else
        {
            GameObject heal;
            heal = Instantiate(healBox, spawnPosition, Quaternion.identity);
            heal.transform.SetParent(collectibles.transform);
        }
    }
}
