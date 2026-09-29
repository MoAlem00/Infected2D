using UnityEngine;

public class Coin : Item
{
    [SerializeField] private AudioClip coinSound;
    [SerializeField] private CoinsManager coinsManager;

    private void Start()
    {
        coinsManager = GameObject.FindGameObjectWithTag("CoinsManager").GetComponent<CoinsManager>();
    }

    protected override void PickUp(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            coinsManager.CollectCoin();
            SoundsManager.Instance.PlaySFX(coinSound, 0.5f);
        }
    }
    
}
