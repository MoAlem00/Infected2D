using System;
using UnityEngine;

public class CoinsPickUp : MonoBehaviour
{
    [SerializeField] private AudioClip coinSound;
    [SerializeField] private CoinsManager coinsManager;

    private void Start()
    {
        coinsManager = GameObject.FindGameObjectWithTag("CoinsManager").GetComponent<CoinsManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            coinsManager.CollectCoin();
            SoundsManager.Instance.PlaySFX(coinSound, 0.5f);
            Destroy(gameObject);
        }
    }
}
