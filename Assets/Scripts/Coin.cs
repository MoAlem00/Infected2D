using UnityEngine;

public class Coin : Item
{
    [SerializeField] private AudioClip coinSound;
    [SerializeField] private CoinsManager coinsManager;

    private void Start()
    {
        coinsManager = GameObject.FindGameObjectWithTag("CoinsManager").GetComponent<CoinsManager>();
    }

    public override void PickUp(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canBePicked = true;
            coinsManager.CollectCoin();
            SoundsManager.Instance.PlaySFX(coinSound, 0.5f);
        }
    }

    public override void OnSpawned()
    {
    }

    public override void OnDespawned()
    {
    }
}
