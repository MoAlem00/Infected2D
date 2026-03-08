using UnityEngine;

public class HeartPickUp : MonoBehaviour
{
    private SpawnCollectibles heart;
    public AudioClip healSound;

    private void Start()
    {
        heart = GameObject.Find("CollectiblesSpawner").GetComponent<SpawnCollectibles>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            HealthComponent playerHealth =  other.gameObject.GetComponent<HealthComponent>();
            if (playerHealth != null)
            {
                SoundsManager.Instance.PlaySFX(healSound);
                playerHealth.Heal();
                heart.SpawnHeals();
                Destroy(gameObject);
            }
        }
    }
}
