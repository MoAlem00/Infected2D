using UnityEngine;

public class Coins : Item
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
            coinsManager.CollectCoin();
            SoundsManager.Instance.PlaySFX(coinSound, 0.5f);
            Destroy(gameObject);
        }
    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            coinsManager.CollectCoin();
            SoundsManager.Instance.PlaySFX(coinSound, 0.5f);
            Destroy(gameObject);
        }
    }*/
}
