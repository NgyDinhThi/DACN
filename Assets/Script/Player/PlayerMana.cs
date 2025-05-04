using UnityEngine;
using System;

public class PlayerMana : MonoBehaviour
{
    [SerializeField] private PlayerStats stats; // Tham chiếu đến chỉ số của nhân vật

    private void Update()
    {
       
    }

    public void UseMana(float amount)
    {
        // Kiểm tra nếu nhân vật có đủ mana để sử dụng
        if (stats.mana >= amount)
        {
            // Giảm mana nhưng đảm bảo không nhỏ hơn 0
            stats.mana = Mathf.Max(stats.mana -= amount, 0f);
        }
    }
}
