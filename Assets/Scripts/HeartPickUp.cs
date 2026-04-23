using UnityEngine;

//script for heal pickup
public class HeartPickUp : MonoBehaviour
{
    private SpawnCollectibles heartSpawner;
    [SerializeField] private AudioClip healSound;

    private void Start()
    {
        heartSpawner = GameObject.FindGameObjectWithTag("CollectiblesSpawner").GetComponent<SpawnCollectibles>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))//if picked by player
        {
            HealthComponent playerHealth = other.gameObject.GetComponent<HealthComponent>();//get the health component
            if (playerHealth != null && playerHealth.health < playerHealth.maxHealth)
            {
                SoundsManager.Instance.PlaySFX(healSound,0.5f);
                playerHealth.Heal();//heal player
                heartSpawner.SpawnHeals();//spawn another heal 
                Destroy(gameObject);
            }
        }
    }
}
