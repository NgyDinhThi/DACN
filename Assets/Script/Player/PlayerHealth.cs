using UnityEngine;

// Quản lý máu, sát thương và hồi máu cho nhân vật người chơi
public class PlayerHealth : MonoBehaviour, IdamageAble
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats; // Tham chiếu dữ liệu nhân vật

    private PlayerAnimation playerAnimation; // Điều khiển animation
    private bool hasDied = false;

    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>(); // Gán animation
    }

    private void Update()
    {
        if (stats.health <= 0f && !hasDied)
        {
            hasDied = true;
            PlayerDeath(); // Chỉ gọi đúng 1 lần
        }
    }

    // Gọi khi nhận sát thương
    public void TakeDamage(float amount)
    {
        if (stats.health <= 0f) return;

        stats.health -= amount;
        AudioManager.instance.Play("EnemyHit");
        DmgManager.instance.hienSatthuong(amount, transform);

        if (stats.health <= 0f)
        {
            stats.health = 0f;
            PlayerDeath();
        }
    }

    // Kiểm tra có thể hồi máu không
    public bool CanRestoreHealth()
    {
        return stats.health >= 0f && stats.health < stats.Max_health;
    }

    // Gọi để hồi máu
    public void RestoredHealth(float amount)
    {
        stats.health += amount;
        stats.health = Mathf.Min(stats.health, stats.Max_health);
    }

    // Gọi khi nhân vật chết
    private void PlayerDeath()
    {
        playerAnimation.SetDeadAni();
        AudioManager.instance.Play("PlayerDeath");
    }

}
