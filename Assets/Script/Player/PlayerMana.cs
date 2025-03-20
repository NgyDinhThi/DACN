using UnityEngine;
using System;

public class PlayerMana : MonoBehaviour
{
    [SerializeField] private PlayerStats stats; // Tham chiếu đến chỉ số của nhân vật

    private void Update()
    {
        // Kiểm tra nếu phím M được nhấn, nhân vật sẽ trừ 2 mana
        if (Input.GetKeyDown(KeyCode.M))
        {
            UseMana(2f);
        }
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
