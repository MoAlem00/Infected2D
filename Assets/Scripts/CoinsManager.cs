using System;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    [SerializeField] private int coinAmount = 5;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private Coin coin;
    [SerializeField] private Transform parent;
    [SerializeField] private int initialPoolSize = 10;
    private float coinTime = 5f;
    private float coinTimer;
    private ObjectPooler<Coin> coinPooler;
    
    private void Awake()
    {
        coinPooler = new ObjectPooler<Coin>(coin,parent,initialPoolSize);
    }

    
    private void Start()
    {
        uiManager.UpdateCoinsText(coinAmount);
    }

    public void CollectCoin()
    {
        coinAmount += 2;
        uiManager.UpdateCoinsText(coinAmount);
    }
    
    public void SubtractCoin(int price)
    {
        coinAmount -= price;
        uiManager.UpdateCoinsText(coinAmount);
    }

    public int GetCoins()
    {
        return coinAmount;
    }

    public void CheatCoins()
    {
        coinAmount += 1000;
        uiManager.UpdateCoinsText(coinAmount);
    }

    private void OnEnable()
    {
        Enemy.OnEnemyDead += SpawnCoin;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDead -= SpawnCoin;
    }

    public void SpawnCoin(Vector3 pos)
    {
        coin = coinPooler.GetPooledObject(pos,Quaternion.identity);
    }
}
