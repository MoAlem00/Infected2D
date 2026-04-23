using System;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    [SerializeField] private int coinAmount = 5;
    [SerializeField] private UIManager uiManager;


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
}
