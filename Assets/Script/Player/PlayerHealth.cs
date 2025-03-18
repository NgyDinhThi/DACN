using UnityEngine;

public class PlayerHealth : MonoBehaviour, IdamageAble
{
    [Header("Config")] // Hiển thị tiêu đề "Config" trong Inspector
    [SerializeField] private PlayerStats stats; // Tham chiếu đến thông tin chỉ số của nhân vật

    private void Update()
    {
        // Kiểm tra nếu phím P được nhấn, nhân vật sẽ nhận 2 sát thương
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(2f);
        }
    }

    public void TakeDamage(float amount)
    {
        // Giảm máu của nhân vật theo lượng sát thương nhận được
        stats.health -= amount;

        // Nếu máu giảm xuống 0 hoặc thấp hơn, gọi hàm xử lý cái chết của nhân vật
        if (stats.health <= 0f)
            PlayerDeath();
    }

    private void PlayerDeath()
    {
        // In ra console thông báo nhân vật đã chết
        Debug.Log("chet me may di");
    }
}