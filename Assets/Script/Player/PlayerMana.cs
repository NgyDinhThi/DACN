using UnityEngine;
using System;

public class PlayerMana : MonoBehaviour
{
    [SerializeField] private PlayerStats stats;


    private void Update()
    {
        // Kiểm tra nếu phím H được nhấn, nhân vật sẽ nhận 2 sát thương
        if (Input.GetKeyDown(KeyCode.M))
        {
            UseMana(2f);
        }
    }
    public void UseMana(float amount)
    {
        if (stats.mana >= amount)
        {
            stats.mana = Mathf.Max(stats.mana-= amount, 0f);
        }
    }

    

}