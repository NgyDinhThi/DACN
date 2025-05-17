using BayatGames.SaveGameFree;
using System;
using UnityEngine;

public class CoinsManager : Singleton<CoinsManager>
{

    [SerializeField] private float cointTest;
    public float Coins { get; set; }
    private const string COIN_KEY = "Coins";

    private void Start()
    {
        Coins = SaveGame.Load(COIN_KEY, cointTest);
        
    }

    public void AddCoin(float amount)
    {
        Coins += amount;
        SaveGame.Save(COIN_KEY, Coins);

    }

    public void RemoveCoin(float amount)
    {
        if (Coins >= amount)
        {
            Coins -= amount;
            SaveGame.Save(COIN_KEY, Coins);
        }

    }

}