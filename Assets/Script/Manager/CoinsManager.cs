using BayatGames.SaveGameFree;
using System;
using UnityEngine;

public class CoinsManager : Singleton<CoinsManager>
{
    [SerializeField] private float cointTest;
    public float Coins { get; set; }
    private const string COIN_KEY = "Coins";

    // Gán giá trị khởi tạo cho Coins từ dữ liệu đã lưu
    private void Start()
    {
        Coins = SaveGame.Load(COIN_KEY, cointTest);
    }

    // Tăng số lượng coin và lưu lại vào bộ nhớ
    public void AddCoin(float amount)
    {
        Coins += amount;
        SaveGame.Save(COIN_KEY, Coins);
    }

    // Giảm số lượng coin nếu đủ và lưu lại vào bộ nhớ
    public void RemoveCoin(float amount)
    {
        if (Coins >= amount)
        {
            Coins -= amount;
            SaveGame.Save(COIN_KEY, Coins);
        }
    }
}
