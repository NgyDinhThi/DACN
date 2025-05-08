using UnityEngine;

// Lớp PlayerHealth chịu trách nhiệm quản lý máu và xử lý sát thương của nhân vật người chơi
public class PlayerHealth : MonoBehaviour, IdamageAble
{
    [Header("Config")] // Hiển thị tiêu đề "Config" trong Inspector
    [SerializeField] private PlayerStats stats; // Tham chiếu đến ScriptableObject chứa chỉ số nhân vật

    private PlayerAnimation playerAnimation; // Dùng để điều khiển animation khi bị thương hoặc chết

    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>(); // Lấy component animation khi bắt đầu
    }

    private void Update()
    {
        // Kiểm tra mỗi frame: nếu máu nhỏ hơn hoặc bằng 0 thì gọi hàm chết
        if (stats.health <= 0f)
        {
            PlayerDeath();
        }
    }

    // Hàm này được gọi khi nhân vật nhận sát thương
    public void TakeDamage(float amount)
    {
        if (stats.health <= 0f) return; // Nếu đã chết rồi thì không nhận thêm sát thương

        stats.health -= amount; // Trừ máu

        // Hiển thị số sát thương trên màn hình tại vị trí nhân vật
        DmgManager.instance.hienSatthuong(amount, transform);

        if (stats.health <= 0f)
        {
            stats.health = 0f; // Đảm bảo không âm máu
            PlayerDeath();     // Gọi xử lý chết
        }
    }

    // Kiểm tra xem có thể hồi máu không (chưa đầy máu và chưa chết)
    public bool CanRestoreHealth()
    {
        return stats.health >= 0f && stats.health < stats.Max_health;
    }

    // Hồi máu cho nhân vật
    public void RestoredHealth(float amount)
    {
        stats.health += amount;
        // Đảm bảo không vượt quá máu tối đa
        stats.health = Mathf.Min(stats.health, stats.Max_health);
    }

    // Xử lý khi nhân vật chết: chạy animation chết
    private void PlayerDeath()
    {
        playerAnimation.SetDeadAni();
    }
}
